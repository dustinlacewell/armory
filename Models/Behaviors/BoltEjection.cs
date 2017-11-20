using Sirenix.OdinInspector;
using UnityEngine;


public enum EjectorState { Empty, Round, Case }

public class BoltEjection : MonoBehaviour {
    public delegate void CaseEjected();
    public delegate void RoundEjected();
    public delegate void StateChanged(EjectorState state);

    public event CaseEjected OnCaseEjected;
    public event RoundEjected OnRoundEjected;
    public event StateChanged OnStateChanged;

    public Bolt bolt;
    public Chamber chamber;

    protected EjectorState state = EjectorState.Empty;
    protected BoltState previousBoltState;
    
    [ShowInInspector]
    public EjectorState State {
        get { return state; }
        set {
            if (value != state && OnStateChanged != null) {
                OnStateChanged(value);
            }
            state = value;
        }
    }

    void Start () {
        if (bolt != null) {
            previousBoltState = bolt.State;
            bolt.OnBoltMoved += OnBoltMoved;
            bolt.OnBoltOpened += OnBoltOpened;
            bolt.OnBoltClosed += OnBoltClosed;
        }
	}

    void OnBoltMoved(float position) {
        if (chamber == null) return;
        if (previousBoltState == BoltState.Closed)
        {
            switch (chamber.Clear())
            {
                case ChamberState.Empty:
                    State = EjectorState.Empty;
                    break;
                case ChamberState.Round:
                    State = EjectorState.Round;
                    break;
                case ChamberState.Case:
                    State = EjectorState.Case;
                    break;
            }
        }
        previousBoltState = BoltState.Running;
    }

    void OnBoltOpened() {
        switch (state)
        {
            case EjectorState.Empty: break;
            case EjectorState.Round:
                Debug.Log("Round ejected.");
                if (OnRoundEjected != null) OnRoundEjected();
                break;
            case EjectorState.Case:
                Debug.Log("Case ejected.");
                if (OnCaseEjected != null) OnCaseEjected();
                break;
        }
        State = EjectorState.Empty;
        previousBoltState = BoltState.Open;
    }

    void OnBoltClosed() {
        if (chamber != null)
        {
            switch (state)
            {
                case EjectorState.Empty: break;
                case EjectorState.Round:
                    chamber.State = ChamberState.Round;
                    break;
                case EjectorState.Case:
                    chamber.State = ChamberState.Case;
                    break;
            }
            State = EjectorState.Empty;
        }
        previousBoltState = BoltState.Closed;
    }
}
