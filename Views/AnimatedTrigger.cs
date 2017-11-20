using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AnimatedTrigger : AnimatedAxel {

    public Trigger trigger;

	void Start () {
        if (trigger != null)
        {
            trigger.OnTriggerPull += (a) => Amount = a;
        }
	}
}
