using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObj : MonoBehaviour
{
    [SerializeField]
    float testObjHp;

    public void TestDebug(int atk)
    {
        Debug.Log("�׽�Ʈ�� ������Ʈ�� ���� ������ :" + atk);
        testObjHp -= atk;
    }
}
