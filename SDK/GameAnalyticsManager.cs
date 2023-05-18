using GameAnalyticsSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAnalyticsManager : MonoBehaviour
{
    void Start()
    {
        GameAnalytics.Initialize();
        IronSource.Agent.validateIntegration();
    }
}
