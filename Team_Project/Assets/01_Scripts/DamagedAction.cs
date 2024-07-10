using UnityEngine;

public abstract class DamagedAction : MonoBehaviour
{
    public abstract void Damaged(float hitPow);

    public abstract void KnockBack(Vector3 atkPos, float knockBackForce);
}
