using System.Collections;
using System.Collections.Generic;
using UnityEngine;  

public class gravityInterface : interactable
{
    gravityController m_gravityController;
    public static Vector3 downAxis;

    void Awake()
    {
        m_gravityController = GetComponentInChildren<gravityController>();
    }

    public override void interact(Transform otherTransform)
    {
        otherTransform.parent.parent.GetComponent<controllerSwitcher>().switchControllers(m_gravityController);
    }
}
