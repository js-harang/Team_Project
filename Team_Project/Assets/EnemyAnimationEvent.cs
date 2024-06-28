using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    public EnemyFSM enemyFSM;
    void Attack()
    {
        enemyFSM.AttackAction();
    }
}