using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public int lv;
    public string exp;
}
public class Users
{
    // User 클래스를 담을수 있는 배열을 만듬
    public UserData[] Datas;
}
