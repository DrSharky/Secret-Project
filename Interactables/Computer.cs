using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO: Add functionality for audio source. - only email audio stuff not done.
//TODO: Separate the screen saver code into a different script. It will make this shorter and more tolerable.

/// <summary>
/// The class to use for interactive computers.
/// </summary>
public class Computer : Interactable
{
    #region Variables

    #region Menus, Commands, Emails
    //List of possible commands at the home menu.
    //Defined in the inspector depending on which computer.
    [SerializeField]
    private List<ComputerCommand> commands;

    //List of possible menus.
    //Defined in the inspector depending on which computer.
    [SerializeField]
    private List<MenuCommand> menus;

    //List of the emails for the computer.
    [SerializeField]
    private List<EmailCommand> emails;

    private EmailMenuCommand emailCommand;
    #endregion

    #region Panel GameObjects
    [Header("Panels")]
    //public GameObject titlePanel;
    public GameObject emailPanel;
    #endregion

    #region Text Components
    [Header("Text Components")]
    public Text menuListText;
    public Text commandListText;
    public Text mainText;
    public Text emailDisplayText;

    //For accessing the menu title text to change it later.
    //Assign this in Start().
    public Text menuTitleText;

    //For accessing the email title text ot change it later.
    //Assign this in Start().
    public Text emailTitleText;

    //For accessing the title text to change it later.
    //Assign this in Start().
    private Text titleText;

    public CanvasGroup titleCanvas;
    public CanvasGroup menuCanvas;
    public CanvasGroup commandCanvas;
    public CanvasGroup displayTextCanvas;

    //For accessing the subtitle text to change it later.
    //Assign this in Start().
    private Text subtitleText;

    private Text[] titleObjects;

    #endregion

    #region Text Mesh Pro InputFields
    [Header("TM_Pro InputFields")]
    //The input field for the text input gameobject.
    [SerializeField]
    //private InputField commandText;
    private TMPro.TMP_InputField commandText;

    [SerializeField]
    private TMPro.TMP_InputField passwordText;
    #endregion

    #region Command Fields
    //Always have a home command.
    private MenuCommand homeCommand;
    //Always have a quit command.
    private ComputerCommand quitCommand;
    //Always have a help command.
    private ComputerCommand helpCommand;
    //Variable to store the current menu.
    private MenuCommand currentCommandMenu;
    #endregion

    #region Home Header Strings
    [Header("Home Header Strings")]
    //Set the home command's title & subtitle in the inspector.
    public string homeCommandTitle;
    public string homeCommandSubtitle;
    #endregion

    #region Screen Saver Fields
    [Header("Screen Saver Fields")]
    //Screen Saver objects and time boolean.
    [SerializeField]
    private GameObject ScreenSaverCanvas;
    //[SerializeField]
    //private GameObject ScreenSaverBlack;
    //[SerializeField]
    //private GameObject ScreenSaverWhite;
    //private bool moveSaverTime = true;
    //private WaitForSeconds screenSaverDelay = new WaitForSeconds(2.0f);
    //private Vector2 screenSaverPos = new Vector2();
    #endregion

    #region Rect Transforms
    //Need this to determine width & height of screen.
    private RectTransform rectTransform;
    //private RectTransform screenSaverTransform;
    #endregion

    #region Email Info
    [Header("Email Info")]
    //We need one of these, of course.
    [SerializeField]
    private EmailInfo emailInfo;
    #endregion

    #region Carets
    [Header("Carets")]
    [SerializeField]
    private Caret cmdCaret;
    //[SerializeField]
    //private Caret passCaret;
    private GameObject cmdCaretObject;
    //private GameObject passCaretObject;
    #endregion

    #region Audio Components
    [Header("Audio Components")]
    //Audio source for the computer.
    //Default clip is accessSound.
    public AudioSource computerAudioSource;

    public AudioClip acceptSound;
    public AudioClip accessSound;
    public AudioClip errorSound;
    public AudioClip typingSound;
    #endregion

    //#region Error Strings
    ////Made this a text area because I'm not sure how to format it properly as a simple string.
    ////NOTE: Might change later.
    //[Space(10)]
    //[TextArea]
    //public string errorText;
    //private string helpTitle = "Help information";
    //#endregion

    //#region Command Strings
    //private string listCmd = "list";
    //private string emailCmd = "email";
    //private string menuIndent = "   ";
    //private string newLine = "\n";
    //private string helpCmd = "help";
    //private string quitCmd = "quit";
    //#endregion

