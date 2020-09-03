using System;
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
    Vector3 downAxis;
    public float jumpPower = 4f;

    //movement
    Vector3 relativeWalkingSpeed = Vector3.zero;
    Vector3 absoluteWalkingSpeed = Vector3.zero;
    Vector3 jumpSpeed;
    Vector3 speed;

    accelerator xAccelerator;
    accelerator zAccelerator;

    fall m_fall;

    //contact
    ContactPoint contact = new ContactPoint();
    bool shouldOrient = false;

    // Start is called before the first frame update
    void Start()
    {
        m_fall = GetComponent<fall>();
        m_Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();
        transform.rotation = Quaternion.Euler(0, 90, 0);

        xAccelerator = new accelerator(m_fall);
        zAccelerator = new accelerator(m_fall);
    }

    // Update is called once per frame
    public void update(InputData m_InputData)
    {
        this.m_InputData = m_InputData;

        shouldOrient = m_fall.getShouldOrient();    
        orientSelf();

        downAxis = gravity.downAxis;
        
        //get values for speed in  x z plane
        calculateWalkSpeeds();

        calculateVertical();

        speed = absoluteWalkingSpeed + jumpSpeed;

        m_Rigidbody.MovePosition(m_Rigidbody.position + (speed) * Time.deltaTime);
    }


    void orientSelf()
    {
        //orient oursleves to normal of current surface, and don't reorient until next contact
        if (shouldOrient)
        {
            contact = m_fall.getPreviousSurface();
            Collider.isTrigger = true;
            transform.rotation = Quaternion.FromToRotation(transform.up, contact.normal) * transform.rotation;
            Collider.isTrigger = false;
            //transform.position = contact.point + contact.normal;
            Debug.DrawRay(transform.position, 2 * downAxis, Color.white, 2);
            m_fall.setShouldOrient(false);
        }
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
        jump();
    }


    void jump()
    {
        if(m_fall.getIsFloored())
        {
            jumpSpeed = Vector3.zero;
        }

        if (m_fall.getIsFloored() && (m_InputData.jumpPressed == 1 ? true : false))
        {
            Debug.Log("uwu");
            jumpSpeed = transform.up * jumpPower * 2;
            if (m_fall.getIsGrounded() || downAxis == Vector3.zero)
            {
                jumpSpeed = transform.up * jumpPower;
            }
            
        }
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

    fall m_fall;


    public accelerator(fall m_fall)
    {
        this.m_fall = m_fall;
    }

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
        if (currentDirection != 0 && m_fall.getIsFloored())
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