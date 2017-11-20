using UnityEngine;

public class AnimatedSlide : AnimatedVector3
{
    public override Vector3 Value {
        get {
            return target.localPosition;
        }

        set {
            target.localPosition = value;
        }
    }
}
