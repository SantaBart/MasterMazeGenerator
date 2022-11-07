using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;


public class Questionnaire : MonoBehaviour
{
    void Start()
    {



        Cursor.visible = true;
       
    }

    public void RedirectToQuestionnaire()
    {
        int id= PlayerPrefs.GetInt("UserID");

        UnityEngine.Application.OpenURL("https://latvia.questionpro.com/SBSOD?custom1=" + id.ToString());

    }
}
