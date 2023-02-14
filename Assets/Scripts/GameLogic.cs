using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
//using Mono.Data.Sqlite;
using System.Data;
using LootLocker.Requests;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

public class GameLogic : MonoBehaviour
{
    public bool TimerOn = false;
    public int seqNo = 0;
    public float Timer;
    private string connection;
    private IDbConnection dbcon;
    private int id;
    [SerializeField]
    public TextMeshProUGUI timerField = null;

    private Vector3 originalPos;
    int leaderboardIDFirst = 11450;
    int leaderboardIDSecond = 11451;
    int leaderboardIDThird = 11459;
    int leaderboardIDFourth = 11460;
    int leaderboardIDFifth = 11461  ;
    string memberID;

    void Start()
    {
        StartTimer();
        seqNo = 0;
        GameObject player = GameObject.Find("Player");
      /*  LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (!response.success)
            {
                UnityEngine.Debug.Log("error starting LootLocker session");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                return;
            }

            UnityEngine.Debug.Log("successfully started LootLocker session");
        });*/
        originalPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
       // memberID = PlayerPrefs.GetString("PlayerID",""); //System.Guid.NewGuid().ToString();

    }

    void Update()
    {
        if (TimerOn == true)
        {
            Timer += Time.deltaTime;
            timerField.text = Math.Ceiling(Timer).ToString();
        }
    }


    public void SubmitScore(int score, int leaderboardID)
    {
        LootLockerSDKManager.SubmitScore(memberID, score, leaderboardID, (response) =>
            {
                if (response.statusCode == 200)
                {
                   UnityEngine.Debug.Log("Successful");
                }
                else
                {
                    UnityEngine.Debug.Log("failed: " + response.Error);
                }
            });
    }


/*public IEnumerator SubmitScoreRoutine(int scoreToUpload, int leaderboardID)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, leaderboardID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully uploaded score");
                done = true;
            }
            else
            {
                Debug.Log("Failed" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
*/

    public void GetCoin()
    {
        seqNo++;
        StopTimer();
       // insertResult();
      

        if (seqNo == 1)
        {
            SubmitScore((int)Math.Ceiling(Timer), leaderboardIDFirst);

            //StartCoroutine(SubmitScoreRoutine((int)Math.Ceiling(Timer), leaderboardIDFirst));
          
        }
        if (seqNo == 2)
        {
            SubmitScore((int)Math.Ceiling(Timer), leaderboardIDSecond);
          //  StartCoroutine(SubmitScoreRoutine((int)Math.Ceiling(Timer), leaderboardIDSecond));

        }
        if (seqNo == 3)
        {
            SubmitScore((int)Math.Ceiling(Timer), leaderboardIDThird);
            //StartCoroutine(SubmitScoreRoutine((int)Math.Ceiling(Timer), leaderboardIDThird));

        }
        if (seqNo == 4)
        {
            SubmitScore((int)Math.Ceiling(Timer), leaderboardIDFourth);
           // StartCoroutine(SubmitScoreRoutine((int)Math.Ceiling(Timer), leaderboardIDFourth));

        }
        if (seqNo == 5)
        {
            SubmitScore((int)Math.Ceiling(Timer), leaderboardIDFifth);
            //StartCoroutine(SubmitScoreRoutine((int)Math.Ceiling(Timer), leaderboardIDFifth));

        }
        Timer = 0;

        if (seqNo == 5)
        {
            /*OpenConnection();
            IDbCommand cmnd = dbcon.CreateCommand();
            cmnd.CommandText = "UPDATE participant " +
                "SET finished = 1, " +
                "finish_date = datetime('now')" +
                "WHERE id=" + id.ToString();
            cmnd.ExecuteNonQuery();*/

            //      CloseConnection();
            PlayerPrefs.DeleteAll();
            UnityEngine.Application.Quit();
            //Izsaukums uz demogrâfiju
            //  Application.OpenURL("https://latvia.questionpro.com/SBSOD?custom1=" + id.ToString());

        }
        else
        {
            ResetPlayer();
            StartTimer();
        }
    }
    public void StartTimer()
    {
        Timer = 0;
        TimerOn = true;
    }

    public void StopTimer()
    {
        TimerOn = false;
    }

  /*private void OpenConnection()
    
    {
        connection = "URI=file:" + Application.dataPath + "/Plugins/Participants.s3db";
        // Open connection
        dbcon = new SqliteConnection(connection);
        dbcon.Open();

    }*/
    /*private void insertResult()
    {
        OpenConnection();
        id = PlayerPrefs.GetInt("UserID");
        IDbCommand cmnd = dbcon.CreateCommand();
        int mistakes = PlayerPrefs.GetInt("Mistakes", 0);
        cmnd.CommandText = "INSERT INTO Results (participantID, time, seqNo, mistakes) VALUES (" + id + ", " + Timer + "," + seqNo + ","+ mistakes+ ")";
        PlayerPrefs.SetInt("Mistakes", 0);
        cmnd.ExecuteNonQuery();
        CloseConnection();

    }*/
  /*  private void NextScene()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }*/
    private void ResetPlayer()
    {
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerController>().enabled = false;
        player.transform.position = originalPos;
        player.GetComponent<PlayerController>().enabled = true;
    }
   /* private void CloseConnection()
    {
        // Close connection
        dbcon.Close();
    }*/
  



}
