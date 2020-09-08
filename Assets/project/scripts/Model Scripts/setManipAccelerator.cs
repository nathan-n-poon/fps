using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setManipAccelerator : accelerator
{
    public setManipAccelerator()
    {
        this.maxSpeed = 0.2f;
    }

    public override float accelerate(float currentDirection, float previousVelocity)
    {
        return maxSpeed * currentDirection;
    }

    public override float decelerate(float previousVelocity)
    {
        return 0f;
    }
}
