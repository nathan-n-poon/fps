using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class gun : MonoBehaviour
{
    protected float maxCooldown;
    protected float currentCooldown;
    public abstract void shoot();
    public abstract void update(InputData m_InputData);
    protected int slot;

    // Start is called before the first frame update
    void Start()
    {
        
    }    
}
