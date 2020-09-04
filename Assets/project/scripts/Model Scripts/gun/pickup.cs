using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : interactable
{
    fall m_fall;
    Rigidbody m_Rigidbody;

    public override void interact(Transform otherTransform)
    {
        m_fall.enabled = false;
        m_Rigidbody.isKinematic = true;
        transform.SetParent(otherTransform.parent);
        transform.localPosition = new Vector3(0.44f, -0.34f, 1.03f);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_fall = GetComponent<fall>();
        m_Rigidbody = GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
