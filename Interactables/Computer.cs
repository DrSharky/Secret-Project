using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO: Add functionality for audio source. - only email audio stuff not done.
//TODO: Add logic for email, separate menu type. (SO)

/// <summary>
/// The class to use for interactive computers.
/// </summary>
public class Computer : Interactable
{
    #region Variables

    #region Common Scriptable Objects
    [Header("Common Computer SOs")]
    public ComputerSounds computerSounds;
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

    [SerializeField]
    private DisplayEventsList displayList;
    #endregion

    #region Text Mesh Pro InputFields
    [Header("TM_Pro InputFields")]
    //The input field for the text input gameobject.
    [SerializeField]
    //private InputField commandText;
    private TMPro.TMP_InputField commandText;

    #endregion

    #region Command Fields
    //Variable to store the current menu.
    private MenuCommand currentCommandMenu;
    [SerializeField]
    private GameObject cmdCaretObject;
    #endregion

    #region Audio Components
    [Header("Audio Components")]
    //Audio source for the computer.
    //Default clip is accessSound.
    public AudioSource computerAudioSource;

    #endregion

    #region Other Components
    //Boolean to tell other things if the player is using the computer currently.
    public static bool usingComputer = false;
    private bool displayText = false;
    private bool isHacking = false;

    private ScreenType currentScreenType = ScreenType.Menu;

    //Make a reference to the pw coroutine so we can properly stop
    //it from running if the user presses escape during hacking.
    private IEnumerator randPasswordCoroutine;

    #endregion

    #region Game Events
    [Header("Game Events")]
    public GameEvent activate;
    public GameEvent deactivate;
    public GameEvent displayScreen;
    public GameEvent helpScreen;
    public GameEvent errorScreen;
    public GameEvent passwordScreen;
    public GameEvent passwordSuccScreen;
    public GameEvent passwordFailScreen;
    public GameEvent emailMenuScreen;
    public GameEvent menuEvent;
    public List<GameEvent> menuScreens;
    public GameEvent errorString;
    public GameEvent emailEvent;
    public GameEvent emailPage;
    public GameEvent emailDelete;
    private int emailIndex;
    private int emailPageIndex = 0;

    #endregion

    #endregion

    #region Methods

    #region MonoBehaviour Methods

