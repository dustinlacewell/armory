using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour {

    public float maxRounds;
    public float currentRounds;

    public float Count()
    {
        return currentRounds;
    }

    public bool Consume(float amount)
    {
        if (currentRounds > amount) {
            currentRounds -= amount;
            return true;
        }
        return false;
    }
}
