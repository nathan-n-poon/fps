﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkingController : Controller
{
    // Start is called before the first frame update
    public override void ReadInput(InputData m_InputData)
    {
        newInput = true;
    }

    void LateUpdate()
    {

    }
}
