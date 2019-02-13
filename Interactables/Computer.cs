using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO: Add functionality for audio source. - only email audio stuff not done.
//TODO: #1 priority, change event manager stuff to game event scriptable object system.
//TODO: Add logic for email, separate menu type. (SO)

public class CanvasGroups
{
    List<CanvasGroup> canvasgroups;
}

/// <summary>
/// The class to use for interactive computers.
/// </summary>
public class Computer : Interactable
{
    #region Variables

    #region Common Scriptable Objects
    [Header("Common Computer SOs")]
    public CommonCompStrings commonStrings;
    public CommonCompCommands commands;
    #endregion

    #region Menus, Commands, Emails
    [Header("Specific Computer SOs")]

    //List of possible commands at the home menu.
    //Defined in the inspector (SO) depending on which computer.
    //May have other commands at home menu other than default commands,
    //so I'm leaving this field in here to be defined later if that happens.
    //public HomeCommands homeCommands;

    //List of possible menus.
    //Defined in the inspector (SO) depending on which computer.
    [SerializeField]
    private MenuCommandList menus;

    //List of the emails for the computer. (SO)
    [SerializeField]
    private EmailCommandList emailInfo;

    private EmailMenuCommand emailCommand;
    #endregion

    //#region Panel GameObjects
    //[Header("Panels")]
    //public GameObject emailPanel;
    //#endregion

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
    //Variable to store the current menu.
    private MenuCommand currentCommandMenu;
    #endregion

    #region Rect Transforms
    //Need this to determine width & height of screen.
    private RectTransform rectTransform;
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
    public ComputerSounds computerSounds;
    public AudioSource computerAudioSource;

    #endregion

    #region Other Components
    //Boolean to tell other things if the player is using the computer currently.
    public static bool usingComputer = false;
    private bool displayText = false;
    private bool isHacking = false;

    private ScreenType currentScreenType = ScreenType.Menu;

    [Header("Other Variables")]
    public RawImage numberText;
    public Text subjectText;

    #endregion

    #region Game Events
    [Header("Game Events")]
    public GameEvent activate;
    public GameEvent deactivate;
    public GameEvent menuScreen;
    public GameEvent displayScreen;
    public GameEvent passwordScreen;

    #endregion

    //#region EventNames
    //private string titleEventString, passEventString,
    //               displayEventString, menuEventString,
    //               emailEventString, exitEventString;
    //#endregion

    #endregion

    #region Methods

    #region MonoBehaviour Methods

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

        //Use this to get the width & height of screen.
        //Needed for the screen saver routine.
        rectTransform = gameObject.GetComponent<RectTransform>();

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

        //Set up the home menu command variables.
        //homeCommand = new MenuCommand();
        //homeCommand.menuTitle = homeCommandTitle;
        //homeCommand.menuPanelTitle = CapitalizeMenuName(homeCommand.commandText) + commonStrings.miscDict[CommonCompStrings.Misc.MenuTitleSuffix];
        //homeCommand.menuSubtitle = homeCommandSubtitle;
        //homeCommand.subCommands = new List<ComputerCommand>(commands.commandDict.Values);

        //Insert the email & home commands,
        //if the email comamnd should exist.
        //Then set the current menu to the home menu.
        if (emailInfo.hasEmail)
        {
            //Email menu should always be the first in the list.
            emailCommand = new EmailMenuCommand();
            //menus.Commands.Insert(0, emailCommand);

            //Populate the email list.
            emailCommand.AssignEmails(emailInfo.Commands);
            //Set the email menu password.
            emailCommand.password = emailInfo.password;

            //Home menu should always be after the email menu, if email menu exists.
            //menus.Commands.Insert(1, homeCommand);
            //Set the current menu to the home menu.
            currentCommandMenu = menus.Commands[1];

            //Set the email text to the correct format.
            ChangeEmailTitleText();

            CreateEmailText();
        }
        else
        {
            //If no email menu exists for the computer, then assign
            //the home menu to be first in the list, and set as current menu.
            //menus.Commands.Insert(0, homeCommand);
            currentCommandMenu = menus.Commands[0];
        }

        //Populate the menu list for the home command menu.
        //homeCommand.displayText = MenusList(currentCommandMenu);

