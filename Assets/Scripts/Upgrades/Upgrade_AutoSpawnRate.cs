using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_AutoSpawnRate : Upgrade
{
    public override void OnButtonClick()
    {
        if (UIManager.Instance.Score >= this.Cost())
        {
            UIManager.Instance.Score -= this.Cost();
            BallManager.Instance.AutoSpawnDelay *= 0.9f;
            level++;
            UpdateText();
        }
    }

    protected override void UpdateText()
    {
        _numberText.text = BallManager.Instance.AutoSpawnDelay.ToString("0.00") + "s";
        _costText.text = Cost().ToString();
    }
}