using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;
public class characterMove : MonoBehaviour
{
    //character objects
    public Rigidbody m_Rigidbody;
    public Collider Collider;

    //constants
    public float gravity = 10f;
    public float skinDepth = 0.3f;
    public Vector3 downAxis = new Vector3(0,-1,0);

    //movement
    Vector3 relativeDownAxis = new Vector3();
    Vector3 relativeSpeed = new Vector3();
    Vector3 walkingSpeed = Vector3.zero;
    Vector3 effectiveGravity = new Vector3();
    Vector3 speed;
    float downAxisSpeed;

    accelerator xAccelerator;
    accelerator zAccelerator;

    //contact
    bool shouldOrient = false;
    ContactPoint contact = new ContactPoint();
    Collision previousSurface = new Collision();

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        relativeDownAxis = transform.InverseTransformDirection(downAxis).normalized;

        xAccelerator = new accelerator( "Horizontal");
        zAccelerator = new accelerator("Vertical");
    }

    // Update is called once per frame
    void Update()
    {

        orientSelf();
        
        //get values for speed in  x z plane
        getWalkSpeeds();

        //get current downAxis, transfrom into vector relative to player transform, and add gravity's speed in that direction 
        calculateGravity();

        speed = walkingSpeed + effectiveGravity;

        //halt movement, relative to player transform, in any direction where there is object 
        checkCollisions(ref speed);

        m_Rigidbody.MovePosition(m_Rigidbody.position + (speed) * Time.deltaTime);

    }

    //set orient flag
    void OnCollisionEnter(Collision other)
    {
        shouldOrient = true;
        
        previousSurface = other;
    }

    void orientSelf()
    {
        relativeDownAxis = transform.InverseTransformDirection(downAxis);
        //orient oursleves to normal of current surface, and don't reorient until next contact
        if (shouldOrient)
        {
            downAxisSpeed = 0;
            //print("Points colliding: " + other.contacts.Length);
            //print("First normal of the point that collide: " + other.contacts[0].point);
            contact = previousSurface.GetContact(0);
            transform.position = contact.normal + contact.point;
            transform.rotation = Quaternion.FromToRotation(transform.up, contact.normal) * transform.rotation;
            relativeDownAxis = transform.InverseTransformDirection(downAxis).normalized;
            shouldOrient = false;
            relativeSpeed = transform.InverseTransformDirection(speed);
            checkCollisions(ref relativeSpeed);

            //relativeSpeed = transform.TransformDirection(speed);
        }
    }

    void getWalkSpeeds()
    {
        xAccelerator.update(walkingSpeed.x);
        zAccelerator.update(walkingSpeed.z);

        xAccelerator.accelerate();
        zAccelerator.accelerate();

        xAccelerator.decelerate();
        zAccelerator.decelerate();

        walkingSpeed.x = xAccelerator.finalVelocity();
        walkingSpeed.z = zAccelerator.finalVelocity();

        //normalise x z movement if necessary and point them in their appropriate global direction

        Debug.Log(walkingSpeed.x); 
        walkingSpeed = (transform.right * walkingSpeed.x + transform.forward * walkingSpeed.z) ;

    }

    void calculateGravity()
    {
        downAxisSpeed += gravity * Time.deltaTime;

        //are we on sope relative to gravity?
        effectiveGravity = relativeDownAxis * gravity;
        checkCollisions(ref effectiveGravity);
        if (isGrounded() && effectiveGravity.magnitude < Mathf.Abs(gravity) / 1.5)
        {
            //Debug.Log("nani");
            downAxisSpeed *= 0.5f;
        }

        else if (isGrounded())
        {
            downAxisSpeed *= 0.8f;
        }
        if (isGrounded() && transform.up == downAxis)
        {
            downAxisSpeed = 0;
        }

        effectiveGravity = downAxisSpeed * relativeDownAxis;
        effectiveGravity = transform.right * effectiveGravity.x + transform.up * effectiveGravity.y + transform.forward * effectiveGravity.z;
    }

    //set speed of transform in direction of object to zero
    void checkCollisions(ref Vector3 otherSpeed)
    {
        float xDistance = Collider.bounds.extents.x + skinDepth;
        float yDistance = Collider.bounds.extents.y + skinDepth;
        float zDistance = Collider.bounds.extents.z + skinDepth;

        if (Physics.Raycast(transform.position, transform.right, xDistance))
        {
            otherSpeed.x = Mathf.Min(0, otherSpeed.x);
            Debug.Log("wrong");
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

    // if there is object below transform, return true
    public bool isGrounded()
    {
        bool isGrounded = false;
        float yDistance = Collider.bounds.extents.y + skinDepth;

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

class accelerator
{
    Vector3 speed;
    int speedComponent;
    string inputAxis;

    float previousVelocity;
    float previousDirection;

    float currentDirection;

    float walkAccel = 1f;
    public float maxSpeed = 6f;
    float accel;

    float newVelocity;


    public accelerator(string inputAxis)
    {
        this.inputAxis = inputAxis;
    }

    public void update(float previousSpeed)
    {

        accel = walkAccel * Time.deltaTime;
        previousVelocity = previousSpeed;
        previousDirection = Mathf.Sign(previousVelocity);
    }

    public void accelerate()
    {
        currentDirection = Input.GetAxisRaw(inputAxis);
        if (currentDirection != 0 && GameObject.FindObjectOfType<characterMove>().isGrounded())
        {
            newVelocity = previousVelocity +  accel * currentDirection;
            if(Mathf.Abs(newVelocity) > maxSpeed)
            {
                newVelocity = maxSpeed * currentDirection;
            }
        }
    }

    public void decelerate()
    {
        if (Mathf.Sign(newVelocity - accel * previousDirection / 2) == Mathf.Sign(newVelocity))
            newVelocity -= accel * previousDirection / 2;
        else
            newVelocity = 0;
    }

    public float finalVelocity ()
    {
        return newVelocity;
    }
}