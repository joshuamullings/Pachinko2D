using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Upgrade : MonoBehaviour
{
    [SerializeField] protected Text _numberText;
    [SerializeField] protected Text _descriptionText;
    [SerializeField] protected Text _costText;
    protected int level;

    [SerializeField] private float _baseCost;
    [SerializeField] private float _costMultiplier;

    public abstract void OnButtonClick();
    protected abstract void UpdateText();

    public int Cost() => (int)(_baseCost * Mathf.Pow(_costMultiplier, level));
}