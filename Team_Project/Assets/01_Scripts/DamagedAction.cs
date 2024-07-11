using UnityEngine;

public abstract class DamagedAction : MonoBehaviour
{
    // �ǰ� ���� �����ϴ� �޼��� ����
    public abstract void Damaged(float hitPow);

    // �ǰ� �� �˹�Ǵ� ���ۿ� ���� �޼��� ����
    public abstract void KnockBack(Vector3 atkPos, float knockBackForce);
}
