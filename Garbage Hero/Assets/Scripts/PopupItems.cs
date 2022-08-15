using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupItems : MonoBehaviour
{
    [SerializeField]
    ShowLeaderboard leaderboardScript;
    
    public void ClosePopup()
    {
        leaderboardScript.HidePlayerNameSetup();
    }
}
