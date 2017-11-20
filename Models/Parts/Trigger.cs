using Sirenix.OdinInspector;
using UnityEngine;


public class Trigger : SerializedMonoBehaviour {

    public interface ISafety
    {
        bool Engaged();
    }

    [ShowInInspector]
    public ISafety safety;

    public delegate void TriggerPull(float amount);
    public event TriggerPull OnTriggerPull;

    protected float amount = 0.0f;

    [ShowInInspector]
    public float Amount { 
        get { return amount; } 
        set { Pull(value); } 
    }

    public void Pull(float amount) {
        if (safety != null && safety.Engaged())
        {
            this.amount = 0.0f;
        } else {
            this.amount = Mathf.Min(1.0f, Mathf.Max(0.0f, amount));
        }
        if (OnTriggerPull != null) OnTriggerPull(this.amount);
    }
}
 