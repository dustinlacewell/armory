using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class SlideHandle : MonoBehaviour {
    public Bolt bolt;
    public AnimatedSlide slide;
    public VRTK_InteractableObject interactable;
    protected Vector3 originalPosition;

    protected void Start() {
        originalPosition = interactable.transform.localPosition;
        interactable.InteractableObjectUngrabbed += OnRelease;
    }

    protected void OnRelease(object o, InteractableObjectEventArgs e) {
        interactable.transform.localPosition = originalPosition;
    }

    protected Vector3 ClosestPointOnLine(Vector3 vA, Vector3 vB, Vector3 vPoint)
    {
        var vVector1 = vPoint - vA;
        var vVector2 = (vB - vA).normalized;

        var d = Vector3.Distance(vA, vB);
        var t = Vector3.Dot(vVector2, vVector1);

        if (t <= 0)
            return vA;

        if (t >= d)
            return vB;

        var vVector3 = vVector2 * t;

        var vClosestPoint = vA + vVector3;

        return vClosestPoint;
    }

    [ShowInInspector]
    public float Position {
        get { return bolt.Position; }
        set { bolt.Position = value; }
    }

    private void FixedUpdate()
    {
        if (!interactable.IsGrabbed()) return;
        var originalLocalPosition = originalPosition;
        var localPosition = slide.transform.parent.InverseTransformPoint(interactable.transform.position);
        var displacement = slide.DisplacementVector;
        var derivedPosition = ClosestPointOnLine(originalLocalPosition, originalLocalPosition - displacement, localPosition);
        var mainDistance = Vector3.Distance(slide.closePosition, slide.openPosition);
        var currentDistance = Vector3.Distance(originalLocalPosition, derivedPosition);
        Position = currentDistance / mainDistance;
    }
}
