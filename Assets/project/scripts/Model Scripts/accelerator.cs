using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class accelerator
{
    protected float previousVelocity;
    protected float previousDirection;

    protected float currentDirection;

    protected bool isFloored;

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

        this.currentDirection = currentDirection;
        this.isFloored = isFloored;

        float newVelocity;
        update(previousSpeed);
        if(currentDirection != 0 && additionalAccelerateConditions())
        {
            newVelocity = accelerate(currentDirection, previousVelocity);
        }
        else
        {
            newVelocity = decelerate(previousVelocity);
        }
        return newVelocity;
}

    public void update(float previousSpeed)
    {
        instantaneousAccel = perSecondAccel * Time.deltaTime;
        previousVelocity = previousSpeed;
        previousDirection = Mathf.Sign(previousVelocity);
    }

    public abstract float accelerate(float currentDirection, float previousVelocity);

    public abstract float decelerate(float previousVelocity);

    protected virtual bool additionalAccelerateConditions()
    {
        return true;
    }
}