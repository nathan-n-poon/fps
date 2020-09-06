using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setManipAccelerator : accelerator
{
    public setManipAccelerator()
    {

    }

    public override float move(float previousSpeed, float currentDirection, bool isFloored)
    {
        throw new System.NotImplementedException();
    }

    public override float accelerate(float currentDirection, float previousVelocity)
    {
        throw new System.NotImplementedException();
    }

    public override float decelerate(float previousVelocity)
    {
        throw new System.NotImplementedException();
    }
}
