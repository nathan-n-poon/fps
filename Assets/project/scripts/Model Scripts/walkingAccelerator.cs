using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkingAccelerator : accelerator
{
    public walkingAccelerator()
    {
        this.walkAccel = 3f;
        this.maxSpeed = 6f;
    }

    public override float move(float previousSpeed, float currentDirection, bool isFloored)
    {
        float newVelocity;
        update(previousSpeed);
        newVelocity = accelerate(currentDirection, previousVelocity, isFloored);
        newVelocity = decelerate(newVelocity);
        return newVelocity;
    }

    public override void update(float previousSpeed)
    {
        accel = walkAccel* Time.deltaTime;
        previousVelocity = previousSpeed;
        previousDirection = Mathf.Sign(previousVelocity);
    }

    public override float accelerate(float currentDirection, float previousVelocity, bool isFloored)
    {
        float newVelocity = previousVelocity;
        this.currentDirection = currentDirection;
        if (currentDirection != 0 && isFloored)
        {
            newVelocity = previousVelocity + accel * currentDirection;
            if (Mathf.Abs(newVelocity) > maxSpeed)
            {
                newVelocity = maxSpeed * currentDirection;
            }
        }
        return newVelocity;
    }

    public override float decelerate(float previousVelocity)
    {
        float newVelocity = previousVelocity;
        //if (GameObject.FindObjectOfType<characterMove>().getIsFloored())
        //{
        if (Mathf.Sign(previousVelocity - accel * previousDirection / 1.5f) == Mathf.Sign(previousVelocity))
        {
            newVelocity -= accel * previousDirection / 1.5f;
        }
        else
            newVelocity = 0;

        return newVelocity;
        //}
    }

}
