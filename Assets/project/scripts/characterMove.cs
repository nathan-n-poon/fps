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
    Vector3 relativeDownAxis = new Vector3();
    Vector3 relativeSpeed = new Vector3();
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

        relativeSpeed.x = accelerate("Horizontal");
        relativeSpeed.z = accelerate("Vertical");

        downAxisSpeed += gravity * Time.deltaTime;
        if (isGrounded())
        {
            downAxisSpeed = 0;
        }

        relativeDownAxis = transform.InverseTransformDirection(downAxis);
        relativeSpeed += relativeDownAxis * downAxisSpeed;

        checkCollisions();

        if ((transform.right * speedX + transform.forward * speedZ).magnitude > 1)
        {
            speed = (transform.right * speedX + transform.forward * speedZ).normalized + transform.up * relativeSpeed.z;
        }

        else
        {
            speed = (transform.right * relativeSpeed.x + transform.up * relativeSpeed.y + transform.forward * relativeSpeed.z);
        }

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
            relativeSpeed.x = Mathf.Min(0, relativeSpeed.x);
        }
        if (Physics.Raycast(transform.position, -transform.right, xDistance))
        {
            relativeSpeed.x = Mathf.Max(0, relativeSpeed.x);
        }

        if (Physics.Raycast(transform.position, transform.up, yDistance))
        {
            relativeSpeed.y = Mathf.Min(0, relativeSpeed.y);
        }
        if (Physics.Raycast(transform.position, -transform.up, yDistance))
        {
            relativeSpeed.y = Mathf.Max(0, relativeSpeed.y);
        }

        if (Physics.Raycast(transform.position, transform.forward, zDistance))
        {
            relativeSpeed.z = Mathf.Min(0, relativeSpeed.z);
        }
        if (Physics.Raycast(transform.position, -transform.forward, zDistance))
        {
            relativeSpeed.z = Mathf.Max(0, relativeSpeed.z);
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
}
