using UnityEngine;

public class LineToRight : MonoBehaviour
{
    public Material bgMaterial;
    public float scrollSpeed;

    private void Update()
    {
        Vector2 dir = Vector2.left;
        bgMaterial.mainTextureOffset += dir * scrollSpeed * Time.deltaTime;
    }
}
