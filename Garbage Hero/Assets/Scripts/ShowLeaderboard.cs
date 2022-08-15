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
    [SerializeField]
    TMP_InputField inputField;
    [SerializeField]
    GameObject popup;

    private void Start()
    {
        if(PlayerPrefs.HasKey("PlayerID")) StartCoroutine(FetchTopScores());
        if(PlayerPrefs.HasKey("PlayerID")) StartCoroutine(FetchUsername());
        HidePlayerNameSetup();
    }

    public void ShowPlayerNameSetup()
    {
        popup.SetActive(true);
    }

    public void HidePlayerNameSetup()
    {
        popup.SetActive(false);
    }

    public void SetPlayerName()
    {
        LootLockerSDKManager.SetPlayerName(inputField.text, (response) =>
        {
            if (response.success)
            {
                Debug.Log("successfully updated player name");
                StartCoroutine(FetchTopScores());
                StartCoroutine(FetchUsername());
            }
            else Debug.Log("failed: " + response.Error);
        });
    }

    public IEnumerator FetchUsername()
    {
        bool done = false;

        LootLockerSDKManager.GetMemberRank(leaderboardID, PlayerPrefs.GetString("PlayerID"), (response) =>
        {
            if (response.success)
            {

                userID.text = "playing as: ";
                userID.text += (response.player.name != "") ? response.player.name : "anon " + response.player.id;
            }
            else
            {
                Debug.Log("Failed: " + response.Error);
                done = true;
            }
        });

        yield return new WaitWhile(() => done == false);
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
