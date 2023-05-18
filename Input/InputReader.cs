using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    FloatingJoystick _joystick;
    private void Awake() => _joystick = FindObjectOfType<FloatingJoystick>();
    public Vector2 ReadInput() => new Vector2(_joystick.Horizontal, _joystick.Vertical);
}
