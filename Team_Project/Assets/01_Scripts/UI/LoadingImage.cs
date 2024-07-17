using System.Collections;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class LoadingImage : MonoBehaviour
{
    [SerializeField] SpriteAtlas spriteAtlas;

    private void Start()
    {
        StartCoroutine(SpriteSwap());
    }

    IEnumerator SpriteSwap()
    {
        int index = 0;
        bool isAscending = true;

        while (true)
        {
            if (isAscending)
            {
                index++;

                if (index == 2)
                    isAscending = false;
            }
            else
            {
                index--;

                if (index == 0)
                    isAscending = true;
            }
            gameObject.GetComponent<Image>().sprite = spriteAtlas.GetSprite("ANI_LOADING_CASTER_" + index);

            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnDisable()
    {
        StopCoroutine(SpriteSwap());
    }
}
