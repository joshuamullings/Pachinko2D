using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_ZoneMultiplier : Upgrade
{
    public override void OnButtonClick()
    {
        if (UIManager.Instance.Score >= this.Cost())
        {
            UIManager.Instance.Score -= this.Cost();
            ScoreZoneManager.Instance.ScoreScale += 1;
            level++;
            UpdateText();
        }
    }

    protected override void UpdateText()
    {
        _numberText.text = ScoreZoneManager.Instance.ScoreScale.ToString() + "x";
        _costText.text = Cost().ToString();
    }
}