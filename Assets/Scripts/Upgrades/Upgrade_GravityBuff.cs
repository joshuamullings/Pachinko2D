using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_GravityBuff : Upgrade
{
    public override void OnButtonClick()
    {
        if (UIManager.Instance.Score >= this.Cost())
        {
            UIManager.Instance.Score -= this.Cost();
            GameManager.Instance.GravityScale += new Vector2(0.0f, -9.81f);
            level++;
            UpdateText();
        }
    }

    protected override void UpdateText()
    {
        _numberText.text = (GameManager.Instance.GravityScale.y / -9.81f).ToString("0.0") + "g";
        _costText.text = Cost().ToString();
    }
}