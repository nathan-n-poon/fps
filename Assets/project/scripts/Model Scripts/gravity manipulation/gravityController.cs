using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityController : Controller
{
    public setGravity m_setGravity;

    // Start is called before the first frame update
    void Start()
    {
        m_setGravity = gameObject.GetComponentInChildren<setGravity>();
        this.enabled = false;
        this.m_Camera.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_setGravity.update(m_InputData);
    }
}
