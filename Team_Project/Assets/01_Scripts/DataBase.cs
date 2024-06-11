using MySql.Data.MySqlClient;
using System;
using System.Data;
using UnityEngine;

public class DataBase : MonoBehaviour
{
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

    public DataSet Search(int uid, string tableName)
    {
        string query = string.Format("select * from test where UID = {0};", uid);

        try
        {
            sqlCon.Open();   //DB 연결

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = sqlCon;
            cmd.CommandText = query;

            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sd.Fill(ds, tableName);

            sqlCon.Close();  //DB 연결 해제

            return ds;
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
            return null;
        }
    }
}
