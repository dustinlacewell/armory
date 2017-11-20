using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : SerializedMonoBehaviour {

    public delegate void HammerCocked();
    public delegate void HammerUncocked();
    public delegate void HammerStruck();

    public interface ISear
    {
        bool Engaged();
    }

    public interface IFireable {
        bool Fire();
    }

    [ShowInInspector]
    public IFireable chamber;

    [ShowInInspector]
    public List<ISear> sears = new List<ISear>();

    public event HammerCocked OnHammerCocked;
    public event HammerUncocked OnHammerUncocked;
    public event HammerStruck OnHammerStruck;

    protected bool cocked = false;

    [ShowInInspector]
    public bool Cocked { get { return cocked; } }

    [Button]
    public void Cock() {
        cocked = true;
        Debug.Log("Hammer Cocked.");
        if (OnHammerCocked != null) OnHammerCocked();
    }

    [Button]
    public void Decock() {
        cocked = false;
        Debug.Log("Hammer DeCocked.");
    }

    [Button]
    protected void Trip() {
        cocked = false;
        Debug.Log("Hammer Tripped.");
        if (chamber != null) chamber.Fire();
        if (OnHammerStruck != null) OnHammerStruck();
    }

    [ShowInInspector]
    public bool CanStrike {
        get {
            if (!cocked) return false;
            foreach (var sear in sears)
            {
                if (sear.Engaged()) return false;
            }
            return true;
        }
    }

    public void Update()
    {
        if (CanStrike) Trip();
    }
}
