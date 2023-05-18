using EasyMobile;
using GameAnalyticsSDK;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

public class LevelCompleteChecker : MonoBehaviour
{
    [SerializeField] int _actionsLeftToFinishLevel;
    [SerializeField] GameObject[] _objectToActivateAfterStage3;
    [SerializeField] GameObject _fx;
    [SerializeField] int _sceneIndexToLoad;

    TimeCounter _totalLevelCounter = new TimeCounter();
    TimeCounter _pastStage3Counter = new TimeCounter();

    private void Start()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Repair_Scene", "Total_Scene");
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Repair_Scene", "Past_Stage_3");
        GameAnalytics.NewDesignEvent("Repair_Scene_Event", 0f);

        

        Dictionary<string, object> pastStage3Parameters = new Dictionary<string, object>()
            {
                { "stageName", "pastStage3" },
                { "progressionStatus", "started" },
                {"timeSpent", 0f}
            };

        Events.CustomData("repairSceneProgression", pastStage3Parameters);

        Dictionary<string, object> totalSceneParameters = new Dictionary<string, object>()
            {
                { "stageName", "totalStage" },
                { "progressionStatus", "started" },
                { "timeSpent", 0f}
            };

        Events.CustomData("repairSceneProgression", totalSceneParameters);
    }

    public void OnStageComplete()
    {
        _actionsLeftToFinishLevel--;

        if (_actionsLeftToFinishLevel == 2)
        {
            foreach (GameObject go in _objectToActivateAfterStage3)
            {
                go.SetActive(true);
            }

            foreach (StartRepairStageRaycastable ss in FindObjectsOfType<StartRepairStageRaycastable>(true))
            {
                ss.PastStage3 = true;
            }

            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Repair_Scene", "Past_Stage_3");


            float time = _pastStage3Counter.EndCounting();
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "stageName", "pastStage3" },
                { "progressionStatus", "started" },
                { "timeSpent", time}
            };

            Events.CustomData("repairSceneProgression", parameters);
            Events.Flush();


            if (Advertising.IsInterstitialAdReady())
                Advertising.ShowInterstitialAd();

        }



        if (_actionsLeftToFinishLevel == 0)
        {
            print("level over");
            _fx.SetActive(true);
            GameManager.Instance.SceneLoader.FadedLoadScene(_sceneIndexToLoad, 1.5f);

            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Repair_Scene", "Total_Scene");
            GameAnalytics.NewDesignEvent("Repair_Scene_Event", 1f);

            float time = _totalLevelCounter.EndCounting();
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "stageName", "totalStage" },
                { "progressionStatus", "started" },
                { "timeSpent", time}
            };

            Events.CustomData("repairSceneProgression", parameters);
            Events.Flush();


            if (Advertising.IsInterstitialAdReady())
                Advertising.ShowInterstitialAd();

        }
    }
}
