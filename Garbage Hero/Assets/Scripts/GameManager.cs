using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

public class GameManager : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine(LootLockerStartSession());
    }

    IEnumerator LootLockerStartSession()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("successfully started LootLocker session");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("error starting LootLocker session");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

}
