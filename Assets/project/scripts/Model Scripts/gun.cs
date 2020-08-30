using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class gun : Controller
{
    protected float maxCooldown;
    protected float currentCooldown;
    public abstract void shoot();
    public abstract void update(InputData m_InputData);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.update(m_InputData);
    }

    
}
