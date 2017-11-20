using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoltExtractor : MonoBehaviour {

    public BoltEjection ejection;
    public BoltCarrier carrier;
    public GameObject round;
    public GameObject casing;

    protected Vector3 roundPosition;
    protected Vector3 casingPosition;

    private void Start()
    {
        if (carrier == null) return;
        if (round != null) {
            roundPosition = round.transform.localPosition;
        }
        if (casing != null) {
            casingPosition = casing.transform.localPosition;
        }
    }

    void Update () {

        if (ejection == null) return;
        switch (ejection.State) {
            case EjectorState.Empty:
                if (round != null) round.SetActive(false);
                if (casing != null) casing.SetActive(false);
                break;
            case EjectorState.Round:
                if (round != null) round.SetActive(true);
                if (casing != null) casing.SetActive(false);
                break;
            case EjectorState.Case:
                if (round != null) round.SetActive(false);
                if (casing != null) casing.SetActive(true);
                break;
        }

        if (carrier == null) return;
        var displacement = carrier.DisplacementVector * carrier.Amount;
        if (round != null) {
            round.transform.localPosition = roundPosition - displacement;
        }
        if (casing != null)
        {
            casing.transform.localPosition = roundPosition - displacement;
        }
    }
}
