using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    static int _currentMoney;

    public event Action<int> OnMoneyChanged;

    private void Start()
    {
        _currentMoney = GameManager.Instance.Saver.GetInt(PrefKeys.Money);
    }
    public void OnMoneyChange(int changeAmount)
    {
        _currentMoney += changeAmount;
        GameManager.Instance.Saver.SaveInt(PrefKeys.Money, _currentMoney);
        OnMoneyChanged(_currentMoney);
    }
}
