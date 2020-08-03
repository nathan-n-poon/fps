using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class characterMove : MonoBehaviour
{
    public Rigidbody m_Rigidbody;
    public Collider Collider;

    public float maxSpeed = 4f;
    public float walkAccel = 1f;
    public float gravity = -10f;
    public float skinDepth = 0.3f;


    Vector3 speed;
    float speedX;
    float speedY;
    float speedZ;
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        //
        //
        //
    }

    // Update is called once per frame
    void Update()
    {
        //x = Input.GetAxis("Horizontal");
        //z = Input.GetAxis("Vertical");
        //Vector3 move = transform.right * x + transform.forward * z;

        //isGrounded = checkGround();
        ////deltaY.y += gravity * Time.deltaTime;
        //if (isGrounded)
        //{
        //    deltaY.y = 0;
        //}

        //m_Rigidbody.MovePosition(m_Rigidbody.position + (move + deltaY) * Speed * Time.deltaTime);

        //--------------------------------------------------------------------------------------------------


        //dx = (v + a/2*t) * t
        Vector3 speed = Vector3.zero;

        speedX = accelerate("Horizontal");
        //Debug.Log("speedX is: ");
        Debug.Log(speedX);
        speedZ = accelerate("Vertical");

        checkCollisions();

        speed = (transform.right * speedX + transform.forward * speedZ).normalized;

        m_Rigidbody.MovePosition(m_Rigidbody.position + (speed) * Time.deltaTime);

    }

    void FixedUpdate()
    {

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

        if (currentDirection != 0)
        {
            velocity += accel * currentDirection;
            if(Mathf.Abs(velocity) > maxSpeed)
            {
                velocity = maxSpeed * currentDirection;
            }
        }
        else
        {
            velocity -= accel * previousDirection;
            if(magnitude != 0)
            {
                if(velocity / Math.Abs(velocity) != previousDirection )
                {
                    velocity = 0;
                }
            }
        }
        //if (Input.GetAxisRaw(axis) != 0)
        //{
        //    magnitude *= Input.GetAxisRaw(axis);
        //}
        //if (velocity != 0)
        //{
        //    magnitude *= previousDirection;
        //}
        return velocity;
    }

    void checkCollisions()
    {
        //float DisstanceToTheGround = Collider.bounds.extents.y;   
        //return Physics.Raycast(transform.position, Vector3.down, DisstanceToTheGround + 0.1f);
        float xDistance = Collider.bounds.extents.x + skinDepth;
        float yDistance = Collider.bounds.extents.y + skinDepth;
        float zDistance = Collider.bounds.extents.z + skinDepth;
        if (Physics.Raycast(transform.position, Vector3.right, xDistance) || Physics.Raycast(transform.position, Vector3.left, xDistance))
        {
            Debug.Log("Forwards/Backwards");
            speedX = 0;
        }
        if (Physics.Raycast(transform.position, Vector3.down, yDistance) || Physics.Raycast(transform.position, Vector3.up, yDistance))
        {
            Debug.Log("Top/Bottom");
            speedY = 0;
        }
        if (Physics.Raycast(transform.position, Vector3.forward, zDistance) || Physics.Raycast(transform.position, Vector3.back, zDistance))
        {
            Debug.Log("Right/Left");
            speedZ = 0;
        }
    }
}
