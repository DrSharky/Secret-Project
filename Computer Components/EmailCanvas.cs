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
    private List<EmailCommand> emailsToDisplay;
    private Dictionary<int, EmailCommand> emailDict = new Dictionary<int, EmailCommand>();
    private int pageIndex;

    public void DeleteEmail(int index)
    {
        //emailsToDisplay[index].showEmail = false;
        //emailsToDisplay.Remove(emailsToDisplay[index]);
        emailDict[index].showEmail = false;
        emailDict.Remove(index);

        CreateEmailText(index - (index % 10));
        SwitchState(ScreenType.EmailMenu);
    }

    public void SelectEmail(int emailIndex)
    {
        if (emailIndex >= (pageIndex + 10))
            return;

        emailIndex -= pageIndex;

        currentEmail = emailsToDisplay[emailIndex-1];
        currentEmail.read = true;
        emailDisplayText.text = currentEmail.displayText;
        SwitchState(ScreenType.Email);
    }

    void Awake()
    {
        emailParentCanvas = GetComponent<CanvasGroup>();
        if (hasEmail)
            CreateEmailObjects();

        for(int i = 0; i < emailCommands.Commands.Count; i++)
        {
            emailDict.Add(i+1, emailCommands.Commands[i]);
        }
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

    public void CreateEmailText(int startIndex = 0)
    {
        pageIndex = startIndex;

        //Need to create 2 objects for each email in list.
        //1 for the number of the email in the list,
        //and the second for the email subject text.
        //They're different because the number is what marks
        //it read/unread by changing text and background color.

        Vector3 emailListPos = emailMenuCanvas.transform.position;
        Quaternion emailListRot = emailMenuCanvas.transform.rotation;

        emailsToDisplay = emailDict.Values.Where((f, i) => i >= startIndex && f.showEmail).ToList();
        //emailsToDisplay = emailCommands.Commands.Where((f, i) => i >= startIndex && f.showEmail).ToList();

        for(int j = 0; j < emailTextObjects.Count; j++)
        {

            Text emailNumText = emailTextObjects[j].numberText.GetComponentInChildren<Text>();
            Text emailSub = emailTextObjects[j].subjectText.GetComponent<Text>();
            RawImage numberImg = emailTextObjects[j].numberText.GetComponent<RawImage>();

            if (j < emailsToDisplay.Count)
            {
                string emailIndex = CommonCompStrings.charDict[CommonCompStrings.Char.LBracket] + (startIndex + j + 1) +
                                    CommonCompStrings.charDict[CommonCompStrings.Char.RBracket];
                subjectText.text = emailsToDisplay[j].subject;

                //emailsToDisplay[j].commandText = (j + 1).ToString();

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
            else
            {
                emailNumText.text = CommonCompStrings.charDict[CommonCompStrings.Char.Empty];
                emailSub.text = CommonCompStrings.charDict[CommonCompStrings.Char.Empty];
                numberImg.color = new Color(0, 0, 0, 0);
            }
            
        }

        List<string> testTextList = new List<string>();

        for (int k = 0; k < emailTextObjects.Count; k++)
        {
            testTextList.Add(emailTextObjects[k].subjectText.GetComponent<Text>().text);
        }
    }
}
