using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltLoading : SerializedMonoBehaviour
{

    public Bolt bolt;
    public Chamber chamber;
    [ShowInInspector]
    public IRoundProvider roundProvider;

    void Start()
    {
        if (bolt != null)
        {
            bolt.OnBoltClosed += () =>
            {
                if (roundProvider.Consume(1))
                {
                    chamber.State = ChamberState.Round;
                }
            };
        }
    }
}

        