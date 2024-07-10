using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateCharacter : MonoBehaviour
{
    [SerializeField] Sprite createSprite;

    [SerializeField, Space(10)] GameObject[] selectImg;

    public void SelectedSlot(int num)
    {
        Image image = selectImg[num].GetComponent<Image>();

        if (image.sprite == createSprite)
            SceneManager.LoadScene("11_CreateScene");
    }
}
