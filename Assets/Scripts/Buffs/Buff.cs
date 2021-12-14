using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private bool _timed;

    public int ID { get; set; }
    public float Duration { get => _duration; set => _duration = value; }
    public bool Timed { get => _timed; set => _timed = value; }

    public abstract void RemoveEffect();
    
    protected abstract void ApplyEffect(Collider2D collider = null);

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Ball") || collider.CompareTag("Extra Ball"))
        {
            EventManager.Instance.BuffCollected(this);
            ApplyEffect(collider);
        }
    }
}