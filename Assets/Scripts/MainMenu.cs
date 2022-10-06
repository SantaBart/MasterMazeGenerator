using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI ifield = null;




    public void PlayGame()
    {
        if (string.IsNullOrEmpty(ifield.text))
        {
           
        }
        else
        {
            Console.WriteLine(ifield.text);
            string userID;
            userID = ifield.text;
            int.TryParse(GetNumbers(userID), out int result);
            PlayerPrefs.SetInt("UserID", result);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private static string GetNumbers(string input)
    {
        return new string(input.Where(c => char.IsDigit(c)).ToArray());
    }

    public void NewUser()
    {

    }

  
}
