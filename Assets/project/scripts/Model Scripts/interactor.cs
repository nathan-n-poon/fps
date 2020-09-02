using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class interactor : MonoBehaviour
{
    BoxCollider m_BoxCollider;

    float maxDuration = 100;
    float currentDuration;

    // Start is called before the first frame update
    void Start()
    {
        m_BoxCollider = GetComponent<BoxCollider>();
        m_BoxCollider.enabled = false;
    }

    // Update is called once per frame
    public void update(InputData m_Inputdata)
    {
        if(m_Inputdata.interactionButtonPressed == 1)
        {
            currentDuration = maxDuration;
        }
        if(m_BoxCollider.enabled)
        {
            currentDuration -= 1;
            currentDuration = Mathf.Max(0, currentDuration);
        }

        m_BoxCollider.enabled = currentDuration == 0 ? false : true;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponentInParent<pickup>().interact(this.transform);
    }
}
