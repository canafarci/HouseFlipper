using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    float _currentMoney;
    public static event Action OnMoneyChanged;
}
