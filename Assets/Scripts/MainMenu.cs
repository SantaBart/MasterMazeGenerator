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
using System.IO;


public class MainMenu : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI ifield = null;

    string filename = "";

    private string connection;
    private IDbConnection dbcon;
    private int id;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        id = CheckPreviousId();
        ifield.text = "ID - "+id.ToString();
        //PlayerPrefs.SetInt("UserID", id);
 

       
    }

    public void PlayGame()
    {
        if (string.IsNullOrEmpty(ifield.text))
        {
           //warning
        }
        else
        {
            OpenConnection();
            IDbCommand cmnd = dbcon.CreateCommand();
            cmnd.CommandText = "UPDATE participant " +
                "SET started = 1, " +
                "start_date = datetime('now')" +
                "WHERE id="+id.ToString();
            cmnd.ExecuteNonQuery();

            CloseConnection();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);  
        }
    }

    private static string GetNumbers(string input)
    {
        return new string(input.Where(c => char.IsDigit(c)).ToArray());
    }

    public void NewUser()
    {
        OpenConnection();
        IDbCommand cmd = dbcon.CreateCommand();
        cmd.CommandText = "select count(id) from participant";
        cmd.CommandType = CommandType.Text;
        int RowCount = 0;

        id = Convert.ToInt32(cmd.ExecuteScalar());
        ifield.text = "ID - "+id.ToString();
        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = "INSERT INTO participant (id) VALUES (" + id + ")";
        cmnd.ExecuteNonQuery();
        int.TryParse(GetNumbers(ifield.text), out int result);
        PlayerPrefs.SetInt("UserID", result);
        CloseConnection();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }


    private void CloseConnection() 
    {
        // Close connection
        dbcon.Close();
    }
    private void OpenConnection()
    {
        connection = "URI=file:" + Application.dataPath + "/Plugins/Participants.s3db";
        // Open connection
        dbcon = new SqliteConnection(connection);
        dbcon.Open();

        // Create table
        IDbCommand dbcmd;
        dbcmd = dbcon.CreateCommand();
        string q_createTable = "CREATE TABLE IF NOT EXISTS Participant" +
            " (id INTEGER PRIMARY KEY, start_date TEXT, started INTEGER, finished INTEGER, finish_date TEXT )";
        dbcmd.CommandText = q_createTable;
        dbcmd.ExecuteReader();
        IDbCommand dbcmd2 = dbcon.CreateCommand();
        string q2_createTable = "CREATE TABLE IF NOT EXISTS Results" +
        " (id INTEGER PRIMARY KEY AUTOINCREMENT, participantID INTEGER, time REAL, seqNo INTEGER, mistakes INTEGER )";
   
        dbcmd2.CommandText = q2_createTable;
        dbcmd2.ExecuteReader();

    }


    private int CheckPreviousId()
    {
        OpenConnection();
        IDbCommand dbcmd;
        dbcmd = dbcon.CreateCommand();
        dbcmd.CommandText = "SELECT id FROM Participant order by id desc limit 1";
        int i = dbcmd.ExecuteScalar() == DBNull.Value ? 0 : Convert.ToInt32(dbcmd.ExecuteScalar());
        CloseConnection();
        return i;
    }


  public void DropTableResults()
    {
        OpenConnection();
        IDbCommand dbcmd;
        dbcmd = dbcon.CreateCommand();
        dbcmd.CommandText = "DROP Table Results ";
        int i = Convert.ToInt32(dbcmd.ExecuteScalar());
        CloseConnection();
    }
    public void DropTableParticipant()
    {
        OpenConnection();
        IDbCommand dbcmd;
        dbcmd = dbcon.CreateCommand();
        dbcmd.CommandText = "DROP Table Participant ";
        int i = Convert.ToInt32(dbcmd.ExecuteScalar());
        CloseConnection();
        ifield.text = "ID - ";
    }
    public void Quit()
    {
        Application.Quit();

    }


}
