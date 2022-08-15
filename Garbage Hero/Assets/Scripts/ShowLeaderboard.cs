using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class ShowLeaderboard : MonoBehaviour
{
    private static int leaderboardID = 5488; //NEEDS TO BE CHANGED FOR LIVE

    [SerializeField]
    TextMeshProUGUI playerNames;
    [SerializeField]
    TextMeshProUGUI playerScores;
    [SerializeField]
    TextMeshProUGUI userID;

    private void Start()
    {
        StartCoroutine(FetchTopScores());
    }

    public IEnumerator FetchTopScores()
    {
        bool done = false;
        LootLockerSDKManager.GetScoreList(leaderboardID, 5, 0, (response) =>
        {
            if (response.success)
            {
                string tempNames = "Names\n-----\n";
                string tempScores = "Scores\n-----\n";

                LootLockerLeaderboardMember[] members = response.items;

                foreach(LootLockerLeaderboardMember member in members)
                {
                    tempNames += member.rank + ". ";
                    tempNames += (member.player.name != "") ? member.player.name + "\n" : "anon " + member.player.id + "\n";
                    tempScores += member.score + "\n";
                }
                done = true;
                playerNames.text = tempNames;
                playerScores.text = tempScores;
                userID.text = "playing as: anon " + PlayerPrefs.GetString("PlayerID");
            }
            else
            {
                Debug.Log("Failed: " + response.Error);
                done = true;
            }
        });

        yield return new WaitWhile(() => done == false);
    }
}
