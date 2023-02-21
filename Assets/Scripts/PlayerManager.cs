using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (!response.success)
            {
                Debug.Log("error starting LootLocker session");
              //  PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                return;
            }

            Debug.Log("successfully started LootLocker session");
        });

    //   StartCoroutine(LoginRoutine());
    }
/*
    IEnumerator LoginRoutine()
    {
        bool done = false;
        PlayerPrefs.SetString("LootLockerGuestPlayerID", "");
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Player was logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("Could not start session");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }*/

}