    //#region Password Strings
    //private string passReq = "Password required";
    //private string passSucc = "Password Succeeded";
    //private string passFail = "Password Failed";
    //private string displayPassAccepted = "Password accepted: <";
    //private string grthan = ">";
    //private string entering = "Entering ";
    //#endregion

    //#region Miscellanous Strings
    //private string emptyString = "";
    //private string alpha = "abcdefghijklmnopqrstuvwxyz";
    //#endregion

    #region Other Variables

    //Boolean to tell other things if the player is using the computer currently.
    public static bool usingComputer = false;
    private bool errorDisplay = false;
    private bool isHacking = false;

    private ScreenType currentScreenType = ScreenType.Normal;

    public RawImage numberText;
    public Text subjectText;

    private Canvas mainCanvas;
    #endregion

    #region EventNames
    private string titleEventString, passEventString,
                   displayEventString, menuEventString,
                   emailEventString, exitEventString;
    #endregion
    #endregion

    #region Methods

    #region MonoBehaviour Methods

     public override void Awake()
    {
        base.Awake();
        mainCanvas = GetComponent<Canvas>();
    }

    void Start()
	{
        //Assign event string variables;
        //titleEventString = StringManager.titlePanelToggle + gameObject.name;
        //passEventString = StringManager.passwordPanelToggle + gameObject.name;
        //displayEventString = StringManager.displayPanelToggle + gameObject.name;
        //menuEventString = StringManager.menuPanelToggle + gameObject.name;
        //emailEventString = StringManager.emailPanelToggle + gameObject.name;
        //exitEventString = StringManager.exitScreenEvent + gameObject.name;

        //Assign these so we don't need to access them indirectly later.
        cmdCaretObject = cmdCaret.gameObject;
        //passCaretObject = passCaret.gameObject;

        //Use this to get the width & height of screen.
        //Needed for the screen saver routine.
        rectTransform = gameObject.GetComponent<RectTransform>();

        //Use this to get the width & height of the screen saver
        //text so when picking a random position, it doesn't overlap the edges.
        //screenSaverTransform = ScreenSaverText.GetComponent<RectTransform>();

        //Computer panel structure will always be the same, so the direct index
        //references with 4 & 5 are fine.
        //titleObjects = titlePanel.GetComponentsInChildren<Text>();
        //if (titleObjects[4] != null)
        //    titleText = titleObjects[4];
        //if (titleObjects[5] != null)
        //    subtitleText = titleObjects[5];
        titleObjects = titleCanvas.gameObject.GetComponentsInChildren<Text>();
        if (titleObjects[0] != null)
                titleText = titleObjects[0];
        if (titleObjects[1] != null)
            subtitleText = titleObjects[1];

        //Add these to every computer.
        helpCommand = new ComputerCommand(StringManager.helpCmd);
        quitCommand = new ComputerCommand(StringManager.quitCmd);
        commands.Add(helpCommand);
        commands.Add(quitCommand);

        //Set up the home menu command variables.
        homeCommand = new MenuCommand();
        homeCommand.menuTitle = homeCommandTitle;
        homeCommand.menuPanelTitle = CapitalizeMenuName(homeCommand.commandText) + StringManager.menuTitleSuffix;
        homeCommand.menuSubtitle = homeCommandSubtitle;
        homeCommand.subCommands = commands;

        //Insert the email & home commands,
        //if the email comamnd should exist.
        //Then set the current menu to the home menu.
        if (emailInfo.hasEmail)
        {
            //Email menu should always be the first in the list.
            emailCommand = new EmailMenuCommand();
            menus.Insert(0, emailCommand);

            //Populate the email list.
            emailCommand.AssignEmails(emails);
            //Set the email menu password.
            emailCommand.password = emailInfo.emailPassword;

            //Home menu should always be after the email menu, if email menu exists.
            menus.Insert(1, homeCommand);
            //Set the current menu to the home menu.
            currentCommandMenu = menus[1];

            //Set the email text to the correct format.
            ChangeEmailTitleText();

            CreateEmailText();
        }
        else
        {
            //If no email menu exists for the computer, then assign
            //the home menu to be first in the list, and set as current menu.
            menus.Insert(0, homeCommand);
            currentCommandMenu = menus[0];
        }

        //Populate the menu list for the home command menu.
        homeCommand.displayText = MenusList(homeCommand);

        //Set the display text for all the menus at start,
        //instead of running them at activation. This will save memory in the long run.
        for(int i = 0; i < menus.Count; i++)
        {
            //If the menu is not email menu, then populate the commands list string
            //from the subcommands of the menu.
            if(!menus[i].commandText.Equals(StringManager.emailCmd, System.StringComparison.Ordinal))
                menus[i].commandsDisplayText = CommandsList(menus[i].subCommands);
        }
    }

