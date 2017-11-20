using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerReleaseSear : MonoBehaviour, Hammer.ISear {

    public Trigger trigger;
    public Hammer hammer;

    protected bool engaged = false;
	
	// Update is called once per frame
	void Start () {
		if (trigger != null) {
            trigger.OnTriggerPull += OnTriggerPull;
        }

        if (hammer != null) {
            hammer.OnHammerStruck += OnHammerStruck;
        }
	}

    void OnTriggerPull(float amount) {
        if (amount == 0.0) engaged = false;
    }

    void OnHammerStruck() {
        engaged = true;
    }

    public bool Engaged() { return enabled && engaged; }

    [ShowInInspector]
    public bool IsEngaged { get { return Engaged(); } }
}
