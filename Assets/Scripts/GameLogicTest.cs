using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using System.Data;


public class GameLogicTest : MonoBehaviour
{
    public bool TimerOn = false;
    public int seqNo = 0;
    public float Timer;
    private int id;
    [SerializeField]
    public TextMeshProUGUI timerField = null;

    private Vector3 originalPos;
    string first;
    string second;
    string third;
    string fourth;
    string fifth;
    [SerializeField]
    public RectTransform panel;
    [SerializeField]
    public TextMeshProUGUI rezText;

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


        if (seqNo == 1)
        {
            first= Math.Ceiling(Timer).ToString();

        }
        if (seqNo == 2)
        {
            second = Math.Ceiling(Timer).ToString();

        }
        if (seqNo == 3)
        {
            third = Math.Ceiling(Timer).ToString();

        }
        if (seqNo == 4)
        {
            fourth = Math.Ceiling(Timer).ToString();

        }
        if (seqNo == 5)
        {
            fifth = Math.Ceiling(Timer).ToString();

        }
        Timer = 0;

        if (seqNo == 5)
        {
            rezText.text = "1. "+ first + " sekundes <br>"+
                           "2. " + second + " sekundes <br>"+
                           "3. " + third + " sekundes <br>"+
                           "4. " + fourth + " sekundes <br>"+
                           "5. " + fifth + " sekundes <br>";
        panel.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

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


    private void ResetPlayer()
    {
        GameObject player = GameObject.Find("Player");
        player.GetComponent<PlayerController>().enabled = false;
        player.transform.position = originalPos;
        player.GetComponent<PlayerController>().enabled = true;
    }
    public void FirstScene()
    {
        Timer = 0;
        SceneManager.LoadScene(0);
    }

}
