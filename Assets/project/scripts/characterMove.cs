using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;

public class characterMove : MonoBehaviour
{
    //character objects
    public Rigidbody m_Rigidbody;
    public Collider Collider;

    //constants
    public float maxSpeed = 4f;
    public float walkAccel = 1f;
    public float gravity =- 10f;
    public float skinDepth = 0.3f;
    Vector3 downAxis = new Vector3(0,1,0);

    //movement
    Vector3 relativeDownAxis = new Vector3();
    Vector3 relativeSpeed = new Vector3();
    Vector3 speed;
    float downAxisSpeed;

    //contact
    bool shouldOrient = false;
    ContactPoint contact = new ContactPoint();
    Collider previousSurface = new Collider();

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;   
    }

    // Update is called once per frame
    void Update()
    {
        //orient oursleves to normal of current surface, and don't reorient until next contact
        if(shouldOrient)
        {
            shouldOrient = false;
            transform.position = contact.normal + contact.point;
            transform.rotation = Quaternion.FromToRotation(transform.up, contact.normal) * transform.rotation;

            //Debug.Log(transform.InverseTransformDirection(downAxis));
        }

        //get values for speed in  x z plane
        relativeSpeed.x = accelerate("Horizontal");
        relativeSpeed.z = accelerate("Vertical");

        //get current downAxis, transfrom into vector relative to player transform, and add gravity's speed in that direction 
        downAxisSpeed += gravity * Time.deltaTime;
        if (isGrounded() && transform.up == downAxis)
        {
            downAxisSpeed = 0;
        }
        relativeDownAxis = transform.InverseTransformDirection(downAxis);
        relativeSpeed += relativeDownAxis * downAxisSpeed;

        //halt movement, relative to player transform, in any direction where there is object 
        checkCollisions();

        //normalise x z movement if necessary and point them in their appropriate global direction
        if ((transform.right * relativeSpeed.x + transform.forward * relativeSpeed.z).magnitude > 1)
        {
            speed = (transform.right * relativeSpeed.x + transform.forward * relativeSpeed.z).normalized;
        }
        else
        {
            speed = (transform.right * relativeSpeed.x + transform.forward * relativeSpeed.z);
        }

        //set all speeds to global directions
        speed = speed * maxSpeed + transform.up * relativeSpeed.y;

        m_Rigidbody.MovePosition(m_Rigidbody.position + (speed) * Time.deltaTime);

    }

    //set orient flag
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

    //set speed of transform in direction of object to zero
    void checkCollisions()
    {
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
        //get the previous velocity
        float velocity = (axis == "Horizontal") ? relativeSpeed.x : relativeSpeed.z;
        float magnitude = Mathf.Abs(velocity);
        float previousDirection = 0;

        float currentDirection = Input.GetAxisRaw(axis);
        float accel = walkAccel * Time.deltaTime;

        if(magnitude != 0)
        {
            previousDirection = velocity / magnitude;
        }

        if (currentDirection != 0 && isGrounded())
        {
            //Debug.Log("nein");
            velocity += accel * currentDirection;
            if(Mathf.Abs(velocity) > maxSpeed)
            {
                velocity = maxSpeed * currentDirection;
            }
        }

        velocity -= accel * previousDirection / 2;

        //disables decel making player transform go backwards (unlikely scenario)
        if (magnitude != 0)
        {
            if (velocity / Math.Abs(velocity) != previousDirection)
            {
                velocity = 0;
                Debug.Log("whta");
            }
        }

        return velocity;
    }

    // if there is object below transform, return true
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
