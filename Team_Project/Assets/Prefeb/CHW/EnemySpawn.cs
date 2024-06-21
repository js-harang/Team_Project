using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;

    private void Start()
    {
        BattleController bC = FindObjectOfType<BattleController>().GetComponent<BattleController>();
        Instantiate(enemyPrefab, gameObject.transform.position, Quaternion.identity);
        bC.EnemyCount += 1;
    }
}