        //Set the display text for all the menus at start,
        //instead of running them at activation. This will save memory in the long run.
        //for(int i = 0; i < menus.Commands.Count; i++)
        //{
        //    //If the menu is not email menu, then populate the commands list string
        //    //from the subcommands of the menu.
        //    if(!menus.Commands[i].commandText.Equals(commonStrings.cmdDict[CommonCompStrings.Command.Email], System.StringComparison.Ordinal))
        //        menus.Commands[i].commandsDisplayText = CommandsList(menus.Commands[i].subCommands);
        //}
    }

    void Update()
	{
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
                        //It needs to be in this order.
                        ResetCommandText();
                        SelectCommandText();
                        if (emailInfo.hasEmail)
                            ShowMenu(menus.Commands[1]);
                        else
                            ShowMenu(menus.Commands[0]);
                        break;
                    case ScreenType.Email:
                        ShowEmailMenu();
                        break;
                    case ScreenType.Menu:
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
                        if(currentCommandMenu.commandText.Equals(commonStrings.cmdDict[CommonCompStrings.Command.Email], System.StringComparison.Ordinal))
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
        //Raise activate game event (SO).
        activate.Raise();

        //Play access sound when activated.
        computerAudioSource.PlayOneShot(computerSounds.audioDict[ComputerSounds.Clips.Access]);

        //EventManager.TriggerEvent(titleEventString, true);
        cmdCaretObject.SetActive(true);

        //Set this static variable so other scripts know the user
        //is using a computer.
        usingComputer = true;

        //Select the input box so that the user doesn't
        //have to select it manually.
        SelectCommandText();

        //Show the home menu on activation.
        if (emailInfo.hasEmail)
            ShowMenu(menus.Commands[1]);
        else
            ShowMenu(menus.Commands[0]);
    }

    //User has exited the computer.
    void ExitScreen()
    {
        //Raise deactivate game event (SO).
        deactivate.Raise();

        ResetCommandText();
        ResetPasswordText();

        //EventManager.TriggerEvent(exitEventString);
        cmdCaret.gameObject.SetActive(false);

        //Set the current menu back to the home menu.
        if (emailInfo.hasEmail)
            currentCommandMenu = menus.Commands[1];
        else
            currentCommandMenu = menus.Commands[0];

        //Set the static variable so other scripts know the user
        //is no longer using a computer.
        usingComputer = false;

        RigidbodyFirstPersonController.frozen = false;
    }
    #endregion

    #region Text Input Methods
    void PasswordEnter(string enteredPassword = null)
    {
        //Password accepted.
        if (enteredPassword == currentCommandMenu.password)
        {
            computerAudioSource.PlayOneShot(computerSounds.audioDict[ComputerSounds.Clips.Accept]);

            //set the hacked to true, so user doesn't have to hack again.
            currentCommandMenu.alreadyHacked = true;

            //Set the display text panel to show the appropriate message.
            mainText.text = commonStrings.passDict[CommonCompStrings.Password.Accepted] +
                            currentCommandMenu.password + 
                            commonStrings.charDict[CommonCompStrings.Char.Greater] +
                            commonStrings.charDict[CommonCompStrings.Char.NewLine] +
                            commonStrings.passDict[CommonCompStrings.Password.Entering] +
                            currentCommandMenu.commandText +
                            commonStrings.charDict[CommonCompStrings.Char.Period];
            //Set the title text to the appropriate message.
            titleText.text = commonStrings.passDict[CommonCompStrings.Password.Success];

            //Hide the password input, need to show the Press Enter prompt after password succeeds.
            //The command input sets to proper display text when the display panel is toggled to true.
            //EventManager.TriggerEvent(passEventString, false);
            //EventManager.TriggerEvent(displayEventString, true);
            displayTextCanvas.alpha = 1;
            currentScreenType = ScreenType.DisplayText;
            ResetPasswordText();
        }
        //Password failed.
        else
        {
            titleText.text = commonStrings.passDict[CommonCompStrings.Password.Fail];
            computerAudioSource.PlayOneShot(computerSounds.audioDict[ComputerSounds.Clips.Error]);

            //EventManager.TriggerEvent(passEventString, false);
            //EventManager.TriggerEvent(displayEventString, true);
            displayTextCanvas.alpha = 1;
            ResetPasswordText();
            currentScreenType = ScreenType.PasswordFail;
        }
        EventManager.TriggerEvent("State" + commandCanvas.name, currentScreenType);
    }

    //The method that runs when a command is entered.
    public void CommandEnter()
    {
        ComputerCommand enteredCommand;
        string commandString = commandText.text;

        //If user is on a display page
        if(displayText)
        {
            //If the user presses enter on the display page.
            if (Input.GetKey(KeyCode.Return))
            {
                commandString = currentCommandMenu.commandText;
                ResetCommandText();
                SelectCommandText();
                displayText = false;
            }
        }

        if(commandString == commonStrings.cmdDict[CommonCompStrings.Command.Quit])
        { ExitScreen(); return; }
        if(commandString == commonStrings.cmdDict[CommonCompStrings.Command.Help])
        { ShowHelpText(); ResetCommandText(); SelectCommandText(); return; }

        enteredCommand = currentCommandMenu.subCommands.Find(x => x.commandText.Equals(commandString, System.StringComparison.Ordinal));
        if (enteredCommand != null)
        {
            mainText.text = enteredCommand.displayText;

            currentScreenType = ScreenType.DisplayText;            

            commandText.interactable = false;
            displayText = true;
            displayScreen.Raise();
            return;
        }
        else
        {
            MenuCommand enteredMenu = menus.Commands.Find(x => x.commandText.Equals(commandString, System.StringComparison.Ordinal));
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
                computerAudioSource.PlayOneShot(computerSounds.audioDict[ComputerSounds.Clips.Accept]);
            }
            //if user entered "list" command
            else if (commandString.Equals(commonStrings.cmdDict[CommonCompStrings.Command.List], System.StringComparison.Ordinal))
            {
                ShowMenu(currentCommandMenu);
            }
            //else the user entered an invalid command.
            else
            {
                computerAudioSource.PlayOneShot(computerSounds.audioDict[ComputerSounds.Clips.Error]);
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
        //titleText.text = commonStrings.emailDict[CommonCompStrings.Email.Prefix] + ;
        //TODO: convert to email canvas change.
        //EventManager.TriggerEvent(emailEventString, true);

        //if (passCaretObject.activeInHierarchy)
        //    passCaretObject.SetActive(false);

        //Set the command input text deactivate.
        //Create an extra panel for email instructions to activate.
    }

    //Display the correct menu & commands.
    void ShowMenu(MenuCommand openMenu)
    {
        currentCommandMenu = openMenu;
        SetTitleText();
        menuListText.text = currentCommandMenu.displayText;
        commandListText.text = currentCommandMenu.commandsDisplayText;
        currentScreenType = ScreenType.Menu;

        menuScreen.Raise();
    }
    #endregion

    #region Hacking Methods
    void ShowHacking()
    {
        currentScreenType = ScreenType.Password;
        EventManager.TriggerEvent("State" + commandCanvas.name, currentScreenType);
        //if (menuCanvas.alpha == 1)
        //    menuCanvas.alpha = 0;
        mainText.text = null;

        computerAudioSource.PlayOneShot(computerSounds.audioDict[ComputerSounds.Clips.Error]);

        //Set title text to "Password required" & subtitle text to nothing.
        titleText.text = commonStrings.passDict[CommonCompStrings.Password.Required];
        subtitleText.text = null;

        passwordScreen.Raise();

        //Reset the command input stuff after entering the hacking menu.
        ResetCommandText();

        commandText.Select();
        commandText.ActivateInputField();
    }

    void StartHacking()
    {
        isHacking = true;
        cmdCaret.gameObject.SetActive(false);
        computerAudioSource.PlayOneShot(computerSounds.audioDict[ComputerSounds.Clips.Typing]);
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
        string builtString = commonStrings.charDict[CommonCompStrings.Char.Empty];
        for(int i = 0; i < wordLength; i++)
        {
            int index = UnityEngine.Random.Range(0, 26);
            builtString += commonStrings.miscDict[CommonCompStrings.Misc.Alphabet][index];
        }
        return builtString;
    }
    #endregion

    #region Change Text Methods
    private void ShowErrorText()
    {
        //Play error sound when error text is displayed.
        mainText.text = commonStrings.errorDict[CommonCompStrings.Error.Text];
        displayTextCanvas.alpha = 1;
        titleText.text = commonStrings.errorDict[CommonCompStrings.Error.Title];
        subtitleText.text = commonStrings.charDict[CommonCompStrings.Char.Empty];
        displayText = true;

        displayScreen.Raise();
    }

    private void ShowHelpText()
    {

        //TODO: NOT YET IMPLEMENTED - There's a difference between help & error text (invalid command screen).
        
        //Play error sound when error text is displayed.
        //mainText.text = commonStrings.helpDict[CommonCompStrings.Error.Text];
        ////EventManager.TriggerEvent(displayEventString, true);
        //displayTextCanvas.alpha = 1;
        //titleText.text = commonStrings.helpDict[CommonCompStrings.Error.Title];
        //subtitleText.text = commonStrings.charDict[CommonCompStrings.Char.Empty];
        //displayText = true;
    }

    //Change the title based on what menu the user is on.
    void SetTitleText()
    {
        //titleText.text = currentCommandMenu.menuTitle;
        //subtitleText.text = currentCommandMenu.menuSubtitle;
        //menuTitleText.text = currentCommandMenu.menuPanelTitle;
    }

    void ChangeEmailTitleText()
    {
        emailTitleText.text = commonStrings.emailDict[CommonCompStrings.Email.TitleYou] + emailInfo.GetEmailCount() +
            commonStrings.emailDict[CommonCompStrings.Email.TitleNum] + emailInfo.GetUnreadCount() + commonStrings.emailDict[CommonCompStrings.Email.TitleUnread];
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
        for (int i = 0; i < menus.Commands.Count; i++)
        {
            if (!menus.Commands[i].commandText.Equals(currentMenu.commandText, System.StringComparison.Ordinal))
                menuString += commonStrings.cmdDict[CommonCompStrings.Command.MenuIndent] +
                    menus.Commands[i].commandText + commonStrings.charDict[CommonCompStrings.Char.NewLine];
        }
        return menuString.Replace(commonStrings.cmdDict[CommonCompStrings.Command.MenuIndent] + currentMenu.commandText +
               commonStrings.charDict[CommonCompStrings.Char.NewLine], commonStrings.charDict[CommonCompStrings.Char.Empty]);
    }

    //Returns a string that contains all the commands from a list.
    string CommandsList(List<ComputerCommand> commandList)
    {
        string commandString = null;
        for (int i = 0; i < commandList.Count; i++)
        {
            commandString += commonStrings.cmdDict[CommonCompStrings.Command.MenuIndent] +
                commandList[i].commandText + commonStrings.charDict[CommonCompStrings.Char.NewLine];
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
        if (commandCanvas.alpha == 0)
            commandCanvas.alpha = 1;
        commandText.Select();
        commandText.ActivateInputField();
    }

    void SelectPasswordText()
    {
        passwordText.Select();
        passwordText.ActivateInputField();
    }
    #endregion

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

        //Vector3 emailListPos = emailPanel.transform.position;
        //Quaternion emailListRot = emailPanel.transform.rotation;
        

        for (int i = 0; i < emailCommand.emailCommands.Count; i++)
        {
            string emailIndex = commonStrings.charDict[CommonCompStrings.Char.LBracket] + (i+1) +
                                commonStrings.charDict[CommonCompStrings.Char.RBracket];
            subjectText.GetComponent<Text>().text = emailCommand.emailCommands[i].subject;
            Text emailNumText;
            Text emailSub;
            RawImage emailNum;

            if (i > 0)
            {
                //emailNum = Instantiate(numberText, emailPanel.transform);
                //emailNum.rectTransform.anchoredPosition += new Vector2(0.0f, -emailNum.rectTransform.rect.height * i);
                //emailSub = Instantiate(subjectText,emailPanel.transform);
                //emailSub.rectTransform.anchoredPosition += new Vector2(0.0f, -emailSub.rectTransform.rect.height * i);
            }
            else
            {
                emailCommand.emailCommands[i].read = true;
                //emailNum = Instantiate(numberText, emailPanel.transform);
                //emailSub = Instantiate(subjectText, emailPanel.transform);
            }

            //emailNumText = emailNum.transform.GetComponentInChildren<Text>();
            //emailNumText.text = emailIndex;

            if (!emailCommand.emailCommands[i].read)
            {
                //emailNum.color = Color.white;
                //emailNumText.color = Color.black;
            }
        }
        //emailPanel.SetActive(false);
    }
}

//Use this to determine behavior when pressing ENTER & ESC.
//Better than using several booleans.
public enum ScreenType
{
    None,
    Menu,
    Password,
    PasswordFail,
    PasswordSucceed,
    Email,
    EmailMenu,
    DisplayText,
    Error,
    Help
}