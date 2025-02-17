using UnityEngine;

public abstract class DamagedAction : MonoBehaviour
{
    // KnockBack 실행에 필요한 변수
    public Rigidbody rb;

    // 피격 시의 동작하는 메서드 선언
    public abstract void Damaged(float hitPow);

    // 피격 시 넉백되는 동작에 대한 메서드 선언
    public abstract void KnockBack(Vector3 atkPos, float knockBackForce);
}
