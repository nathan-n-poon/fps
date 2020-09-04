using System.Collections;
using System.Collections.Generic;
using UnityEngine;  

public class gravityInterface : interactable
{
    gravityController m_gravityController;
    public static Vector3 downAxis;

    void Start()
    {
        m_gravityController = GetComponent<gravityController>();
    }

    public override void interact(Transform otherTransform)
    {
        otherTransform.parent.GetComponent<InputManager>().setController(m_gravityController);
    }
}
