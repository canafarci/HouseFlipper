using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : Mover
{
    InputReader _inputReader;

    private void Start()
    {
        _inputReader = GetComponent<InputReader>();
        _speed = GameManager.Instance.References.GameConfig.PlayerSpeed;
    } 
}

