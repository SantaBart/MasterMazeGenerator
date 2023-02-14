using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;

public class PlayerCollisionTest : MonoBehaviour
{



    void OnTriggerEnter(Collider other)
    {

      //  Debug.Log(other.name);
        if(other.name== "Coin(Clone)") 
       {
            GameObject GameLogic = GameObject.Find("GameLogic");
            GameLogic.GetComponent<GameLogicTest>().GetCoin();
        }
    }
}
