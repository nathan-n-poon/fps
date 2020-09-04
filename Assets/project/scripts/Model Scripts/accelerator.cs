using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class accelerator
{
    protected float previousVelocity;
    protected float previousDirection;

    protected float currentDirection;

    protected float walkAccel;
    protected float maxSpeed;
    protected float accel;

    public abstract float move(float previousSpeed, float currentDirection, bool isFloored);

    public abstract void update(float previousSpeed);

    public abstract float accelerate(float currentDirection, float previousVelocity, bool isFloored);

    public abstract float decelerate(float previousVelocity);
}