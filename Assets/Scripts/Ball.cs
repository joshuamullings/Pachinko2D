using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] public int PointsMultiplier = 1;

    [SerializeField] private float _impluseForce = 10.0f;

    private void Awake()
    {
        GetComponent<Rigidbody2D>().AddForce(
            new Vector2(
                Random.Range(-_impluseForce, _impluseForce),
                0.0f
            )
        );
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Kill Zone"))
        {
            if (this.CompareTag("Extra Ball"))
            {
                Destroy(this.gameObject, 0.0f);
            }
            else
            {
                Destroy(this.gameObject, 0.0f);
                BallManager.Instance.CurrentBalls--;
            }
        }
    }
}