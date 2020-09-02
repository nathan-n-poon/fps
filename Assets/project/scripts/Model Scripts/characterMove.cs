﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;
using UnityEngine;
public class characterMove : MonoBehaviour
{
    InputData m_InputData;

    //character objects
    Rigidbody m_Rigidbody;
    Collider Collider;

    //constants
    public Vector3 downAxis = new Vector3(0,-1,0);
    public float jumpPower = 4f;

    //movement
    Vector3 relativeWalkingSpeed = Vector3.zero;
    Vector3 absoluteWalkingSpeed = Vector3.zero;
    Vector3 effectiveGravity = new Vector3();
    Vector3 jumpSpeed;
    Vector3 speed;
    float downAxisSpeed;

    accelerator xAccelerator;
    accelerator zAccelerator;

    //contact
    bool shouldOrient = false;
    ContactPoint contact = new ContactPoint();
    ContactPoint previousSurface = new ContactPoint();
    float xDistance;
    float yDistance;
    float zDistance;
    bool isFloored = false;
    bool isGrounded = false;
    int touchingCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();
        Time.timeScale = 1.0f;
        transform.rotation = Quaternion.Euler(0, 90, 0);
        xDistance = Collider.bounds.extents.x;
        yDistance = Collider.bounds.extents.y;
        zDistance = Collider.bounds.extents.z;

        xAccelerator = new accelerator();
        zAccelerator = new accelerator();
    }

    // Update is called once per frame
    public void update(InputData m_InputData)
    {
        isFloored = isFloored | Physics.Raycast(transform.position, -transform.up, yDistance);
        this.m_InputData = m_InputData;
        
        orientSelf();
        
        //get values for speed in  x z plane
        calculateWalkSpeeds();

        calculateVertical();

        speed = absoluteWalkingSpeed + effectiveGravity + jumpSpeed;

        m_Rigidbody.MovePosition(m_Rigidbody.position + (speed) * Time.deltaTime);
    }

    //set orient flag
    void OnCollisionEnter(Collision other)
    {
        shouldOrient = true;

        isFloored = true;

        touchingCount++;

        int temp = other.contacts.Length - 1;
        if(temp >= 0)
        {
            previousSurface = other.contacts[temp];
        }
    }       

    void OnCollisionExit(Collision collision)
    {
        touchingCount--;
        if(touchingCount == 0)
        {
            isFloored = false;
        }
        else
        {
            isFloored = true;
        }
    }

    void orientSelf()
    {
        //orient oursleves to normal of current surface, and don't reorient until next contact
        if (shouldOrient)
        {
            contact = previousSurface;
            Collider.isTrigger = true;
            transform.rotation = Quaternion.FromToRotation(transform.up, contact.normal) * transform.rotation;
            Collider.isTrigger = false;
            //transform.position = contact.point + contact.normal;
            Debug.DrawRay(transform.position, 2 * downAxis, Color.white, 2);
            shouldOrient = false;
        }
        isGrounded = (isFloored && contact.normal == -(downAxis.normalized)) ? true : false;
    }

    void calculateWalkSpeeds()
    {
        if(m_InputData.horizontalMove != 0 || (relativeWalkingSpeed.x != 0))
        {
            relativeWalkingSpeed.x = xAccelerator.walk(relativeWalkingSpeed.x, m_InputData.horizontalMove);
        }
        if (m_InputData.verticalMove != 0 || (relativeWalkingSpeed.z != 0))
        {
            relativeWalkingSpeed.z = zAccelerator.walk(relativeWalkingSpeed.z, m_InputData.verticalMove);
        }

        //normalise x z movement if necessary and point them in their appropriate global direction

        Vector3 temp = (transform.right * relativeWalkingSpeed.x + transform.forward * relativeWalkingSpeed.z);
        absoluteWalkingSpeed = temp;

    }

    void calculateVertical()
    {
        calculateGravity();
        jump();

        if (isGrounded)
        {
            downAxisSpeed = 0;
        }

        effectiveGravity = downAxisSpeed * downAxis;
    }

    void calculateGravity()
    {
        downAxisSpeed += downAxis.magnitude * Time.deltaTime;

        //are we on slope relative to gravity?
        effectiveGravity = transform.InverseTransformDirection(downAxis);
        checkCollisions(ref effectiveGravity);
        //if (isFloored && effectiveGravity.magnitude < Mathf.Abs(gravity) / 1.25)
        //{
        //    downAxisSpeed *= 0.5f;
        //    Debug.Log(effectiveGravity.magnitude);
        //}
    }

    void jump()
    {
        if(isFloored)
        {
            jumpSpeed = Vector3.zero;
        }

        if (isFloored && (m_InputData.jumpPressed == 1 ? true : false))
        {
            Debug.Log("uwu");
            jumpSpeed = transform.up * jumpPower / 1;
            if (isGrounded || downAxis == Vector3.zero)
            {
                jumpSpeed = transform.up * jumpPower;
            }

            Debug.Log(jumpSpeed);
        }
    }

    //set speed of transform in direction of object to zero
    void checkCollisions(ref Vector3 otherSpeed)
    {
        if (Physics.Raycast(transform.position, transform.right, xDistance))
        {
            otherSpeed.x = Mathf.Min(0, otherSpeed.x);
        }
        if (Physics.Raycast(transform.position, -transform.right, xDistance))
        {
            otherSpeed.x = Mathf.Max(0, otherSpeed.x);
        }

        if (Physics.Raycast(transform.position, transform.up, yDistance))
        {
            otherSpeed.y = Mathf.Min(0, otherSpeed.y);
        }
        if (Physics.Raycast(transform.position, -transform.up, yDistance))
        {
            otherSpeed.y = Mathf.Max(0, otherSpeed.y);
            //Debug.Log("wrong");
        }

        if (Physics.Raycast(transform.position, transform.forward, zDistance))
        {
            otherSpeed.z = Mathf.Min(0, otherSpeed.z);
        }
        if (Physics.Raycast(transform.position, -transform.forward, zDistance))
        {
            otherSpeed.z = Mathf.Max(0, otherSpeed.z);
        }
    }

    public bool getIsFloored()
    {
        return isFloored;
    }

    public bool getIsGrounded()
    {
        return isGrounded;
    }

    public Vector3 getWalkingSpeed()
    {
        return relativeWalkingSpeed;
    }

    public void setWalkingSpeed(Vector3 newWalkingSpeed)
    {
        relativeWalkingSpeed = newWalkingSpeed;
    }

}

