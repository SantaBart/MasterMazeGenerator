using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;

public class PlayerCollision : MonoBehaviour
{



    void OnTriggerEnter(Collider other)
    {

        Debug.Log(other.name);
        if(other.name== "Coin(Clone)") 
       {
            GameObject GameLogic = GameObject.Find("GameLogic");
            GameLogic.GetComponent<GameLogic>().GetCoin();
        }
    }
}
