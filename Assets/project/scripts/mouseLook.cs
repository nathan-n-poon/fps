using UnityEngine;
using System.Collections;

public class mouseLook : MonoBehaviour
{
    //Make sure you attach a Rigidbody in the Inspector of this GameObject
    public Rigidbody m_Rigidbody;

    Quaternion deltaRotation;
    float xRotation = 0f;

    public float mouseXSensitivity = 100f;
    public float mouseYSensitivity = 100f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        //Fetch the Rigidbody from the GameObject with this script attached
        //m_Rigidbody = GetComponent<Rigidbody>();

        deltaRotation = new Quaternion();
    }

    private void Update()
    {
        //rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseXSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseYSensitivity * Time.deltaTime;

        deltaRotation = Quaternion.Euler(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //valid--------------------
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * deltaRotation);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //-----------------------------------------------------

    }

    void FixedUpdate()
    {
        
    }
}