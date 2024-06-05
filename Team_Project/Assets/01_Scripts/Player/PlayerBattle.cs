using UnityEngine;


public class PlayerBattle : MonoBehaviour
{
    // 공격 판정 위치
    public GameObject atkPosition;
    public Vector3 atkLenght;

    // 공격 감지할 레이어
    public LayerMask enemyLayer;
    // 적들 콜라이더 담아놓을 배열
    Collider[] enemys;

    PlayerState pState;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

        }
    }

    public void Attack(float atkPower)
    {
        Vector3 position = atkPosition.transform.position;
        enemys = Physics.OverlapBox(position, atkLenght, Quaternion.identity, enemyLayer);

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
        pState.UnitStat = UnitState.Hurt;
    }

    private void Start()
    {
        pState = GetComponent<PlayerState>();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(atkPosition.transform.position, atkLenght);
    }
}
