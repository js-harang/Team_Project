using MySql.Data.MySqlClient;
using System;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    public static MySqlConnection sqlCon;

    [System.Serializable]
    public struct DatabaseInfo
    {
        [SerializeField]
        public string ipAddress;
        public string dbName;
        public string dbId;
        public string dbPw;
    }

    public DatabaseInfo database;

    private void Awake()
    {
        string strCon = string.Format("server={0}; database={1}; uid={2}; pwd={3};",
                                        database.ipAddress, database.dbName, database.dbId, database.dbPw);

        try
        {
            sqlCon = new MySqlConnection(strCon);
            Debug.Log("Connection True");
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
}
