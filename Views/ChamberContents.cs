using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ChamberContents : MonoBehaviour {

    public Chamber chamber;
    public GameObject round;
    public GameObject casing;

	void Update () {
        if (chamber == null) return;
        switch (chamber.State) {
            case ChamberState.Empty:
                if (round != null) round.SetActive(false);
                if (casing != null) casing.SetActive(false);
                break;
            case ChamberState.Round:
                if (round != null) round.SetActive(true);
                if (casing != null) casing.SetActive(false);
                break;
            case ChamberState.Case:
                if (round != null) round.SetActive(false);
                if (casing != null) casing.SetActive(true);
                break;
        }
    }

    private void OnRenderObject()
    {
        Update();
    }
}
