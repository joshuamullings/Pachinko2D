using System.Collections;
using UnityEngine;

public class Buff_Timed_IncreaseGravity : Buff
{
    public override void RemoveEffect()
    {
        Physics2D.gravity = new Vector2(0.0f, -9.81f);
        Destroy(this.gameObject);
    }

    protected override void ApplyEffect(Collider2D collider = null)
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<CircleCollider2D>().enabled = false;
        Physics2D.gravity = new Vector2(0.0f, -20.0f);
    }
}