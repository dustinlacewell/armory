using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltCocking : MonoBehaviour {

    public Bolt bolt;
    public Hammer hammer;

	void Start () {
        if (bolt != null) {
            bolt.OnBoltOpened += () =>
            {
                if (hammer != null)
                {
                    hammer.Cock();
                }
            };
        }
	}
}
