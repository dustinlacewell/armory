using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltBlowback : MonoBehaviour {

    public Bolt bolt;
    public Chamber chamber;

	void Start () {
        if (chamber != null) {
            chamber.OnShotFired += () =>
            {
                if (bolt != null) bolt.Open();
            };
        }
	}
}
