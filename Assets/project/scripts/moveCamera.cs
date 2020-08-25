using UnityEngine;
using System.Collections;

public class moveCamera : MonoBehaviour
{
    mouseLook mouseLook;
    headBob headBob;
    Vector3 walkingSpeed;
    Rigidbody m_Rigidbody;
    Camera m_Camera; 

    void Start()
    {
        m_Rigidbody = transform.parent.GetComponent<Rigidbody>();
        m_Camera = GetComponent<Camera>();

        mouseLook = new mouseLook(m_Rigidbody, transform);
        headBob = new headBob(m_Camera, transform);
    }

    private void Update()
    {
        walkingSpeed = GameObject.FindObjectOfType<characterMove>().getWalkingSpeed();
        mouseLook.Update(walkingSpeed);
        headBob.Update(walkingSpeed);
    }


}

class mouseLook
{
    Rigidbody m_Rigidbody;
    Transform transform;
    Quaternion deltaRotation;
    float xRotation = 0f;
    public float mouseXSensitivity = 100f;
    public float mouseYSensitivity = 100f;

    public mouseLook(Rigidbody m_Rigidbody, Transform transform)
    {
        Cursor.lockState = CursorLockMode.Locked;
        this.m_Rigidbody = m_Rigidbody;
        this.transform = transform;
    }

    public void Update(Vector3 walkingSpeed)
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseXSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseYSensitivity * Time.deltaTime;

        deltaRotation = Quaternion.Euler(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * deltaRotation);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        if (deltaRotation != Quaternion.identity)
        {
            GameObject.FindObjectOfType<characterMove>().setWalkingSpeed(0.9f * walkingSpeed);
        }
    }
}

class headBob
{
    Camera m_Camera;
    Transform transform;
    float deltaY;
    float totalTime = 0;

    public headBob(Camera m_Camera, Transform transform)
    {
        this.m_Camera = m_Camera;
        this.transform = transform;
    }
    
    public void Update(Vector3 walkingSpeed)
    {
        totalTime += Time.deltaTime;
        deltaY = walkingSpeed.magnitude * Mathf.Sin(totalTime * 5) / 100;
        transform.localPosition = new Vector3(0, 0.5f + deltaY, 0);
        if (totalTime > float.MaxValue - 1000)
        {
            totalTime = 0;
        }
        Debug.Log(transform.localPosition.y - 0.5);
    }
}