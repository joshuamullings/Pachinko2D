using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_MaximumBalls : Upgrade
{
    public override void OnButtonClick()
    {
        if (UIManager.Instance.Score >= this.Cost())
        {
            UIManager.Instance.Score -= this.Cost();
            BallManager.Instance.MaximumBalls++;
            level++;
            UpdateText();
        }
    }

    protected override void UpdateText()
    {
        _numberText.text = BallManager.Instance.MaximumBalls.ToString();
        _costText.text = Cost().ToString();
    }
}