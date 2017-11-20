using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void RoundConsumedEvent(object sender, float amount);

public interface IRoundProvider
{
    float Count();
    bool Consume(float amount);
    event RoundConsumedEvent OnRoundConsumed;
}

public enum BoltState { Closed, Running, Open }

public delegate void BoltOpened();
public delegate void BoltClosed();
public delegate void BoltLocked();
public delegate void BoltUnlocked();
public delegate void BoltMoved(float amount);

public class Bolt : MonoBehaviour {

    public event BoltOpened OnBoltOpened;
    public event BoltClosed OnBoltClosed;
    public event BoltLocked OnBoltLocked;
    public event BoltLocked OnBoltUnlocked;
    public event BoltMoved OnBoltMoved;

    [ShowInInspector]
    protected float position = 0.0f;
    protected bool locked = false;
    protected TweenerCore<float, float, FloatOptions> tween;

    public TweenerCore<float, float, FloatOptions> Tween {
        get {
            if (tween != null && !tween.IsPlaying())
            {
                tween = null;
            }
            return tween;
        }
    }

    public float Position {
        get { return position; }
        set {
            value = Mathf.Min(1.0f, Mathf.Max(0.0f, value));
            if (value == position) return;
            if (locked) return;
            position = value;
            if (OnBoltMoved != null) OnBoltMoved(position);
            if (position == 0.0f) {
                Debug.Log("Bolt closed.");
                if (OnBoltClosed != null) OnBoltClosed(); 
            } else if (position == 1.0f) { 
                Debug.Log("Bolt opened.");
                if (OnBoltOpened != null) OnBoltOpened(); 
            }
        }
    }

    public BoltState State { 
        get {
            if (position == 0.0f) return BoltState.Closed;
            if (position == 1.0f) return BoltState.Open;
            return BoltState.Running;
        }
    }

    public TweenerCore<float, float, FloatOptions> BaseTween(float position, float speed)
    {
        return DOTween.To(() => this.position, v => this.Position = v, position, speed);
    }

    public TweenerCore<float, float, FloatOptions> OpenTween(float speed)
    {
        return BaseTween(1.0f, speed);
    }

    public TweenerCore<float, float, FloatOptions> CloseTween(float speed)
    {
        return BaseTween(0.0f, speed);
    }

    [Button]
    public TweenerCore<float, float, FloatOptions> Open() {
        if (State == BoltState.Open) return null;
        tween = OpenTween(1.0f);
        return tween;
    }

    [Button]
    public TweenerCore<float, float, FloatOptions> Close() {
        if (State == BoltState.Closed) return null;
        tween = CloseTween(1.0f);
        return tween;
    }

    [Button]
    public void Lock() {
        if (State != BoltState.Open) return;
        locked = true;
        if (OnBoltLocked != null) OnBoltLocked();
    }

    [Button]
    public void Unlock() {
        if (State != BoltState.Open && locked) return;
        locked = false;
        if (OnBoltUnlocked != null) OnBoltUnlocked();
    }
}
