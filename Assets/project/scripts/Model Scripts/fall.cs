using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fall : MonoBehaviour
{
    protected Rigidbody m_Rigidbody;

    protected Collider Collider;
    protected ContactPoint previousSurface = new ContactPoint();

    protected Vector3 downAxis = new Vector3();
    public Vector3 effectiveGravity = new Vector3();

    protected bool isFloored = false;
    protected bool isGrounded = false;
    protected bool shouldOrient = false;

    protected int touchingCount = 0;
    protected float downAxisSpeed;

    protected float xDistance;
    protected float yDistance;
    protected float zDistance;

    // Start is called before the first frame update
    protected void Start()
    {
        m_Rigidbody = GetComponentInChildren<Rigidbody>();
        Collider = GetComponentInChildren<Collider>();
        xDistance = Collider.bounds.extents.x;
        yDistance = Collider.bounds.extents.y;
        zDistance = Collider.bounds.extents.z;
    }

    // Update is called once per frame
    protected void FixedUpdate()
    {
        downAxis = gravity.downAxis;
        isGrounded = (isFloored && previousSurface.normal == -(downAxis.normalized)) ? true : false;

        calculateGravity();

        if (stopFall())
        {
            downAxisSpeed = 0;
        }

        effectiveGravity = downAxisSpeed * downAxis;

        m_Rigidbody.MovePosition(m_Rigidbody.position + effectiveGravity * Time.deltaTime);
    }

    //set orient flag
    protected void OnCollisionEnter(Collision other)
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

    protected void OnCollisionExit(Collision collision)
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


    protected void calculateGravity()
    {
        downAxisSpeed += downAxis.magnitude * Time.deltaTime;

        //are we on slope relative to gravity?
        //effectiveGravity = transform.InverseTransformDirection(downAxis);
        //checkCollisions(ref effectiveGravity);
        //if (isFloored && effectiveGravity.magnitude < Mathf.Abs(gravity) / 1.25)
        //{
        //    downAxisSpeed *= 0.5f;
        //    Debug.Log(effectiveGravity.magnitude);
        //}
    }


    //set speed of transform in direction of object to zero
    protected void checkCollisions(ref Vector3 otherSpeed)
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

    public virtual bool stopFall()
    {
        return isGrounded;
    }
}