using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TriggerController : MonoBehaviour {

    public VRTK_InteractableObject interactable;
    public Trigger trigger;
    public AnimatedVector3 animator;

    protected VRTK_ControllerEvents events;

    [ShowInInspector]
    public float Rotation {
         get { return trigger.Amount; }
         set { trigger.Amount = value; }
    }

    private void Start()
    {
        if (interactable != null) {
            interactable.InteractableObjectGrabbed += (o, e) =>
            {
                if (events == null)
                {
                    var device = e.interactingObject;
                    var alias = VRTK_DeviceFinder.GetScriptAliasController(device);
                    events = alias.GetComponent<VRTK_ControllerEvents>();
                }
            };

            interactable.InteractableObjectUngrabbed += (o, e) =>
            {
                var device = e.interactingObject;
                var alias = VRTK_DeviceFinder.GetScriptAliasController(device);
                if (events == alias.GetComponent<VRTK_ControllerEvents>())
                {
                    events = null;
                }
            };
        }
    }

    private void Update()
    {
        if (events != null) {
            var triggerAmount = events.GetTriggerAxis();
            if (trigger != null && triggerAmount != trigger.Amount)
            {
                trigger.Amount = triggerAmount;
                if (animator != null) animator.Amount = triggerAmount;
            }
        }
    }
}
