using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObj : MonoBehaviour
{
    [SerializeField]
    float testObjHp;

    public void TestDebug(int atk)
    {
        Debug.Log("테스트용 오브젝트가 받은 데미지 :" + atk);
        testObjHp -= atk;
    }
}
