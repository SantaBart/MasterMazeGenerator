using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;

public class PlayerCollisionExample : MonoBehaviour
{



    void OnTriggerEnter(Collider other)
    {

      //  Debug.Log(other.name);
        if(other.name== "Coin") 
       {
            GameObject GameLogic = GameObject.Find("GameLogic");
            GameLogic.GetComponent<GameLogicExample>().GetCoin();
        }
    }
}
