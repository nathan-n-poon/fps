using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerSwitcher : MonoBehaviour
{
    InputManager m_InputManager;
    Controller oldController;
    Controller newController;

    void Start()
    {
        m_InputManager = gameObject.GetComponent<InputManager>();
        oldController = gameObject.GetComponent<Controller>();
    }

    public void switchControllers(Controller newController)
    {
        oldController.disableCamera();
        m_InputManager.setController(newController);
        newController.enableCamera();

        this.newController = newController;
    }

    public void switchBackControllers()
    {
        this.newController.disableCamera();
        m_InputManager.setController(oldController);
        oldController.enableCamera();
    }

}
