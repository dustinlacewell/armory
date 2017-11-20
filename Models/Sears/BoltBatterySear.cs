using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltBatterySear : MonoBehaviour, Hammer.ISear {

    public Bolt bolt;
    public float threshold = 1.0f;

    public bool Engaged() { return bolt.Position != 0.0f; }

    [ShowInInspector]
    public bool IsEngaged { get { return Engaged(); } }
}
