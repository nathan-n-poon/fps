using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class realisticFall : fall
{
    public override bool stopFall()
    {
        return( base.stopFall() && transform.rotation.x == previousSurface.normal.x);
    }
}
