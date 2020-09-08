using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkingAccelerator : accelerator
{
    public walkingAccelerator()
    {
        this.perSecondAccel = 3f;
        this.maxSpeed = 6f;
        this.decelerationFactor = 2f;
    }

    public override float accelerate(float currentDirection, float previousVelocity)
    {
        float newVelocity = previousVelocity;
  
        newVelocity = previousVelocity + instantaneousAccel * currentDirection;
        if (Mathf.Abs(newVelocity) > maxSpeed)
        {
            newVelocity = maxSpeed * currentDirection;
        }
        return newVelocity;
    }

    public override float decelerate(float previousVelocity)
    {
        float newVelocity = previousVelocity;
        //if (GameObject.FindObjectOfType<characterMove>().getIsFloored())
        //{
        if (Mathf.Sign(previousVelocity - instantaneousAccel * previousDirection * decelerationFactor) == Mathf.Sign(previousVelocity))
        {
            newVelocity -= instantaneousAccel * previousDirection * decelerationFactor;
        }
        else
            newVelocity = 0;

        return newVelocity;
        //}
    }

    protected override bool additionalAccelerateConditions()
    {
        return this.isFloored;
    }

}
