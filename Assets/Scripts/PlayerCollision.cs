/******
 * Author: Santa Bartuðçvica
 * Summary: Trigger for the labyrinth  test coin collection. 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;

public class PlayerCollision : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {

        if(other.name== "Coin(Clone)") 
       {
            GameObject GameLogic = GameObject.Find("GameLogic");
            GameLogic.GetComponent<GameLogic>().GetCoin();
        }
    }
}
