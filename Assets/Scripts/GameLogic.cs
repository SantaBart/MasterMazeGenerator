using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using Mono.Data.Sqlite;
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
    string leaderboardIDFirst = "first";
    string leaderboardIDSecond = "second";
    string leaderboardIDThird = "third";
    string leaderboardIDFourth = "fourth";
    string leaderboardIDFifth = "fifth";
    string memberID;

    int firstRez;
    int secondRez;
    int thirdRez;
    int fourthRez;
    int fifthRez;


    void Start()
    {
        StartTimer();
        seqNo = 0;
        GameObject player = GameObject.Find("Player");

        originalPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        memberID = PlayerPrefs.GetString("PlayerID",""); //System.Guid.NewGuid().ToString();

    }

    void Update()
    {
        if (TimerOn == true)
        {
            Timer += Time.deltaTime;
            timerField.text = Math.Ceiling(Timer).ToString();
        }
    }


    public void SubmitScore(int score, string leaderboardID)
    {
        LootLockerSDKManager.SubmitScore(memberID, score,  leaderboardID, memberID, (response) =>
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
   
    public void GetCoin()
    {
        seqNo++;
        StopTimer();
   
      

        if (seqNo == 1)
        {
            firstRez = (int)Math.Ceiling(Timer);
            SubmitScore(firstRez, leaderboardIDFirst);
          
        }
        if (seqNo == 2)
        {
            secondRez = (int)Math.Ceiling(Timer);
            SubmitScore(secondRez, leaderboardIDSecond);

        }
        if (seqNo == 3)
        {
            thirdRez = (int)Math.Ceiling(Timer);
            SubmitScore(thirdRez, leaderboardIDThird);

        }
        if (seqNo == 4)
        {
            fourthRez = (int)Math.Ceiling(Timer);
            SubmitScore(fourthRez, leaderboardIDFourth);

        }
        if (seqNo == 5)
        {
            fifthRez = (int)Math.Ceiling(Timer);
          
            SubmitScore(fifthRez, leaderboardIDFifth);
        


            OpenConnection();
            IDbCommand cmnd = dbcon.CreateCommand();
            cmnd.CommandText = "INSERT INTO ParticipantResults (participantID, first, second, third, fourth, fifth ) VALUES( '"+memberID+"' , " + firstRez + ", " + secondRez + ", "+ thirdRez + ", " + fourthRez + "," + fifthRez+")";
   
            cmnd.ExecuteNonQuery(); 
            CloseConnection();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(5);      
        }else
        {
            ResetPlayer();
            StartTimer();
        }
        Timer = 0;
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


     private void CloseConnection() 
     {
       //   Close connection
         dbcon.Close();
     }
       private void OpenConnection()
        {
            connection = "URI=file:" + UnityEngine.Application.dataPath + "/Plugins/Participants.s3db";
           // Open connection
            dbcon = new SqliteConnection(connection);
            dbcon.Open();
            //Create table
            IDbCommand dbcmd;
            dbcmd = dbcon.CreateCommand();
            string createTable = "CREATE TABLE IF NOT EXISTS ParticipantResults (id INTEGER PRIMARY KEY AUTOINCREMENT, participantID TEXT, first INTEGER, second INTEGER, third INTEGER, fourth INTEGER, fifth INTEGER)";
            dbcmd.CommandText = createTable;
            dbcmd.ExecuteReader();

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
  
    private void ResetPlayer()
    {
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerController>().enabled = false;
        player.transform.position = originalPos;
        player.GetComponent<PlayerController>().enabled = true;
    }

  



}
