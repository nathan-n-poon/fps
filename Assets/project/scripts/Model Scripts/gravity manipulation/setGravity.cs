using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setGravity : MonoBehaviour
{
    InputData m_InputData;
    Vector3 speed;

    accelerator xAccelerator;
    accelerator yAccelerator;
    accelerator zAccelerator;

    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(0, 0, 0);

        xAccelerator = new setManipAccelerator();
        yAccelerator = new setManipAccelerator();
        zAccelerator = new setManipAccelerator();
    }

    // Update is called once per frame
    public void update(InputData m_InputData)
    {
        this.m_InputData = m_InputData;

        calculateSpeed();

        speed *= Time.deltaTime;

        transform.localPosition = new Vector3(Mathf.Min(transform.localPosition.x + speed.x, 4.5f), Mathf.Min(transform.localPosition.y + speed.y, 4.5f), Mathf.Min(transform.localPosition.z + speed.z, 4.5f));
    }//

    void calculateSpeed()
    {
        speed.x = xAccelerator.move(0, m_InputData.horizontalMove, false);
        speed.y = yAccelerator.move(0, m_InputData.mouseButton, false);
        speed.z = zAccelerator.move(0, m_InputData.verticalMove, false);
    }
}
