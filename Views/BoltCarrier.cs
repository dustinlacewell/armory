using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BoltCarrier : AnimatedSlide {

    public Bolt bolt;

    private void Start()
    {
         if (bolt != null) 
         {
            bolt.OnBoltMoved += (position) => Amount = position;
         }
    }
}
