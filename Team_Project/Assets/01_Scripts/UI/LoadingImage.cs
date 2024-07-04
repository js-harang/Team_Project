using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingImage : MonoBehaviour
{
    [SerializeField] Sprite[] images;

    private void Start()
    {
        StartCoroutine(SpriteSwap());
    }

    IEnumerator SpriteSwap()
    {
        int index = 0;
        while (true)
        {
            gameObject.GetComponent<Image>().sprite = images[index];
            index = (index + 1) % images.Length;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnDisable()
    {
        StopCoroutine(SpriteSwap());
    }
}
