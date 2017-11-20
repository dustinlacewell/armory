using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class MagazineRoundProvider : MonoBehaviour, IRoundProvider {

    public event RoundConsumedEvent OnRoundConsumed;

    public VRTK_SnapDropZone dropzone;

    protected Magazine mag;

    void Start ()
    {
        if (dropzone != null)
        {
            dropzone.ObjectSnappedToDropZone += (object sender, SnapDropZoneEventArgs e) => {
                mag = e.snappedObject.GetComponent<Magazine>();
            };

            dropzone.ObjectUnsnappedFromDropZone += (object sender, SnapDropZoneEventArgs e) => {
                mag = null;
            };
        }
    }

    protected Magazine Magazine {
        get {
            var snappedObject = dropzone.GetCurrentSnappedObject();
            if (snappedObject != null) return snappedObject.GetComponent<Magazine>();
            return null;
        }
    }

    public float Count()
    {
        if (mag == null) return 0;
        return mag.Count();
    }

    public bool Consume(float amount)
    {
        if (mag == null) return false;
        if (mag.Consume(amount))
        {
            if (OnRoundConsumed != null) OnRoundConsumed(this, amount);
            return true;
        };
        return false;
    }
}
