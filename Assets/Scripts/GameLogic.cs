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

    void Start()
    {
        StartTimer();
        seqNo = 0;
        GameObject player = GameObject.Find("Player");
        originalPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);

    }
    void Update()
    {
        if (TimerOn == true)
        {
            Timer += Time.deltaTime;
            timerField.text = Math.Ceiling(Timer).ToString();
        }
    }

    public void GetCoin()
    {
        seqNo++;
        StopTimer();
        insertResult();
        Timer = 0;

        if (seqNo == 5)
        {
            OpenConnection();
            IDbCommand cmnd = dbcon.CreateCommand();
            cmnd.CommandText = "UPDATE participant " +
                "SET finished = 1, " +
                "finish_date = datetime('now')" +
                "WHERE id=" + id.ToString();
            cmnd.ExecuteNonQuery();

            CloseConnection();
            Application.OpenURL("https://latvia.questionpro.com/SBSOD?custom1=" + id.ToString());
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

    private void OpenConnection()
    {
        connection = "URI=file:" + Application.dataPath + "/Plugins/Participants.s3db";
        // Open connection
        dbcon = new SqliteConnection(connection);
        dbcon.Open();

    }
    private void insertResult()
    {
        OpenConnection();
        id = PlayerPrefs.GetInt("UserID");
        IDbCommand cmnd = dbcon.CreateCommand();
        int mistakes = PlayerPrefs.GetInt("Mistakes", 0);
        cmnd.CommandText = "INSERT INTO Results (participantID, time, seqNo, mistakes) VALUES (" + id + ", " + Timer + "," + seqNo + ","+ mistakes+ ")";
        PlayerPrefs.SetInt("Mistakes", 0);
        cmnd.ExecuteNonQuery();
        CloseConnection();

    }
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
    private void CloseConnection()
    {
        // Close connection
        dbcon.Close();
    }
  



}
