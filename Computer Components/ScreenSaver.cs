using System.Collections;
using UnityEngine;

public class ScreenSaver : MonoBehaviour
{
    private WaitForSeconds screenSaverDelay = new WaitForSeconds(2.0f);
    private Vector2 screenSaverPos = new Vector2();
    private GameObject screenSaverBlack;
    private GameObject screenSaverWhite;
    private RectTransform rectTransform;

    private void Awake()
    {
        screenSaverBlack = transform.GetChild(0).gameObject;
        screenSaverWhite = transform.GetChild(1).gameObject;
        rectTransform = GetComponent<RectTransform>();
    }

    // Use this for initialization
    void Start()
	{
        StartCoroutine(MoveScreenSaver());
	}

    IEnumerator MoveScreenSaver()
    {
        while (true)
        {
            int randomColor = Random.Range(0, 2);
            if (randomColor == 1)
            {
                screenSaverBlack.SetActive(false);
                screenSaverWhite.SetActive(true);
            }
            else
            {
                screenSaverBlack.SetActive(true);
                screenSaverWhite.SetActive(false);
            }
            RandomScreenPosition();
            yield return screenSaverDelay;
        }
    }

    void RandomScreenPosition()
    {
        int widthLimit = ((int)-(rectTransform.rect.width / 2) - 140);
        int heightLimit = ((int)(rectTransform.rect.height / 2) - 65);
        int posX = Random.Range(-widthLimit, widthLimit);
        int posY = Random.Range(-heightLimit, heightLimit);
        screenSaverPos.x = posX;
        screenSaverPos.y = posY;
        rectTransform.anchoredPosition = screenSaverPos;
    }
}