using System.Collections;
using UnityEngine;

public class Buff_Instant_SpawnBall : Buff
{
    public override void RemoveEffect() {}
    
    protected override void ApplyEffect(Collider2D collider = null)
    {
        BallManager.Instance.SpawnBallBuff(this.transform.position);
        Destroy(this.gameObject);
    }
}