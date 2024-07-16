using UnityEngine;

public class SpringAnimation : MonoBehaviour
{
    float time;
    RectTransform currentPosition;
    float x;

    private void Start()
    {
        currentPosition = gameObject.GetComponent<RectTransform>();
        x = currentPosition.anchoredPosition.x;
        Debug.Log(x);
    }

    private void Update()
    {
        //if (time < 0.4f)
        //    currentPosition.anchoredPosition = new Vector2(x, 12 - (30 * time));
        ////this.transform.position = new Vector3(0, 12 - (30 * time), 0);
        ////else if (time < 0.5f)
        ////    this.transform.position = new Vector3(0, time - 0.4f, 0) * 4;
        ////else if (time < 0.6f)
        ////    this.transform.position = new Vector3(0, 0.6f - time, 0) * 4;
        ////else if (time < 0.7f)
        ////    this.transform.position = new Vector3(0, (time - 0.6f) / 2, 0) * 4;
        ////else if (time < 0.8f)
        ////    this.transform.position = new Vector3(0, 0.05f - (time - 0.7f) / 2, 0) * 4;
        ////else
        ////    this.transform.position = Vector3.zero;

        ////if(time >= 1f)
        ////    resetAnim();

        //time += Time.deltaTime;
    }

    public void resetAnim()
    {
        time = 0;
    }
}
