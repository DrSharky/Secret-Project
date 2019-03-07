using System.Collections;
using UnityEngine;
using TMPro;
using System;

/// <summary>
/// This class is for creating a custom caret for TextMeshPro InputField GameObjects in Unity.
/// </summary>
public class Caret : MonoBehaviour
{
    #region Variables

    ///<summary> The text object for the caret. </summary>
    public TMP_Text caret;

    ///<summary> The transform, for changing the position.  </summary>
    public RectTransform rect;

    ///<summary>
    ///Number (in seconds) to wait before switching the 
    ///blink state of the caret.
    ///</summary>
    public float blinkRate = 0.5f;

    /// <summary> The original position of the caret. </summary>
    public Vector2 originPos;

    /// <summary> The last character input in the command input string. </summary>
    private char lastChar;

    /// <summary> Bool used to check if the string is empty. </summary>
    private bool empty;

    /// <summary>Boolean to determine whether or not to use the CheckForReset Coroutine.</summary>
    private bool debugCheck = true;

    /// <summary>
    /// An approximate value to convert spacing from TMPro values to default font space values.
    /// This value may not work for all fonts, change as needed.
    /// Default: 5.3f
    /// </summary>
    [SerializeField]
    private float fontSpacing = 5.3f;

    /// <summary> The font object, used for getting the advance distance of each character. </summary>
    [SerializeField]
    private TMP_FontAsset tmpFont;

    /// <summary> The text object of the input field, for getting the text inside the input box. </summary>
    [SerializeField]
    private TMP_Text inputText;

    /// <summary> The input field, needed for getting the char limit and total text input. </summary>
    [SerializeField]
    private TMP_InputField inputField;

    private WaitForSeconds blinkDelay = new WaitForSeconds(0.5f);

    private WaitForSeconds debugDelay = new WaitForSeconds(3.0f);

    private IEnumerator blinkRoutine;
    private IEnumerator debugRoutine;

    /// <summary>
    /// The string that should replace the vertical line as the caret in the TMPro InputField.
    /// Default: "_" (underscore)
    /// </summary>
    public string replaceCaret = "_";
    private string emptyString = "";
    private string bkspace = "\b";
    private string bkspaceDbl = "\b\b";

    /// <summary>
    /// Boolean to determine whether the caret has been enabled at least once.
    /// This is used to catch errors in case the Reset() method is ever called before the caret is enabled.
    /// </summary>
    private bool initialized = false;

    #endregion

    #region Methods

    /// <summary>
    /// Assign the coroutines in Awake so that
    /// StopRoutine actually does something.
    /// </summary>
    void Awake()
    {
        blinkRoutine = Blink();
        debugRoutine = CheckForReset();
    }

    /// <summary> Run when the command input panel is activated. </summary>
    void OnEnable()
    {
        originPos = rect.anchoredPosition;
        StartCoroutine(blinkRoutine);
        if(debugCheck)
            StartCoroutine(debugRoutine);
        initialized = true;
    }

    /// <summary> Run when the command input panel is deactivated. </summary>
    private void OnDisable()
    {
        Reset();
        StopCoroutine(blinkRoutine);
        if(debugCheck)
            StopCoroutine(debugRoutine);
    }

    /// <summary> Reset the caret to its original position. </summary>
    public void Reset()
    {
        if(initialized)
            rect.anchoredPosition = originPos;
    }

    /// <summary> Change location based on input. Called by ipFld onValueChanged method. </summary>
    public void ChangeLocation()
    {
        //If the user has pressed a key.
        if (Input.anyKey)
        {
            //Distance to move the caret in the x direction.
            float xAdv = 0f;

            //If the character count is greater than the max & input is not backspace, then return.
            if (inputText.textInfo.characterCount >= inputField.characterLimit + 1 &&
                !Input.inputString.Equals(bkspace, StringComparison.Ordinal))
                return;

            //Using double backspace because sometimes it inputs 2 if you hold backspace.
            //Possible problem: What if it inputs more than 2 in a certain instance? (i.e. frame lag)
            //Suggested fix if this is an issue, use regex to account for any number of backspace characters.
            else if (Input.inputString.Equals(bkspace, StringComparison.Ordinal) ||
                     Input.inputString.Equals(bkspaceDbl, StringComparison.Ordinal))
            {
                //If the user has pressed backspace, check if the input field
                //has any text in it.
                if (inputField.text.Length >= 0 && !empty)
                {
                    //Set xAdv to the distance specified by the font, according to the last character entered.
                    xAdv = tmpFont.characterDictionary[lastChar].xAdvance / fontSpacing;

                    //Move the caret's position to the left xAdv distance.
                    rect.anchoredPosition = new Vector2(rect.anchoredPosition.x - xAdv, rect.anchoredPosition.y);

                    //If the text input is empty, set empty bool to true.
                    if (inputField.text.Length == 0)
                        empty = true;
                }
            }

            //If the user presses a key that inputs a character, and the limit has not been reached,
            //then attempt to move the caret to the right.
            else if (Input.inputString.Length > 0 && !(inputText.textInfo.characterCount >=
                     inputField.characterLimit + 1))
            {
                //Try to move it to the right, using this in case of weird behaviour.
                try
                {
                    //Set xAdv to the distance specified by the font, according to the new character entered.
                    //fontSpacing is an approximation to the distance difference between TMPro fonts and regular fonts.
                    //The 5.3 value may not work for all fonts, and should be tweaked accordingly.
                    xAdv = tmpFont.characterDictionary[Input.inputString[0]].xAdvance / fontSpacing;

                    //Move the caret's position to the right xAdv distance.
                    rect.anchoredPosition = new Vector2(rect.anchoredPosition.x + xAdv, rect.anchoredPosition.y);

                    //Set empty to false, because a new character has been entered & entry hasn't failed thus far.
                    empty = false;
                }

                //Just in case anything weird happens.
                catch (Exception ex)
                {
                    //Output the message and stack trace of the exception.
                    Debug.Log(ex.Message + "Stack Trace: " + ex.StackTrace);
                }
            }

            //If the user presses the Enter/Return key, keypad enter, or Escape,
            //then reset the location and stop blink if needed.
            else if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Escape))
            {
                //If the Escape key is pressed, then stop the coroutines.
                if (Input.GetKey(KeyCode.Escape))
                {
                    StopCoroutine(blinkRoutine);
                    StopCoroutine(debugRoutine);
                }

                //Reset the location of the caret.
                Reset();
            }

            //If the user presses any other key that doesn't
            //input a character (i.e. Shift/Alt/Ctrl etc.),
            //then just return.
            else if (Input.inputString.Equals(emptyString, StringComparison.Ordinal))
                return;

            //After input checks, check to see if the input text is empty.
            //If it isn't empty, then set lastChar to the last character in the string.
            if (inputField.text.Length > 0)
                lastChar = inputField.text.Substring(inputField.text.Length - 1)[0];
        }
    }

    /// <summary>
    /// Coroutine that handles the caret blinking behaviour.
    /// </summary>
    IEnumerator Blink()
    {
        while (true)
        {
            if (caret.text.Equals(emptyString, StringComparison.Ordinal))
                caret.text = replaceCaret;
            else
                caret.text = emptyString;
            yield return blinkDelay;
        }
    }

    ///<summary> 
    ///Coroutine that checks if string is empty every
    ///interval of debugDelay (Default 3 seconds).
    ///If so, reset to originPos. 
    ///</summary>
    IEnumerator CheckForReset()
    {
        while (true)
        {
            if (inputText.text.Length == 1)
                rect.anchoredPosition = originPos;
            yield return debugDelay;
        }
    }

    #endregion
}