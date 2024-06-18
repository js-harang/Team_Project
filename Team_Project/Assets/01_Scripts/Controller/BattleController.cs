using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    [SerializeField]
    GameObject hyperMoveZone;
    [SerializeField]
    GameObject enemyPrefab;
    [SerializeField]
    GameObject[] blockWall;

    int enemyCount;

    [SerializeField]
    int totalRound;
    int nowRound;

}
