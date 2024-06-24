using UnityEngine;

public class Billboard : MonoBehaviour
{
    Transform target;

    private void Start()
    {
        target = Camera.main.transform;
    }

    private void Update()
    {
        transform.forward = target.forward;
    }
}
