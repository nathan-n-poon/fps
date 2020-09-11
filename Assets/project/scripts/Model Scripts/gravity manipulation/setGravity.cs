using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setGravity : MonoBehaviour
{
    InputData m_InputData;
    Vector3 speed;
    Vector3 baseOffset;

    float bounds = 0.3f;
    float boundsScale;

    accelerator xAccelerator;
    accelerator yAccelerator;
    accelerator zAccelerator;

    // Start is called before the first frame update
    void Start()
    {
        boundsScale = gravity.maxGs / bounds;
        transform.localPosition = new Vector3(0, 0, 0);
        baseOffset = transform.localPosition;

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

        if(speed != Vector3.zero)
        {
            transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x + speed.x, -bounds, bounds),
                                                  Mathf.Clamp(transform.localPosition.y + speed.y, -bounds, bounds), 
                                                  Mathf.Clamp(transform.localPosition.z + speed.z, -bounds, bounds));
            gravity.downAxis = boundsScale * (transform.localPosition - baseOffset);
        }        
    }//

    void calculateSpeed()
    {
        speed.x = xAccelerator.move(0, m_InputData.horizontalMove, false);
        speed.y = yAccelerator.move(0, m_InputData.mouseButton, false);
        speed.z = zAccelerator.move(0, m_InputData.verticalMove, false);

        Vector3 temp = speed.x * transform.right + speed.y * transform.up + speed.z * transform.forward;
        speed = temp / boundsScale * 2;
    }
}