    void Start()
	{
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
        }
        else
        {
            //If no email menu exists for the computer, then assign
            //the home menu to be first in the list, and set as current menu.
            //menus.Commands.Insert(0, homeCommand);
            currentCommandMenu = menus.Commands[0];
        }
    }

    void Update()
	{
        if (usingComputer)
        {
            //If the user presses Esc & is using the computer
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                switch (currentScreenType)
                {
                    case ScreenType.EmailMenu:
                    case ScreenType.Password:
                        commandText.text = null;
                        if(randPasswordCoroutine != null)
                            StopCoroutine(randPasswordCoroutine);
                        isHacking = false;
                        if (computerAudioSource.isPlaying)
                            computerAudioSource.Stop();
                        if (emailInfo.hasEmail)
                            ShowMenu(menus.Commands[1]);
                        else
                            ShowMenu(menus.Commands[0]);
                        break;
                    //Note: pressing Esc in Email doesn't work in editor, but works in a build.
                    //This isn't broken, it just has weird behaviour in play mode.
                    case ScreenType.Email:
                        ShowEmailMenu();
                        break;
                    case ScreenType.Menu:
                        ExitScreen();
                        break;
                    case ScreenType.DisplayText:
                        ShowMenu(currentCommandMenu);
                        break;
                }
            }
            //keep isHacking here, or at least keep the order of "check escape key, then isHacking".
            //This way, user can cancel hacking with escape, but can't otherwise interrupt the
            //hacking process with other keystrokes.
            else if (isHacking)
                return;
            else if(currentScreenType == ScreenType.Email)
            {
                switch (Input.inputString)
                {
                    case "q":
                        ShowEmailMenu();
                        break;
                    case "d":
                        emailDelete.sentInt = emailIndex;
                        emailDelete.Raise();
                        emailDelete.sentInt = 0;
                        ShowEmailMenu();
                        break;
                    case "n":
                        commandText.text = null;
                        if (emailIndex < emailInfo.Commands.Count - 1)
                        {
                            emailIndex++;
                            if ((emailIndex % 10) == 0)
                                emailPageIndex += 10;
                            emailEvent.sentInt = emailIndex;
                            emailEvent.Raise();
                            emailEvent.sentInt = 0;
                        }
                        break;
                    case "p":
                        commandText.text = null;
                        if (emailIndex > 0)
                        {
                            emailIndex--;
                            if ((emailIndex - 9) % 10 == 0)
                                emailPageIndex = 0;
                            emailEvent.sentInt = emailIndex;
                            emailEvent.Raise();
                            emailEvent.sentInt = 0;
                        }
                        break;
                    default:
                        commandText.text = null;
                        break;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                if(currentScreenType == ScreenType.Menu && commandText.text.Equals(CommonCompStrings.charDict[CommonCompStrings.Char.Empty], System.StringComparison.Ordinal))
                {
                    computerAudioSource.PlayOneShot(computerSounds.audioDict[ComputerSounds.Clips.Error]);
                    ShowErrorText();
                    return;
                }

                if(currentScreenType == ScreenType.EmailMenu)
                {
                    switch (commandText.text)
                    {
                        case "p":
                            if (emailPageIndex < 10)
                                break;
                            else
                                emailPageIndex -= 10;
                            emailPage.sentInt = emailPageIndex;
                            commandText.text = null;
                            emailPage.Raise();
                            emailPage.sentInt = 0;
                            break;
                        case "n":
                            if ((emailPageIndex + 10) > emailInfo.GetEmailCount())
                                break;
                            else
                                emailPageIndex += 10;
                            commandText.text = null;
                            emailPage.sentInt = emailPageIndex;
                            emailPage.Raise();
                            emailPage.sentInt = 0;
                            break;
                        case "q":
                            commandText.text = null;
                            emailIndex = 0;
                            emailPageIndex = 0;
                            ShowMenu(menus.Commands.Find(x => x.commandText == "home"));
                            break;
                        //Using default case for numbers since we aren't sure the limits of the numbers.
                        //Use default and try to parse the number if possible.
                        default:
                            if(int.TryParse(commandText.text, out emailIndex))
                            {
                                emailEvent.sentInt = emailIndex;
                                emailEvent.Raise();
                                emailEvent.sentInt = 0;
                                currentScreenType = ScreenType.Email;
                            }
                            else
                            {
                                emailPage.sentInt = emailPageIndex;
                                emailPage.Raise();
                                emailPage.sentInt = 0;
                            }
                            commandText.text = null;
                            break;
                    }
                }
                else if (currentScreenType == ScreenType.Password)
                {
                    if (!currentCommandMenu.alreadyHacked)
                        PasswordEnter(commandText.text);
                    else
                    {
                        //PasswordEnter(currentCommandMenu.password);
                        //SelectCommandText();
                        //if(currentCommandMenu.commandText.Equals(CommonCompStrings.cmdDict[CommonCompStrings.Command.Email], System.StringComparison.Ordinal))
                        //    ShowEmailMenu();
                        //else
                        //    ShowMenu(currentCommandMenu);
                    }
                }
                else if(currentScreenType == ScreenType.PasswordFail)
                    ShowHacking();
                else if(currentScreenType == ScreenType.PasswordSucceed)
                {
                    if (currentCommandMenu.commandText.Equals(CommonCompStrings.cmdDict[CommonCompStrings.Command.Email],
                        System.StringComparison.Ordinal))
                        ShowEmailMenu();
                    else
                        ShowMenu(currentCommandMenu);
                }
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

        //Set this static variable so other scripts know the user
        //is using a computer.
        usingComputer = true;
    }

    //User has exited the computer.
    void ExitScreen()
    {
        //Set the current menu back to the home menu.
        if (emailInfo.hasEmail)
            currentCommandMenu = menus.Commands[1];
        else
            currentCommandMenu = menus.Commands[0];

        menuScreens.Find(x => x.sentString == "home").Raise();

        //Raise deactivate game event (SO).
        deactivate.Raise();

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

            menuScreens.Find(x => x.sentString == currentCommandMenu.commandText).Raise();

            //if (currentCommandMenu.commandText.Equals(CommonCompStrings.cmdDict[CommonCompStrings.Command.Email], System.StringComparison.Ordinal))
            //{
            //    ShowEmailMenu();
            //}

            //set the hacked to true, so user doesn't have to hack again.
            currentCommandMenu.alreadyHacked = true;

            currentScreenType = ScreenType.PasswordSucceed;
            displayScreen.Raise();
            passwordSuccScreen.Raise();
        }
        //Password failed.
        else
        {
            computerAudioSource.PlayOneShot(computerSounds.audioDict[ComputerSounds.Clips.Error]);
            currentScreenType = ScreenType.PasswordFail;
            passwordFailScreen.Raise();
        }
    }

    //The method that runs when a command is entered.
    public void CommandEnter()
    {
        ComputerCommand enteredCommand;
        string commandString = commandText.text;

        if(currentScreenType == ScreenType.Error)
        {
            ShowMenu(currentCommandMenu);
            return;
        }

        //If user is on a display page
        if(currentScreenType == ScreenType.DisplayText || currentScreenType == ScreenType.Error  || currentScreenType == ScreenType.Help)
        {
            //If the user presses enter on the display page.
            if (Input.GetKey(KeyCode.Return))
            {
                commandString = currentCommandMenu.commandText;
                //SelectCommandText();
                displayText = false;
                ShowMenu(currentCommandMenu);
                return;
            }
        }

        if(commandString == CommonCompStrings.cmdDict[CommonCompStrings.Command.Quit])
        { ExitScreen(); return; }
        if(commandString == CommonCompStrings.cmdDict[CommonCompStrings.Command.Help])
        { ShowHelpText(); return; }

        enteredCommand = currentCommandMenu.subCommands.Find(x => x.commandText.Equals(commandString, System.StringComparison.Ordinal));
        if (enteredCommand != null)
        {
            //mainText.text = enteredCommand.displayText;

            currentScreenType = ScreenType.DisplayText;            

            commandText.interactable = false;
            displayText = true;
            displayList.Events.Find(x => x.sentString == commandText.text).Raise();
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
                        PasswordEnter(currentCommandMenu.password);
                        return;

                        //Make sure that the other commands don't enter a password if
                        //the menu has already been hacked.
                        //if (currentScreenType == ScreenType.Password)
                        //{
                        //    return;
                        //}
                    }
                }
                else
                {
                    if (currentScreenType != ScreenType.DisplayText && currentScreenType != ScreenType.Error
                    && currentScreenType != ScreenType.Help)
                        computerAudioSource.PlayOneShot(computerSounds.audioDict[ComputerSounds.Clips.Accept]);

                    if (currentCommandMenu.commandText.Equals(CommonCompStrings.cmdDict[CommonCompStrings.Command.Email],
                    System.StringComparison.Ordinal))
                        ShowEmailMenu();
                    else
                        ShowMenu(enteredMenu);
                }
            }
            //if user entered "list" command
            else if (commandString.Equals(CommonCompStrings.cmdDict[CommonCompStrings.Command.List], System.StringComparison.Ordinal))
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
    }
    #endregion

    #region Show Menu Methods
    void ShowEmailMenu()
    {
        currentScreenType = ScreenType.EmailMenu;
        commandText.text = null;
        emailMenuScreen.Raise();
    }

    //Display the correct menu & commands.
    void ShowMenu(MenuCommand openMenu)
    {
        currentCommandMenu = openMenu;
        currentScreenType = ScreenType.Menu;

        menuScreens.Find(x => x.sentString == openMenu.commandText).Raise();
        menuEvent.Raise();
    }
    #endregion

    #region Hacking Methods
    void ShowHacking()
    {
        currentScreenType = ScreenType.Password;
        computerAudioSource.PlayOneShot(computerSounds.audioDict[ComputerSounds.Clips.Error]);
        passwordScreen.Raise();
    }

    void StartHacking()
    {
        isHacking = true;
        cmdCaretObject.SetActive(false);
        computerAudioSource.PlayOneShot(computerSounds.audioDict[ComputerSounds.Clips.Typing]);
        int passTime = currentCommandMenu.password.Length;
        //Set coroutine value right before calling it, need to know passTime before
        //setting coroutine value, because passTime can be a variable length.
        randPasswordCoroutine = GetRandPassword(passTime);
        StartCoroutine(randPasswordCoroutine);
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
        PasswordEnter(commandText.text);
    }

    void RandomizeLetters(int index = 0)
    {
        commandText.text = currentCommandMenu.password.Substring(0, index) + GetRandomLetters(currentCommandMenu.password.Length - index);
    }

    string GetRandomLetters(int wordLength)
    {
        string builtString = CommonCompStrings.charDict[CommonCompStrings.Char.Empty];
        for(int i = 0; i < wordLength; i++)
        {
            int index = UnityEngine.Random.Range(0, 26);
            builtString += CommonCompStrings.miscDict[CommonCompStrings.Misc.Alphabet][index];
        }
        return builtString;
    }
    #endregion

    #region Change Text Methods
    private void ShowErrorText()
    {
        errorString.sentString = commandText.text;
        currentScreenType = ScreenType.Error;
        displayText = true;
        displayScreen.Raise();
        errorString.Raise();
        errorScreen.Raise();
        errorString.sentString = CommonCompStrings.charDict[CommonCompStrings.Char.Empty];
    }

    private void ShowHelpText()
    {
        currentScreenType = ScreenType.Help;
        displayText = true;
        displayScreen.Raise();
        helpScreen.Raise();
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

    #endregion
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