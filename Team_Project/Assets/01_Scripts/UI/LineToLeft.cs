using UnityEngine;

public class LineToLeft : MonoBehaviour
{
    public Material bgMaterial;
    public float scrollSpeed;

    private void Update()
    {
        Vector2 dir = Vector2.right;
        bgMaterial.mainTextureOffset += dir * scrollSpeed * Time.deltaTime;
    }
}
