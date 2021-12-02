using System.Collections;
using UnityEngine;

public class Buff_IncreaseGravity : Buff
{
    public override void RemoveEffect()
    {
        Physics2D.gravity = new Vector2(0.0f, -9.81f);
    }

    protected override void ApplyEffect()
    {
        Physics2D.gravity = new Vector2(0.0f, -20.0f);
    }
}