class accelerator
{
    float previousVelocity;
    float previousDirection;

    float currentDirection;

    float walkAccel = 3f;
    public float maxSpeed = 6f;
    float accel;


    public float walk(float previousSpeed, float currentDirection)
    {
        float newVelocity;
        update(previousSpeed);
        newVelocity = accelerate(currentDirection, previousVelocity);
        newVelocity = decelerate(newVelocity);
        return newVelocity;

    }

    public void update(float previousSpeed)
    {

        accel = walkAccel * Time.deltaTime;
        previousVelocity = previousSpeed;
        previousDirection = Mathf.Sign(previousVelocity);
    }

    public float accelerate(float currentDirection, float previousVelocity)
    {
        float newVelocity = previousVelocity;
        this.currentDirection = currentDirection;
        if (currentDirection != 0 && GameObject.FindObjectOfType<characterMove>().getIsFloored())
        {
            newVelocity = previousVelocity +  accel * currentDirection;
            if(Mathf.Abs(newVelocity) > maxSpeed)
            {
                newVelocity = maxSpeed * currentDirection;
            }
        }
        return newVelocity;
    }

    public float decelerate(float previousVelocity)
    {
        float newVelocity = previousVelocity;
        //if (GameObject.FindObjectOfType<characterMove>().getIsFloored())
        //{
            if (Mathf.Sign(previousVelocity - accel * previousDirection / 1.5f) == Mathf.Sign(previousVelocity))
            {
                newVelocity -= accel * previousDirection / 1.5f;
            }
            else
                newVelocity = 0;
        
            return newVelocity;
        //}
    }
}