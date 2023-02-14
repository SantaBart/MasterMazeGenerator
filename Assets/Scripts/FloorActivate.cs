using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
/*Counting mistakes*/

public class FloorActivate : MonoBehaviour
{
    private int mistakes;
    private int steps=0;
    private int previous;
    void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name);
        if (other.name == "Player" && GetInstanceID()==previous)
        {
            steps++;
            if (steps > 1)
            {
                //Debug.Log("steps");
               // mistakes = PlayerPrefs.GetInt("Mistakes", 0);
                mistakes++;
               // PlayerPrefs.SetInt("Mistakes", mistakes);
            }
        }
        previous = GetInstanceID();
    }
}
