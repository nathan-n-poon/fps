using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class accelerator
{
    protected float previousVelocity;
    protected float previousDirection;

    protected float currentDirection;

    protected float maxSpeed;
    protected float perSecondAccel;
    protected float decelerationFactor;
    protected float instantaneousAccel;

    public virtual float move(float previousSpeed, float currentDirection, bool isFloored)
    {
        if(previousSpeed == 0 && currentDirection == 0)
        {
            return 0;
        }

        float newVelocity = previousVelocity;
        update(previousSpeed);
        if(currentDirection != 0)
        {
            newVelocity = accelerate(currentDirection, previousVelocity, isFloored);
        }
        else
        {
            newVelocity = decelerate(newVelocity);
        }
        return newVelocity;
}

    public void update(float previousSpeed)
    {
        instantaneousAccel = perSecondAccel * Time.deltaTime;
        previousVelocity = previousSpeed;
        previousDirection = Mathf.Sign(previousVelocity);
    }

    public abstract float accelerate(float currentDirection, float previousVelocity, bool isFloored);

    public abstract float decelerate(float previousVelocity);
}