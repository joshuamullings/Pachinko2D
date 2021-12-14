using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreZone : MonoBehaviour
{
    [SerializeField] private Text _zoneText;
    [SerializeField] private int _zoneValue;

    private void Update()
    {
        _zoneText.text = _zoneValue.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Ball") || collider.CompareTag("Extra Ball"))
        {
            UIManager.Instance.Score +=
                _zoneValue * collider.GetComponent<Ball>().PointsMultiplier;
        }
    }
}