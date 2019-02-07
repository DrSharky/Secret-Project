using System.Collections;
using UnityEngine;

public class ScreenSaver : MonoBehaviour
{
    private WaitForSeconds screenSaverDelay = new WaitForSeconds(2.0f);
    private Vector2 screenSaverPos = new Vector2();
    private GameObject screenSaverBlack;
    private GameObject screenSaverWhite;
    private RectTransform rectTransform;

    //create instance of the coroutine so it can be stopped & started again.
    private IEnumerator saverCoroutine;

    private void Awake()
    {
        screenSaverBlack = transform.GetChild(0).gameObject;
        screenSaverWhite = transform.GetChild(1).gameObject;
        rectTransform = GetComponent<RectTransform>();
        saverCoroutine = MoveScreenSaver();
    }

    //Start the coroutine when the canvas is enabled, instead of Start.
    void OnEnable()
    {
        StartCoroutine(saverCoroutine);
    }

    //Stop the coroutine when the canvas is disabled.
    void OnDisable()
    {
        StopCoroutine(saverCoroutine);
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

    //May need to adjust screen sizes to variables to accommodate for other computer screens.
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