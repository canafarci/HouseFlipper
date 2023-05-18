using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStageStateCallback
{
    void StageEndCallback();
    void StateStartCallback();
}
