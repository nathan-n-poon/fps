using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    public void interact(Transform otherTransform)
    {
        transform.SetParent(otherTransform.parent);
        transform.localPosition = new Vector3(0.44f, -0.34f, 1.03f);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
