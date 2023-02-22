/******
 * Author: Santa Bartuðçvica
 * Summary: Main menu logic. Includes scene loading and saving players ID.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using System.Data;
using System.IO;
using LootLocker.Requests;


public class MainMenu : MonoBehaviour
{
    // player id
    [SerializeField]
    public TextMeshProUGUI ifield = null;

 

    public void PlayTestGame()
    {
        SceneManager.LoadScene(4);
    }

    // load instruction scene for the training test
    public void TestInstruction()
    {
        SceneManager.LoadScene(3);
    } 
    
    //load instruction scene for the labyrinth test
    public void Instruction()
    {
        SceneManager.LoadScene(1);
    }
    // load example scene for the labyrinth test
    public void PlayGame()
    {
        PlayerPrefs.SetString("PlayerID", ifield.text);
        SceneManager.LoadScene(6);
     
    }

    //Quit application
    public void Quit()
    {
        Application.Quit();

    } 

    //Delete previous player data
    public void QuitLast()
    {
        PlayerPrefs.DeleteAll();
        UnityEngine.Application.Quit();

    }


}
