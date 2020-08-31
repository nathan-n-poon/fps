using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smg : gun
{
    Transform m_Transform;
    void Start()
    {


        m_Transform = transform;
    maxCooldown = 100f;
        currentCooldown = maxCooldown;
    }

    public override void shoot()
    {
        Debug.DrawRay(m_Transform.position, 10 * m_Transform.forward, Color.white, 2);
    }

    // Update is called once per frame
    public override void update(InputData m_InputData)
    {
        currentCooldown -= 1f;
        currentCooldown = Mathf.Max(currentCooldown, 0f);
        //Debug.Log(currentCooldown);

        if ((m_InputData.primaryAttackPressed == 1 ? true : false) && currentCooldown == 0)
        {
            shoot();
            currentCooldown = maxCooldown;
        }
    }
}
