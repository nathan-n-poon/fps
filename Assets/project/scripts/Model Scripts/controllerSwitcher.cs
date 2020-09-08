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
        Debug.Log(oldController);
    }

    public void switchControllers(Controller newController)
    {

        oldController.disableCamera();
        oldController.enabled = false;

        newController.enabled = true;
        newController.enableCamera();

        m_InputManager.setController(newController);
        this.newController = newController;
    }

    public void switchBackControllers()
    {
        newController.disableCamera();
        newController.enabled = false;

        oldController.enabled = true;
        oldController.enableCamera();

        m_InputManager.setController(oldController);
    }

}
