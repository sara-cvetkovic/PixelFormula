using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardUIHandler : MonoBehaviour
{
    public GameObject leaderboardItemPrefab;
    SetLeaderboardItemInfo[] setLeaderboardItemInfo;
    VerticalLayoutGroup leaderboardLayoutGroup;

    public void Init()
    {
        leaderboardLayoutGroup = GetComponentInChildren<VerticalLayoutGroup>();

        if (leaderboardLayoutGroup == null)
        {
            Debug.LogError("Nema VerticalLayoutGroup!");
            return;
        }

        CarLapCounter[] carLapCounterArray = FindObjectsByType<CarLapCounter>(FindObjectsSortMode.None);

        if (leaderboardItemPrefab == null)
        {
            Debug.LogError("Leaderboard Item Prefab nije podešen!");
            return;
        }

        if (carLapCounterArray.Length == 0)
        {
            Debug.LogWarning("Nema CarLapCounter objekata!");
            return;
        }

        setLeaderboardItemInfo = new SetLeaderboardItemInfo[carLapCounterArray.Length];

        for (int i = 0; i < carLapCounterArray.Length; i++)
        {
            GameObject leaderboardInfoGameObject = Instantiate(leaderboardItemPrefab, leaderboardLayoutGroup.transform);
            setLeaderboardItemInfo[i] = leaderboardInfoGameObject.GetComponent<SetLeaderboardItemInfo>();
            setLeaderboardItemInfo[i].SetPositionText($"{i + 1}.");
        }

        Canvas.ForceUpdateCanvases();
    }

    public void UpdateList(List<CarLapCounter> lapCounters)
    {
        if (setLeaderboardItemInfo == null) return;

        string p1Name = PlayerPrefs.GetString("SkracenoPlayer1");

        for (int i = 0; i < lapCounters.Count; i++)
        {
            string carName = lapCounters[i].gameObject.name;
            Debug.Log("carName: " + carName + " | p1Name: " + p1Name);
            setLeaderboardItemInfo[i].SetDriverNameText(carName);

            // P1 je onaj cije ime odgovara, P2 je drugi
            string carKey = carName == p1Name ? "P1CAR" : "P2CAR";

            CarData carData = Resources.Load<CarData>("CarData/" + PlayerPrefs.GetString(carKey));
            if (carData != null)
                setLeaderboardItemInfo[i].SetTeamLogo(carData.TeamLogoSprite);
        }
    }
}