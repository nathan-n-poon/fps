using UnityEngine;
using System.Collections;

public class moveCamera : MonoBehaviour
{
    mouseLook mouseLook;
    headBob headBob;
    Vector3 walkingSpeed;
    Rigidbody m_Rigidbody;
    Camera m_Camera;

    characterMove m_characterMove;

    void Start()
    {
        m_characterMove = GetComponentInParent<characterMove>();
        m_Rigidbody = transform.parent.GetComponent<Rigidbody>();
        m_Camera = GetComponent<Camera>();

        mouseLook = new mouseLook(m_Rigidbody, transform, m_characterMove);
        headBob = new headBob(m_Camera, transform);
    }

    public void update(InputData m_InputData)
    {
        Debug.Log(m_characterMove);
        walkingSpeed = m_characterMove.getWalkingSpeed();
        mouseLook.update(walkingSpeed, m_InputData);
        headBob.update(walkingSpeed);
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
    characterMove m_characterMove;

    public mouseLook(Rigidbody m_Rigidbody, Transform transform, characterMove m_characterMove)
    {
        Cursor.lockState = CursorLockMode.Locked;
        this.m_Rigidbody = m_Rigidbody;
        this.transform = transform;
        this.m_characterMove = m_characterMove;
    }

    public void update(Vector3 walkingSpeed, InputData m_InputData)
    {
        if (m_InputData.horizontalLook != 0)
        {
            float mouseX = m_InputData.horizontalLook * mouseXSensitivity * Time.deltaTime;
            deltaRotation = Quaternion.Euler(Vector3.up * mouseX);

            m_Rigidbody.MoveRotation(m_Rigidbody.rotation * deltaRotation);

            if (deltaRotation != Quaternion.identity)
            {
                m_characterMove.setWalkingSpeed(0.9f * walkingSpeed);
            }
        }
        if (m_InputData.verticalLook != 0)
        {
            float mouseY = m_InputData.verticalLook * mouseYSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
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
    
    public void update(Vector3 walkingSpeed)
    {
        totalTime += Time.deltaTime;
        deltaY = walkingSpeed.magnitude * Mathf.Sin(totalTime * 5) / 100;
        transform.localPosition = new Vector3(0, 0.5f + deltaY, 0);
        if (totalTime > float.MaxValue - 1000)
        {
            totalTime = 0;
        }
    }
}