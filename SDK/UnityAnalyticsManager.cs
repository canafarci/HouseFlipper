using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;
using Unity.Services.Core.Environments;

public class UnityAnalyticsManager : MonoBehaviour
{
    async void Awake()
    {
        var options = new InitializationOptions();
        options.SetEnvironmentName("production");
        await UnityServices.InitializeAsync(options);
    }
}
