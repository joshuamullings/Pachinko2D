using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Buff : MonoBehaviour
{
    [SerializeField] protected float _buffDuration;

    public int BuffID { get; set; }

    public abstract void RemoveEffect();

    protected abstract void ApplyEffect();
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Ball"))
        {
            ApplyEffect();
            BuffManager.Instance.ActiveBuffsList[BuffID].ActivateBuff(_buffDuration);
            Destroy(this.gameObject);
        }
    }
}