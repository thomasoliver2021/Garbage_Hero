using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LootLocker.Requests;

public class ScoreTracker : MonoBehaviour
{
    private static int leaderboardID = 5488; //NEEDS TO BE CHANGED FOR LIVE
    private int score;

    private void Start()
    {
        score = 0;
    }

    public void updateScore(int addedPoints)
    {
        score += addedPoints;
    }

    public IEnumerator SubmitScoreToLeaderboard()
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerID, score, leaderboardID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("successfully uploaded score");
                done = true;
            }
            else
            {
                Debug.Log("error uploading score: " + response.Error);
                done = true;
            }
        });

        yield return new WaitWhile(() => done == false);
    }
}
