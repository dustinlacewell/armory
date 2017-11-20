using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltLocking : MonoBehaviour {

    public Bolt bolt;
    [ShowInInspector]
    public IRoundProvider roundProvider;

	void Start () {
        if (bolt != null) {
            bolt.OnBoltOpened += () =>
            {
                 if (roundProvider != null && roundProvider.Count() == 0) {
                    bolt.Lock();
                 }
            };
        }
	}
}
