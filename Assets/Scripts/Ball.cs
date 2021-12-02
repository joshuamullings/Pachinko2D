using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
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
            Destroy(this.gameObject, 0.0f);
            BallManager.Instance.CurrentBalls--;
        }
    }
}