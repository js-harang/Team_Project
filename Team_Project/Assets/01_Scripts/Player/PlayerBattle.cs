using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    public GameObject atkPosition;
    public Vector3 atkLenght;

    public LayerMask enemyLayer;

    Collider[] enemys;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        Vector3 position = atkPosition.transform.position;
        enemys = Physics.OverlapBox(position, atkLenght,Quaternion.identity,enemyLayer);
        
        foreach (Collider enemy in enemys)
        {
            if (enemy.gameObject.tag == "Enemy")
            {
                Debug.Log("EnemyAtk");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(atkPosition.transform.position, atkLenght);
    }
}
