using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;

public class characterMove : MonoBehaviour
{
    public Rigidbody m_Rigidbody;
    public Collider Collider;

    public float maxSpeed = 4f;
    public float walkAccel = 1f;
    public float gravity =- 10f;
    public float skinDepth = 0.3f;
    public float mu = 0.15f;
    public float mass = 10f;

    Vector3 downAxis = new Vector3(0,1,0);
    Vector3 slideDirection = new Vector3(0,0,0);
    Vector3 speed;
    float speedX;
    float speedY;
    float speedZ;
    float slopeSpeed = 0;
    bool shouldOrient = false;
    ContactPoint contact = new ContactPoint();
    Collider previousSurface = new Collider();
    float slopeAcceleration;

    float downAxisSpeed;

    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;   
        //
        //
        //
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldOrient)
        {
            count++;
            shouldOrient = false;
            transform.position = contact.normal + contact.point;
            transform.rotation = Quaternion.FromToRotation(transform.up, contact.normal) * transform.rotation;

            Debug.Log(transform.InverseTransformDirection(downAxis));
        }

        checkCollisions();

        speedX = accelerate("Horizontal");
        speedZ = accelerate("Vertical");
        speed = (transform.right * speedX + transform.forward * speedZ);
        if ((transform.right * speedX + transform.forward * speedZ).magnitude > 1)
        {
            speed = (transform.right * speedX + transform.forward * speedZ).normalized;
        }

        downAxisSpeed += gravity * Time.deltaTime;
        if (isGrounded())
        {
            downAxisSpeed = 0;
        }

        slopeSpeed += slopeAcceleration * Time.deltaTime;
        if (downAxis == transform.up || !isGrounded())
        {
            slopeSpeed = 0;
        }


        //Debug.Log(slopeSpeed);



        speed = speed * maxSpeed + downAxis * downAxisSpeed + slideDirection * slopeSpeed;

        m_Rigidbody.MovePosition(m_Rigidbody.position + (speed) * Time.deltaTime);

    }

    void FixedUpdate()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider != previousSurface)
        {
            shouldOrient = true;
            //print("Points colliding: " + other.contacts.Length);
            //print("First normal of the point that collide: " + other.contacts[0].point);
            contact = other.GetContact(0);
            setSlopeSpeed();
        }
        previousSurface = other.collider;
    }

    void checkCollisions()
    {
        //float DisstanceToTheGround = Collider.bounds.extents.y;   
        //return Physics.Raycast(transform.position, Vector3.down, DisstanceToTheGround + 0.1f);
        float xDistance = Collider.bounds.extents.x + skinDepth;
        float yDistance = Collider.bounds.extents.y + skinDepth;
        float zDistance = Collider.bounds.extents.z + skinDepth;

        if (Physics.Raycast(transform.position, transform.right, xDistance))
        {
            speedX = Mathf.Min(0, speedX);
        }
        if (Physics.Raycast(transform.position, -transform.right, xDistance))
        {
            speedX = Mathf.Max(0, speedX);
        }

        if (Physics.Raycast(transform.position, transform.up, yDistance))
        {
            speedY = Mathf.Min(0, speedY);
        }
        if (Physics.Raycast(transform.position, -transform.up, yDistance))
        {
            speedY = Mathf.Max(0, speedY);
        }

        if (Physics.Raycast(transform.position, transform.forward, zDistance))
        {
            speedZ = Mathf.Min(0, speedZ);
        }
        if (Physics.Raycast(transform.position, -transform.forward, zDistance))
        {
            speedZ = Mathf.Max(0, speedZ);
        }
    }

    float accelerate(string axis)
    { 
        float velocity = (axis == "Horizontal") ? speedX : speedZ;
        float magnitude = Mathf.Abs(velocity);
        float currentDirection = Input.GetAxisRaw(axis);
        float previousDirection = 0;
        float accel = walkAccel * Time.deltaTime;

        if(magnitude != 0)
        {
            previousDirection = velocity / magnitude;
        }

        if (currentDirection != 0 && isGrounded())
        {
            Debug.Log("nein");
            velocity += accel * currentDirection;
            if(Mathf.Abs(velocity) > maxSpeed)
            {
                velocity = maxSpeed * currentDirection;
            }
        }

        velocity -= accel * previousDirection / 2;
        if(magnitude != 0)
        {
            if(velocity / Math.Abs(velocity) != previousDirection )
            {
                velocity = 0;
            }
        }
        
        return velocity;
    }

    bool isGrounded()
    {
        bool isGrounded = false;
        float yDistance = Collider.bounds.extents.y + skinDepth + 0.2f;

        if (Physics.Raycast(transform.position, transform.up, yDistance))
        {
            isGrounded = true;
        }
        if (Physics.Raycast(transform.position, -transform.up, yDistance))
        {
            isGrounded = true;
        }
        return isGrounded;
    }

    void setSlopeSpeed()
    {
        //if (isGrounded())
        //{
        //slideDirection = new Vector3(0.7f, 0.7f, 0f);
        slideDirection = -(contact.normal - downAxis);
        float theta = Vector3.Angle(contact.normal, downAxis);
            slopeAcceleration = gravity * (Mathf.Sin(theta) - Mathf.Cos(theta) * mu);
        //}
    }

}
