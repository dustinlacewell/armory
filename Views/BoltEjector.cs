using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltEjector : MonoBehaviour {

    public BoltEjection ejection;
    public BoltExtractor extractor;
    public Transform direction;
    public float directionalForce = 1.0f;
    public Vector3 rotation;
    public float rotationalForce = 1.0f;

    protected void Eject(GameObject original) {
        var copy = Instantiate(original);
        copy.transform.position = original.transform.position;
        copy.transform.rotation = original.transform.rotation;
        var body = copy.GetComponent<Rigidbody>();
        if (body == null) {
            DestroyImmediate(copy);
        }
        body.AddForce(direction.eulerAngles * directionalForce);
        body.AddTorque(rotation * rotationalForce);
        copy.SetActive(true);
    }

    private void Start()
    {
        if (ejection == null) return;
        ejection.OnCaseEjected += () => Eject(extractor.casing);
        ejection.OnRoundEjected += () => Eject(extractor.round);
    }
}
