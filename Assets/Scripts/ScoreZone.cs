using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreZone : MonoBehaviour
{
    [SerializeField] private Text _zoneText;
    [SerializeField] private int _zoneAmount;

    private void Update()
    {
        double zoneValue = ZoneValue();

        if (zoneValue < 1000)
        {
            _zoneText.text = zoneValue.ToString();
        }
        else
        {
            double exponent = System.Math.Floor(System.Math.Log10(System.Math.Abs(zoneValue)));
            double mantissa = (zoneValue / System.Math.Pow(10, exponent));
            _zoneText.text = mantissa.ToString("0.0") + "e" + exponent.ToString();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Ball") || collider.CompareTag("Extra Ball"))
        {
            UIManager.Instance.Score +=
                ZoneValue() * collider.GetComponent<Ball>().PointsMultiplier;
        }
    }

    private int ZoneValue()
    {
        return ScoreZoneManager.Instance.ScoreScale * _zoneAmount;
    }   
}