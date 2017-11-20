using Sirenix.OdinInspector;
using UnityEngine;

public enum ChamberState { Empty, Round, Case }


public class Chamber : MonoBehaviour, Hammer.IFireable {

    public delegate void ShotFired();
    public delegate void StateChanged(ChamberState state);

    public event ShotFired OnShotFired;
    public event StateChanged OnStateChanged;

    protected ChamberState state = ChamberState.Empty;

    [ShowInInspector]
    public ChamberState State {
        get { return state; }
        set {
            if (value != state && OnStateChanged != null) 
               OnStateChanged(value);
            state = value;
        }
    }

    [Button]
    public bool Fire() {
        switch (state) {
            case ChamberState.Round:
                State = ChamberState.Case;
                if (OnShotFired != null) OnShotFired();
                Debug.Log("Shot fired.");
                return true;
            default: return false;
        }
    }

    [Button]
    public ChamberState Clear() {
        var returnedState = state;
        State = ChamberState.Empty;
        return returnedState;
    }
}
