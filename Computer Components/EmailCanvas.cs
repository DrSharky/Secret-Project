using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailCanvas : MonoBehaviour
{
    [SerializeField] private GameObject numberObj;
    [SerializeField] private Text numberText;
    [SerializeField] private Text subjectText;

    [SerializeField] private bool hasEmail;
    [SerializeField] private EmailCommandList emailCommands;

    private CanvasGroup emailCanvas;


    void Awake()
    {
        emailCanvas = GetComponent<CanvasGroup>();
        if (hasEmail)
            CreateEmailText();
    }

    public void SwitchState(ScreenType state)
    {
        switch (state)
        {
            case ScreenType.Email:
            case ScreenType.EmailMenu:
                emailCanvas.alpha = 1;
                break;
            default:
                emailCanvas.alpha = 0;
                break;
        }
    }

    void CreateEmailText()
    {
        //Need to create 2 objects for each email in list.
        //1 for the number of the email in the list,
        //and the second for the email subject text.
        //They're different because the number is what marks
        //it read/unread by changing text and background color.

        Vector3 emailListPos = emailCanvas.transform.position;
        Quaternion emailListRot = emailCanvas.transform.rotation;


        for (int i = 0; i < emailCommands.Commands.Count; i++)
        {
            string emailIndex = CommonCompStrings.charDict[CommonCompStrings.Char.LBracket] + (i + 1) +
                                CommonCompStrings.charDict[CommonCompStrings.Char.RBracket];
            subjectText.text = emailCommands.Commands[i].subject;
            Text emailNumText;
            Text emailSub;
            Text emailNum;

            //if (i > 0)
            //{
                emailNum = Instantiate(numberText, emailCanvas.transform);
                emailNum.rectTransform.anchoredPosition += 
                    new Vector2(0.0f, -emailNum.rectTransform.rect.height * i);
                emailSub = Instantiate(subjectText, emailCanvas.transform);
                emailSub.rectTransform.anchoredPosition += 
                    new Vector2(0.0f, -emailSub.rectTransform.rect.height * i);
            //}
            //else
            //{
            //    emailCommands.Commands[i].read = true;
            //    emailNum = Instantiate(numberText, emailCanvas.transform);
            //    emailSub = Instantiate(subjectText, emailCanvas.transform);
            //}

            emailNumText = emailNum.transform.GetComponentInChildren<Text>();
            emailNumText.text = emailIndex;

            if (!emailCommands.Commands[i].read)
            {
                emailNum.color = Color.white;
                emailNumText.color = Color.black;
            }
        }
    }
}
