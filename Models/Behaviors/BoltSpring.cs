using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BoltSpring : MonoBehaviour {

    public Bolt bolt;
    public VRTK_InteractableObject interactable;

    private void Update()
    {
        if (bolt == null) return;
        if (bolt.Tween != null) return;
        if (bolt.State == BoltState.Closed) return;
        if (interactable != null && interactable.IsGrabbed()) return;
        bolt.Close();
    }
}