    void Update()
	{
        //If the screen saver routine has counted to 2,
        //run it so that it will move.
        //if (moveSaverTime)
        //    StartCoroutine(MoveScreenSaver());

        if (usingComputer)
        {
            //If the user presses Esc & is using the computer,
            //then reset the computer and give player control back.
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                switch (currentScreenType)
                {
                    case ScreenType.EmailMenu:
                    case ScreenType.Password:
                        //Reset and select command text before show menu.
                        //Not sure why, but it needs to be in this order.
                        ResetCommandText();
                        SelectCommandText();
                        ShowMenu(homeCommand);
                        break;
                    case ScreenType.Email:
                        ShowEmailMenu();
                        break;
                    case ScreenType.Normal:
                        ExitScreen();
                        break;
                }
            }
            else if (isHacking)
                return;
            else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                if (currentScreenType == ScreenType.Password)
                {
                    if (!currentCommandMenu.alreadyHacked)
                        PasswordEnter(passwordText.text);
                    else
                    {
                        SelectCommandText();
                        if(currentCommandMenu.commandText.Equals(StringManager.emailCmd, System.StringComparison.Ordinal))
                            ShowEmailMenu();
                        else
                            ShowMenu(currentCommandMenu);
                    }
                }
                else if(currentScreenType == ScreenType.PasswordFail)
                    ShowHacking();
                else
                    CommandEnter();
            }
            else if ((Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
                     && currentScreenType == ScreenType.Password && commandText.text.Length == 0)
            {
                //If Ctrl is pressed on a hackable menu, start hacking process.
                StartHacking();
            }
        }
    }
    #endregion

    #region Activate / Deactivate Computer
    //User activated the computer by interacting with it.
    public override void Activate()
    {
        mainCanvas.enabled = true;
        titleCanvas.alpha = 1;
        menuCanvas.alpha = 1;
        EventManager.TriggerEvent("Activate" + commandCanvas.name, ScreenType.Normal);
        //Play access sound when activated.
        computerAudioSource.PlayOneShot(accessSound);

        //Turns off the screen saver text.
        ScreenSaverCanvas.SetActive(false);

        //EventManager.TriggerEvent(titleEventString, true);
        cmdCaretObject.SetActive(true);

        //Set this static variable so other scripts know the user
        //is using a computer.
        usingComputer = true;

        //Select the input box so that the user doesn't
        //have to select it manually.
        SelectCommandText();

        //Show the home menu on activation.
        ShowMenu(homeCommand);
    }

    //User has exited the computer.
    void ExitScreen()
    {
        ResetCommandText();
        ResetPasswordText();

        //Set the screen saver to be active again.
        ScreenSaverCanvas.SetActive(true);
        EventManager.TriggerEvent(exitEventString);
        cmdCaret.gameObject.SetActive(false);

        //Set the current menu back to the home menu.
        currentCommandMenu = homeCommand;

        //Set the static variable so other scripts know the user
        //is no longer using a computer.
        usingComputer = false;

        RigidbodyFirstPersonController.frozen = false;
        mainCanvas.enabled = false;
        titleCanvas.alpha = 0;
        menuCanvas.alpha = 0;
        EventManager.TriggerEvent("Toggle" + commandCanvas.name);
    }
    #endregion

    #region Text Input Methods
    void PasswordEnter(string enteredPassword = null)
    {
        //Password accepted.
        if (enteredPassword == currentCommandMenu.password)
        {
            computerAudioSource.PlayOneShot(acceptSound);

            //set the hacked to true, so user doesn't have to hack again.
            currentCommandMenu.alreadyHacked = true;

            //Set the display text panel to show the appropriate message.
            mainText.text = StringManager.displayPassAccepted + currentCommandMenu.password + StringManager.grthan + StringManager.newLine
                + StringManager.entering + currentCommandMenu.commandText + StringManager.period;

            //Set the title text to the appropriate message.
            titleText.text = StringManager.passSucc;

            //Hide the password input, need to show the Press Enter prompt after password succeeds.
            //The command input sets to proper display text when the display panel is toggled to true.
            EventManager.TriggerEvent(passEventString, false);
            //EventManager.TriggerEvent(displayEventString, true);
            displayTextCanvas.alpha = 1;
            ResetPasswordText();
        }
        //Password failed.
        else
        {
            titleText.text = StringManager.passFail;
            computerAudioSource.PlayOneShot(errorSound);

            EventManager.TriggerEvent(passEventString, false);
            //EventManager.TriggerEvent(displayEventString, true);
            displayTextCanvas.alpha = 1;
            ResetPasswordText();
            currentScreenType = ScreenType.PasswordFail;
        }
    }

    //The method that runs when a command is entered.
    public void CommandEnter()
    {
        ComputerCommand enteredCommand;
        string commandString = commandText.text;

        //If user is on a display page (using errorDisplay bool, should rename).
        if(errorDisplay)
        {
            //If the user presses enter on the error display page.
            if (Input.GetKey(KeyCode.Return))
            {
                commandString = currentCommandMenu.commandText;
                ResetCommandText();
                SelectCommandText();
                errorDisplay = false;
            }
        }

        if(commandString == StringManager.quitCmd)
        { ExitScreen(); return; }
        if(commandString == StringManager.helpCmd)
        { ShowErrorText(); ResetCommandText(); SelectCommandText(); return; }

        enteredCommand = currentCommandMenu.subCommands.Find(x => x.commandText.Equals(commandString, System.StringComparison.Ordinal));
        if (enteredCommand != null)
        {
            mainText.text = enteredCommand.displayText;

            //EventManager.TriggerEvent(displayEventString, true);
            displayTextCanvas.alpha = 1;
            menuCanvas.alpha = 0;

            EventManager.TriggerEvent("State" + commandCanvas.name, ScreenType.DisplayText);
            

            commandText.interactable = false;
            errorDisplay = true;
            return;
        }
        else
        {
            MenuCommand enteredMenu = menus.Find(x => x.commandText.Equals(commandString, System.StringComparison.Ordinal));
            if (enteredMenu != null)
            {
                if (enteredMenu.hackable)
                {
                    currentCommandMenu = enteredMenu;
                    if (!currentCommandMenu.alreadyHacked)
                    {
                        ShowHacking();
                        return;
                    }
                    else
                    {
                        //Make sure that the other commands don't enter a password if
                        //the menu has already been hacked.
                        if(currentScreenType == ScreenType.Password)
                            PasswordEnter();
                    }
                }
                ShowMenu(enteredMenu);
                computerAudioSource.PlayOneShot(acceptSound);
            }
            //if user entered "list" command
            else if (commandString.Equals(StringManager.listCmd, System.StringComparison.Ordinal))
            {
                ShowMenu(currentCommandMenu);
            }
            //else the user entered an invalid command.
            else
            {
                computerAudioSource.PlayOneShot(errorSound);
                ShowErrorText();
            }
        }
        //Reset the command input field.
        ResetCommandText();
        SelectCommandText();
    }
    #endregion

    #region Show Menu Methods
    void ShowEmailMenu()
    {
        //TODO: --ENTER EMAIL MENU IMPLEMENTATION HERE--
        currentScreenType = ScreenType.EmailMenu;
        titleText.text = emailInfo.emailTitle;
        EventManager.TriggerEvent(emailEventString, true);

        //if (passCaretObject.activeInHierarchy)
        //    passCaretObject.SetActive(false);

        //Set the command input text deactivate.
        //Create an extra panel for email instructions to activate.
    }

    //Display the correct menu & commands.
    void ShowMenu(MenuCommand openMenu)
    {
        //if (passCaretObject.activeInHierarchy)
        //{
        //    EventManager.TriggerEvent(passEventString, false);
        //    //passCaretObject.SetActive(false);
        //    cmdCaretObject.SetActive(true);
        //}

        currentCommandMenu = openMenu;
        SetTitleText();
        menuListText.text = currentCommandMenu.displayText;
        commandListText.text = currentCommandMenu.commandsDisplayText;
        //EventManager.TriggerEvent(displayEventString, false);
        displayTextCanvas.alpha = 0;
        //EventManager.TriggerEvent(menuEventString, true);
        currentScreenType = ScreenType.Normal;
    }
    #endregion

    #region Hacking Methods
    void ShowHacking()
    {
        currentScreenType = ScreenType.Password;
        EventManager.TriggerEvent("Activate" + commandCanvas.name, currentScreenType);
        mainText.text = null;

        //Separate logic if the password has already been entered before &
        //does not require hacking if so.

        computerAudioSource.PlayOneShot(errorSound);

        //Set title text to "Password required" & subtitle text to nothing.
        titleText.text = StringManager.passReq;
        subtitleText.text = null;

        //Set the password input panel to appear, & other menu stuff to disappear.
        //EventManager.TriggerEvent(passEventString, true);
        //EventManager.TriggerEvent(menuEventString, false);

        //Reset the command input stuff after entering the hacking menu.
        ResetCommandText();
        //Disable the cmdCaret & show the password caret.
        //passCaretObject.SetActive(true);
        cmdCaretObject.SetActive(false);

        //Set the password input field as the active UI element.
        passwordText.Select();
        passwordText.ActivateInputField();
    }

    void StartHacking()
    {
        isHacking = true;
        computerAudioSource.PlayOneShot(typingSound);
        int passwordLength = currentCommandMenu.password.Length;
        StartCoroutine(GetRandPassword(passwordLength));
    }

    //This creates the effect of the letters in the password
    //being randomized while the hacking is attempted.
    IEnumerator GetRandPassword(int waitTime)
    {
        float beginningTime = Time.time;
        while (true)
        {
            if(Time.time - (beginningTime + 1) > currentCommandMenu.password.Length)
                break;
            else
                RandomizeLetters((int)(Time.time - beginningTime));

            yield return new WaitForSeconds(0.0f);
        }
        //Put the stop for the typing sound here, so it stops pretty much on time.
        if (computerAudioSource.isPlaying)
            computerAudioSource.Stop();
        isHacking = false;
        PasswordEnter(passwordText.text);
    }

    void RandomizeLetters(int index = 0)
    {
        passwordText.text = currentCommandMenu.password.Substring(0, index) + GetRandomLetters(currentCommandMenu.password.Length - index);
    }

    string GetRandomLetters(int wordLength)
    {
        string builtString = StringManager.emptyString;
        for(int i = 0; i < wordLength; i++)
        {
            int index = UnityEngine.Random.Range(0, 26);
            builtString += StringManager.alpha[index];
        }
        return builtString;
    }
    #endregion

    #region Change Text Methods
    private void ShowErrorText()
    {
        //Play error sound when error text is displayed.
        mainText.text = StringManager.errorText;
        //EventManager.TriggerEvent(displayEventString, true);
        displayTextCanvas.alpha = 1;
        titleText.text = StringManager.helpTitle;
        subtitleText.text = StringManager.emptyString;
        errorDisplay = true;
    }

    //Change the title based on what menu the user is on.
    void SetTitleText()
    {
        titleText.text = currentCommandMenu.menuTitle;
        subtitleText.text = currentCommandMenu.menuSubtitle;
        menuTitleText.text = currentCommandMenu.menuPanelTitle;
    }

    void ChangeEmailTitleText()
    {
        emailTitleText.text = StringManager.emailTitleYou + emailInfo.totalEmails +
            StringManager.emailTitleNum + emailInfo.unreadEmails + StringManager.emailTitleUnread;
    }

    //Uses char array for best performance.
    string CapitalizeMenuName(string menuName)
    {
        char[] a = menuName.ToCharArray();
        a[0] = char.ToUpper(a[0]);
        return new string(a);
    }
    #endregion

    #region List Command Methods
    //Returns a string that contains all the menu commands from a list.
    string MenusList(MenuCommand currentMenu)
    {
        string menuString = null;
        for (int i = 0; i < menus.Count; i++)
        {
            if (!menus[i].commandText.Equals(currentMenu.commandText, System.StringComparison.Ordinal))
                menuString += StringManager.menuIndent + menus[i].commandText + StringManager.newLine;
        }
        return menuString.Replace(StringManager.menuIndent + currentMenu.commandText + StringManager.newLine, StringManager.emptyString);
    }

    //Returns a string that contains all the commands from a list.
    string CommandsList(List<ComputerCommand> commandList)
    {
        string commandString = null;
        for (int i = 0; i < commandList.Count; i++)
        {
            commandString += StringManager.menuIndent + commandList[i].commandText + StringManager.newLine;
        }
        return commandString;
    }
    #endregion

    #region Get Info Methods
    //TODO: GetEmailInfo() Email info needs to received for the computer.
    void GetEmailInfo()
    {
        //Not yet implemented.
        //This method will get info on
        //whether emails have been received, deleted, read, etc.

        //Email info has to be serialized & saved.
        //I don't have all of that persistent data stuff yet.
    }

    //TODO: GetHackInfo() Hack info needs to received for the computer.
    void GetHackInfo()
    {
        //Not yet implemented.
        //This method will get info on
        //whether the user has input passwords/hacked the menus.

        //Computer hack info has to be serialized & saved.
        //I don't have all of that persistent data stuff yet.
    }
    #endregion

    #region Input Field Methods
    //Resets the command text input field.
    void ResetCommandText()
    {
        commandText.interactable = true;
        commandText.text = null;
        cmdCaret.Reset();
    }

    void ResetPasswordText()
    {
        passwordText.interactable = true;
        passwordText.text = null;
        //if(passCaret.enabled)
        //    passCaret.Reset();
    }

    void SelectCommandText()
    {
        commandText.Select();
        commandText.ActivateInputField();
    }

    void SelectPasswordText()
    {
        passwordText.Select();
        passwordText.ActivateInputField();
    }
    #endregion

    //#region Screen Saver Methods
    //Returns a random position on a computer screen for the screen saver.
    //Vector2 RandomScreenPosition()
    //{
    //    //The numbers 140 and 65 are determined from the size of the screen saver text box. They should stay constant across all computers.
    //    int posX = UnityEngine.Random.Range((int)-((rectTransform.rect.width / 2) - 140), (int)((rectTransform.rect.width / 2) - 140));
    //    int posY = UnityEngine.Random.Range((int)-((rectTransform.rect.height / 2) - 65), (int)((rectTransform.rect.height / 2) - 65));
    //    screenSaverPos.x = posX;
    //    screenSaverPos.y = posY;
    //    return screenSaverPos;
    //}

    //Coroutine for running the screen saver behaviour.
    //IEnumerator MoveScreenSaver()
    //{
    //    moveSaverTime = false;
    //    int randomColor = UnityEngine.Random.Range(0, 2);
    //    if (randomColor == 1)
    //    {
    //        ScreenSaverBlack.SetActive(false);
    //        ScreenSaverWhite.SetActive(true);
    //    }
    //    else
    //    {
    //        ScreenSaverBlack.SetActive(true);
    //        ScreenSaverWhite.SetActive(false);
    //    }
    //    screenSaverTransform.anchoredPosition = RandomScreenPosition();
    //    yield return screenSaverDelay;
    //    moveSaverTime = true;
    //}
    //#endregion

    #endregion

    void CreateEmailText()
    {
        //Need to create 2 objects for each email in list.
        //1 for the number of the email in the list,
        //and the second for the email subject text.
        //They're different because the number is what marks
        //it read/unread by changing text and background color.

        RectTransform numberRect = numberText.GetComponent<RectTransform>();
        RectTransform subjectRect = subjectText.GetComponent<RectTransform>();

        Vector3 emailListPos = emailPanel.transform.position;
        Quaternion emailListRot = emailPanel.transform.rotation;
        

        for (int i = 0; i < emailCommand.emailCommands.Count; i++)
        {
            string emailIndex = StringManager.leftBr + (i+1) + StringManager.rightBr;
            subjectText.GetComponent<Text>().text = emailCommand.emailCommands[i].subject;
            Text emailNumText;
            Text emailSub;
            RawImage emailNum;

            if (i > 0)
            {
                emailNum = Instantiate(numberText, emailPanel.transform);
                emailNum.rectTransform.anchoredPosition += new Vector2(0.0f, -emailNum.rectTransform.rect.height * i);
                emailSub = Instantiate(subjectText,emailPanel.transform);
                emailSub.rectTransform.anchoredPosition += new Vector2(0.0f, -emailSub.rectTransform.rect.height * i);
            }
            else
            {
                emailCommand.emailCommands[i].read = true;
                emailNum = Instantiate(numberText, emailPanel.transform);
                emailSub = Instantiate(subjectText, emailPanel.transform);
            }

            emailNumText = emailNum.transform.GetComponentInChildren<Text>();
            emailNumText.text = emailIndex;

            if (!emailCommand.emailCommands[i].read)
            {
                emailNum.color = Color.white;
                emailNumText.color = Color.black;
            }
        }
        emailPanel.SetActive(false);
    }
}

//Use this to determine behavior when pressing ENTER & ESC.
//Better than using several booleans.
public enum ScreenType
{
    None,
    Normal,
    Password,
    PasswordFail,
    Email,
    EmailMenu,
    DisplayText
}