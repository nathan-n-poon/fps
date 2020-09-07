using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityController : Controller
{
    setGravity m_setGravity;

    // Start is called before the first frame update
    void Start()
    {
        this.disableCamera();
        m_setGravity = gameObject.GetComponent<setGravity>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_setGravity.update(m_InputData);
    }
}
