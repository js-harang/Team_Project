using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattle : Unit, IBattle
{
    // ���� ���� ��ġ
    public GameObject atkPosition;
    public Vector3 atkLenght;

    // ���� ������ ���̾�
    public LayerMask enemyLayer;
    // ���� �ݶ��̴� ��Ƴ��� �迭
    Collider[] enemys;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack(atkPower);
        }
    }

    public void Attack(float atkPower)
    {
        Vector3 position = atkPosition.transform.position;
        enemys = Physics.OverlapBox(position, atkLenght,Quaternion.identity,enemyLayer);
        
        foreach (Collider enemy in enemys)
        {
            if (enemy.gameObject.tag == "Enemy")
            {

                Debug.Log("EnemyAtk" + atkPower);
            }
        }
    }

    public void Hurt(float damage)
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(atkPosition.transform.position, atkLenght);
    }
}
