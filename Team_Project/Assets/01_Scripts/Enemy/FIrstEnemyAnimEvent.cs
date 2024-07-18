using UnityEngine;

public class FIrstEnemyAnimEvent : MonoBehaviour
{
    FirstEnemy fEnemy;

    [SerializeField]
    Transform atkPosition;
    [SerializeField]
    float atkRange;

    Collider[] players;
    [SerializeField]
    LayerMask playerLayer;

    private void Start()
    {
        fEnemy = GetComponentInParent<FirstEnemy>();
    }

    public void Attack()
    {
        float atkPower = fEnemy.atkPower;
        Vector3 atkBox = new Vector3(atkRange, atkRange, atkRange);
        players = Physics.OverlapBox(
            atkPosition.position, atkBox, Quaternion.identity, playerLayer);

        foreach (Collider player in players)
        {
            if (player.gameObject.CompareTag("Player"))
            {
                DamagedAction damageAct = player.GetComponent<DamagedAction>();
                damageAct.Damaged(atkPower);
                damageAct.KnockBack(atkPosition.position, 7);
            }
        }
    }
}