using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSear : MonoBehaviour, Hammer.ISear {

    public Trigger trigger;
    public float threshold = 1.0f;

    [ShowInInspector]
    public bool IsEngaged {
        get { return Engaged(); }
    }

    public bool Engaged() { 
        return enabled && (trigger.Amount < threshold); 
    }
}
