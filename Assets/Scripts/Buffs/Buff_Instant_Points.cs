using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Buff_Instant_Points : Buff
{
    [SerializeField] private Color _buffColour;

    public override void RemoveEffect() {}

    protected override void ApplyEffect(Collider2D collider)
    {
        SpriteRenderer spriteRenderer = collider.GetComponent<SpriteRenderer>();
        Ball ball = collider.GetComponent<Ball>();
        Text text = collider.GetComponentInChildren<Text>();
        ParticleSystem particleSystem = collider.GetComponentInChildren<ParticleSystem>();
        ParticleSystem.EmissionModule particleSystemEmission = collider.GetComponentInChildren<ParticleSystem>().emission;

        spriteRenderer.color = _buffColour;
        ball.PointsMultiplier += 1;
        text.enabled = true;
        text.text = collider.GetComponent<Ball>().PointsMultiplier.ToString() + "x";
        particleSystem.Play();
        particleSystemEmission.rateOverTime = particleSystemEmission.rateOverTime.constant + 20.0f;

        Destroy(this.gameObject);
    }
}