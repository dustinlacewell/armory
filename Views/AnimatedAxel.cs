using System;
using UnityEngine;

public class AnimatedAxel : AnimatedVector3
{
    public override Vector3 Value {
        get {
            return target.localEulerAngles;
        }

        set {
            target.localEulerAngles = value;
        }
    }
}
