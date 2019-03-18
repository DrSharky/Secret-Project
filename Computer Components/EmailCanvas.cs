using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public struct EmailTextObject
{
    public GameObject numberText;
    public GameObject subjectText;
}

public class EmailCanvas : MonoBehaviour
{
    [SerializeField] private GameObject numberObj;
    [SerializeField] private Text numberText;
    [SerializeField] private Text subjectText;
    [SerializeField] private GameObject subjectObj;

    [SerializeField] private bool hasEmail;
    [SerializeField] private EmailCommandList emailCommands;
    [SerializeField] private CanvasGroup emailMenuCanvas;
    [SerializeField] private CanvasGroup emailDisplayCanvas;
    [SerializeField] private Text emailDisplayText;

    private EmailCommand currentEmail;
    private CanvasGroup emailParentCanvas;
    private List<EmailTextObject> emailTextObjects = new List<EmailTextObject>();

    public void SelectEmail(int emailIndex)
    {
        currentEmail = emailCommands.Commands[emailIndex];
        currentEmail.read = true;
        emailDisplayText.text = currentEmail.displayText;
        SwitchState(ScreenType.Email);
    }

    void Awake()
    {
        emailParentCanvas = GetComponent<CanvasGroup>();
        if (hasEmail)
            CreateEmailObjects();
    }

    public void SwitchState(ScreenType state)
    {
        switch (state)
        {
            case ScreenType.Email:
                emailDisplayCanvas.alpha = 1;
                emailMenuCanvas.alpha = 0;
                emailParentCanvas.alpha = 1;
                break;
            case ScreenType.EmailMenu:
                emailMenuCanvas.alpha = 1;
                emailDisplayCanvas.alpha = 0;
                emailParentCanvas.alpha = 1;
                CreateEmailText();
                break;
            default:
                emailMenuCanvas.alpha = 0;
                emailDisplayCanvas.alpha = 0;
                emailParentCanvas.alpha = 0;
                break;
        }
    }

    //TODO: Change so that text from email body can be used in the email text area.
    //Maybe create a second text area for displaying body of emails.

    void CreateEmailObjects()
    {
        for (int i = 0; i < 10; i++)
        {
            Text emailSub;
            GameObject numObjCopy;
            GameObject subObjCopy;

            numObjCopy = Instantiate(numberObj, emailMenuCanvas.transform);
            subObjCopy = Instantiate(subjectObj, emailMenuCanvas.transform);

            EmailTextObject emailLine = new EmailTextObject { numberText = numObjCopy, subjectText = subObjCopy };

            emailTextObjects.Add(emailLine);

            RectTransform numRect = emailTextObjects[i].numberText.GetComponent<RectTransform>();
            RectTransform subRect = emailTextObjects[i].subjectText.GetComponent<RectTransform>();

            numRect.anchorMin = new Vector2(0f, 1f);
            numRect.anchorMax = new Vector2(0f, 1f);

            numRect.anchoredPosition += new Vector2(0f, -numRect.rect.height * (i + 1));


            subRect.anchorMin = new Vector2(0f, 1f);
            subRect.anchorMax = new Vector2(0f, 1f);

            subRect.anchoredPosition += new Vector2(0f, -subRect.rect.height * (i + 1));
        }
    }

    void CreateEmailText(int startIndex = 0)
    {
        //Need to create 2 objects for each email in list.
        //1 for the number of the email in the list,
        //and the second for the email subject text.
        //They're different because the number is what marks
        //it read/unread by changing text and background color.

        Vector3 emailListPos = emailMenuCanvas.transform.position;
        Quaternion emailListRot = emailMenuCanvas.transform.rotation;

        List<EmailCommand> emailsToDisplay = emailCommands.Commands.Where((f, i) => i >= startIndex && f.showEmail).ToList();

        for(int j = 0; j < 10 && j < emailsToDisplay.Count; j++)
        {
            string emailIndex = CommonCompStrings.charDict[CommonCompStrings.Char.LBracket] + (j + 1) +
                                CommonCompStrings.charDict[CommonCompStrings.Char.RBracket];
            subjectText.text = emailsToDisplay[j].subject;
            emailsToDisplay[j].commandText = (j+1).ToString();

            Text emailNumText = emailTextObjects[j].numberText.GetComponentInChildren<Text>();
            Text emailSub = emailTextObjects[j].subjectText.GetComponent<Text>();
            RawImage numberImg = emailTextObjects[j].numberText.GetComponent<RawImage>();
            emailNumText.text = emailIndex;
            emailSub.text = emailsToDisplay[j].subject;

            if (!emailsToDisplay[j].read)
            {
                numberImg.color = Color.white;
                emailNumText.color = Color.black;
            }
            else
            {
                numberImg.color = Color.black;
                emailNumText.color = Color.white;
            }

        }


        //for (int i = 0; i < emailCommands.Commands.Count; i++)
        //{
        //    string emailIndex = CommonCompStrings.charDict[CommonCompStrings.Char.LBracket] + (i + 1) +
        //                        CommonCompStrings.charDict[CommonCompStrings.Char.RBracket];
        //    subjectText.text = emailCommands.Commands[i].subject;
        //    Text emailNumText;
        //    Text emailSub;
        //    GameObject numObjCopy;

        //    numObjCopy = Instantiate(numberObj, emailMenuCanvas.transform);

        //    RectTransform numRect = numObjCopy.GetComponent<RectTransform>();
        //    RawImage numberImg = numObjCopy.GetComponent<RawImage>();

        //    numRect.anchorMin = new Vector2(0f, 1f);
        //    numRect.anchorMax = new Vector2(0f, 1f);

        //    numRect.anchoredPosition +=
        //        new Vector2(0f, -numRect.rect.height * (i + 1));

        //    emailSub = Instantiate(subjectText, emailMenuCanvas.transform);

        //    emailSub.rectTransform.anchorMin = new Vector2(0f, 1f);
        //    emailSub.rectTransform.anchorMax = new Vector2(0f, 1f);

        //    emailSub.rectTransform.anchoredPosition +=
        //        new Vector2(0f, -emailSub.rectTransform.rect.height * (i + 1));

        //    emailNumText = numObjCopy.transform.GetComponentInChildren<Text>();
        //    emailNumText.text = emailIndex;

        //    if (!emailCommands.Commands[i].read)
        //    {
        //        numberImg.color = Color.white;
        //        emailNumText.color = Color.black;
        //    }
        //}
    }
}
