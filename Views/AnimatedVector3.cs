using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class AnimatedVector3 : MonoBehaviour {

    public Transform target;

    protected float amount = 0.0f;

    protected bool HasTarget { get { return target != null; } }

    [ShowIf("HasTarget")]
    public Vector3 closePosition;
    [ShowIf("HasTarget")]
    public Vector3 openPosition;

    [Button]
    [ShowIf("HasTarget")]
    void SetClosePosition()
    {
        closePosition = Value;
    }

    [Button]
    [ShowIf("HasTarget")]
    void SetOpenPosition()
    {
        openPosition = Value;
    }

    public abstract Vector3 Value { get; set; }

    [ShowInInspector]
    [SerializeField]
    public float Amount {
        get { return amount; }
        set { amount = Mathf.Min(1.0f, Mathf.Max(0.0f, value)); Value = CalculateValue(); }
    }

    public Vector3 DisplacementVector {
        get { return closePosition - openPosition; }
    }

    Vector3 CalculateValue()
    {
        return Vector3.Lerp(closePosition, openPosition, amount);
    }

    public TweenerCore<float, float, FloatOptions> BaseTween(float amount, float speed)
    {
        return DOTween.To(() => amount, v => amount = v, amount, speed);
    }

    public TweenerCore<float, float, FloatOptions> OpenTween(float speed)
    {
        return BaseTween(1.0f, speed);
    }

    public TweenerCore<float, float, FloatOptions> CloseTween(float speed)
    {
        return BaseTween(0.0f, speed);
    }

    public Sequence FullTween(float speed)
    {
        var halfSpeed = speed / 2.0f;
        var openTween = OpenTween(halfSpeed);
        var closeTween = CloseTween(halfSpeed);
        return DOTween.Sequence().Append(openTween).Append(closeTween);
    }

    public void Open(float speed)
    {
        OpenTween(speed).Play();
    }

    public void Close(float speed)
    {
        CloseTween(speed).Play();
    }

    public void OpenClose(float speed)
    {
        FullTween(speed).Play();
    }
}
