using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravity : interactable
{

    public Vector3 downAxis;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        downAxis = Physics.gravity;
    }
}
