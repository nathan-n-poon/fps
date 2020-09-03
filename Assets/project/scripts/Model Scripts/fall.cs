using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fall : MonoBehaviour
{
    Rigidbody m_Rigidbody;

    Collider Collider;
    ContactPoint previousSurface = new ContactPoint();

    Vector3 downAxis = new Vector3();
    Vector3 effectiveGravity = new Vector3();

    public bool isFloored = false;
    public bool isGrounded = false;
    bool shouldOrient = false;

    public int touchingCount = 0;
    float downAxisSpeed;

    float xDistance;
    float yDistance;
    float zDistance;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponentInChildren<Rigidbody>();
        Collider = GetComponentInChildren<Collider>();
        xDistance = Collider.bounds.extents.x;
        yDistance = Collider.bounds.extents.y;
        zDistance = Collider.bounds.extents.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        downAxis = gravity.downAxis;
        isGrounded = (isFloored && previousSurface.normal == -(downAxis.normalized)) ? true : false;

        calculateGravity();

        if (isGrounded && transform.rotation.x == previousSurface.normal.x)
        {
            downAxisSpeed = 0;
        }

        effectiveGravity = downAxisSpeed * downAxis;

        m_Rigidbody.MovePosition(m_Rigidbody.position + effectiveGravity * Time.deltaTime);
    }

    //set orient flag
    void OnCollisionEnter(Collision other)
    {
        shouldOrient = true;

        isFloored = true;

        touchingCount++;

        int temp = other.contacts.Length - 1;
        if (temp >= 0)
        {
            previousSurface = other.contacts[temp];
        }
    }

    void OnCollisionExit(Collision collision)
    {
        touchingCount--;
        if (touchingCount == 0)
        {
            isFloored = false;
        }
        else
        {
            isFloored = true;
        }
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

    public bool getShouldOrient()
    {
        return shouldOrient;
    }

    public ContactPoint getPreviousSurface()
    {
        return previousSurface;
    }

    public void setShouldOrient(bool shouldOrient)
    {
        this.shouldOrient = shouldOrient;
    }
}
