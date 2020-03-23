using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;
using TMPro;

//Class which handles in game voice controls
public class VoiceControl : MonoBehaviour
{
    #region == Private variables ==
    private KeywordRecognizer speechRecognizer;
    private Dictionary<string, Action> voiceActions = new Dictionary<string, Action>();
    private StartGame startgame;
    private Pause pause;
    private Options options;
    private Boolean nameset=false;
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI progressText;
    public Image loadingimage;
    public Image loadingimageprogress;
    public GameObject namePanel;
    public GameObject tutorialPanel;
    private bool voiceContolsOn;

    private DictationRecognizer dictationRecognizer;
    #endregion

    void Start()
    {
        //set voice controls to false
        voiceContolsOn = false;

        //disable all panel items to be shown when name is interpreted
        playerName.enabled = false;
        progressText.enabled = false;
        loadingimage.enabled = false;
        loadingimageprogress.enabled = false;

        //initialise name to empty string - reset previous rounds name if present
        PlayerPrefs.SetString("Name", "");

        // Get scripts to access methods.
        startgame = gameObject.GetComponent<StartGame>();
        pause = GameObject.Find("Main Camera").GetComponent<Pause>();
        options = GameObject.Find("VikingSoundHorn").GetComponent<Options>();

        // All all voice commands to Dictionary
        AddAllVoiceCommands();

        //set up dictation recogniser to allow user to input name
        dictationRecognizer = new DictationRecognizer();
        //set timeout period for dictation recognizer
        dictationRecognizer.InitialSilenceTimeoutSeconds = 15;
        dictationRecognizer.DictationResult += DictationRecognizer_DictationResult;
        dictationRecognizer.DictationHypothesis += DictationRecognizer_DictationHypothesis;
        dictationRecognizer.Start();

        //added timeout to dictation for name - after 15 seconds disable and enable voice commands
        Invoke("DicationTimeout", 15);

    }

    // Method entered when Dictation recogniser has got a result - https://docs.unity3d.com/ScriptReference/Windows.Speech.DictationRecognizer.html
    private void DictationRecognizer_DictationResult(string name, ConfidenceLevel confidence)
    {
        //get a substring of name less than 9 so that it fits the UI
        if (name.Length > 9) {
            name = name.Substring(0, 9);
        }

        //set user voice returned name in player prefs
        PlayerPrefs.SetString("Name", name);
        playerName.text = name;

        //display name panel after result
        displayNamePanel();

        //after name is set turn off panel after 2 seconds
        Invoke("TurnOffPanel", 2);

        //shutdown dictation recogniser as cannot use at same time as voice controls - 
        ShutDownDictationRecogniser();

        //start speech recognisers for game controls
        SetupSpeechRecogniser();
        voiceContolsOn = true;
    }

    // Method entered when Dictation recogniser is interpretting a result - https://docs.unity3d.com/ScriptReference/Windows.Speech.DictationRecognizer.html
    private void DictationRecognizer_DictationHypothesis(string text)
    {
        progressText.enabled = true;
        loadingimage.enabled = true;
        loadingimageprogress.enabled = true;
    }

    //method to initialise and start sppech recognisers
    private void SetupSpeechRecogniser()
    {
        // Add keywords from dictionary to KeywordRecognizer and start listening for commands.
        speechRecognizer = new KeywordRecognizer(voiceActions.Keys.ToArray());
        speechRecognizer.OnPhraseRecognized += SpeechRecognizer_OnPhraseRecognized;
        speechRecognizer.Start();
    }

    //method to shutdown dictation recogniser
    private void ShutDownDictationRecogniser()
    {
        //stop the dication recogniser and dispose of resources it holds
        dictationRecognizer.Stop();
        dictationRecognizer.Dispose();
    }


    // On Phrase detected call the method if the command is found.
    private void SpeechRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs speech)
    {
        //call voice action found method
        voiceActions[speech.text].Invoke();
    }

    private void AddAllVoiceCommands()
    {
        // add voice commands for starting game to dictionary
        voiceActions.Add("start game", StartGame);
        voiceActions.Add("play game", StartGame);
        voiceActions.Add("start", StartGame);
        voiceActions.Add("play", StartGame);
        voiceActions.Add("begin", StartGame);
        voiceActions.Add("begin game", StartGame);

        // add voice commands for pause to dictionary
        voiceActions.Add("pause game", PauseGame);
        voiceActions.Add("unpause game", PauseGame);
        voiceActions.Add("pause", PauseGame);
        voiceActions.Add("unpause", PauseGame);
        voiceActions.Add("resume", PauseGame);
        voiceActions.Add("resume game", PauseGame);

        // add voice commands for sound to dictionary
        voiceActions.Add("sound", ChangeSound);
        voiceActions.Add("sound on", ChangeSound);
        voiceActions.Add("sound off", ChangeSound);
        voiceActions.Add("turn on sound", ChangeSound);
        voiceActions.Add("turn off sound", ChangeSound);
        voiceActions.Add("turn sound on", ChangeSound);
        voiceActions.Add("turn sound off", ChangeSound);
        voiceActions.Add("volume on", ChangeSound);
        voiceActions.Add("volume off", ChangeSound);

        // add voice commands for reset game to dictionary
        voiceActions.Add("reset game", ResetGame);
        voiceActions.Add("restart game", ResetGame);
        voiceActions.Add("reset", ResetGame);
        voiceActions.Add("restart", ResetGame);

        // add voice commands for exit game to dictionary
        voiceActions.Add("exit game", ExitGame);
        voiceActions.Add("close game", ExitGame);
        voiceActions.Add("exit", ExitGame);
        voiceActions.Add("close", ExitGame);
        voiceActions.Add("quit game", ExitGame);
        voiceActions.Add("quit", ExitGame);

        // add voice commands for tutorial to dictionary
        voiceActions.Add("open tutorial", DisplayTutorial);
        voiceActions.Add("close tutorial", DisplayTutorial);
        voiceActions.Add("display tutorial", DisplayTutorial);
        voiceActions.Add("show tutorial", DisplayTutorial);
        voiceActions.Add("open guide", DisplayTutorial);
        voiceActions.Add("close guide", DisplayTutorial);
        voiceActions.Add("display guide", DisplayTutorial);
        voiceActions.Add("show guide", DisplayTutorial);
        voiceActions.Add("tutorial", DisplayTutorial);
        voiceActions.Add("guide", DisplayTutorial);
        voiceActions.Add("help", DisplayTutorial);

        // add voice commands for game controls to dictionary
        voiceActions.Add("jump", PlayerJump);
        voiceActions.Add("left", PlayerMoveLeft);
        voiceActions.Add("right", PlayerMoveRight);
        voiceActions.Add("crouch", PlayerRoll);
        voiceActions.Add("roll", PlayerRoll);
        voiceActions.Add("attack", PlayerAttack);
        voiceActions.Add("swipe", PlayerAttack);
    }

    //method to start game
    private void StartGame()
    {
        // Call script to start game.
        startgame.OnMouseDown();
    }

    //method to pause game
    private void PauseGame()
    {
        // Call script to pause game.
        pause.PauseGame();
    }
    
    //method to quit application
    private void ExitGame()
    {
        Application.Quit();
    }

    //method to restart game
    private void ResetGame()
    {
        //set movement state false
        startgame.SetMovementState(false);
        //Shut down phraze recognition system, as cannot have both dictation and phraze active at same time - https://docs.microsoft.com/en-us/windows/mixed-reality/voice-input-in-unity
        PhraseRecognitionSystem.Shutdown();
        GameObject.Find("Kinect Manager").GetComponent<KManager>()._kinect.Close();
        //reload scene
        SceneManager.LoadScene(0);
    }

    //method to turn on/off sound
    private void ChangeSound()
    {
        // Turn sound on or off.
        options.OnMouseDown();
    }

    //mrthod to turn on and off name panel
    private void TurnOffPanel()
    {
        // Turn panel off when finished
        namePanel.SetActive(false);
    }

    //method to display tutorial
    private void DisplayTutorial()
    {
        // If tutorial display is on turn off.
        if (tutorialPanel.activeSelf)
        {
            tutorialPanel.SetActive(false);
        }
        else
        {
            // Turn on tutorial.
            tutorialPanel.SetActive(true);
        }
    }

    //method to close tutorial
    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
    }

    //method to display name option to user
    private void displayNamePanel()
    {
        playerName.enabled = true;
        progressText.enabled = false;
        loadingimage.enabled = false;
        loadingimageprogress.enabled = false;
    }

    //method to add timeout to names
    private void DicationTimeout()
    {
        ShutDownDictationRecogniser();
        TurnOffPanel();
        if (voiceContolsOn == false) {
            SetupSpeechRecogniser();
        }
    }

    // Other game methods implemented with voice, such as jump, roll, move and attack.
    private void PlayerJump()
    {
        startgame.PlayerJump();
    }

    private void PlayerMoveRight()
    {
        startgame.MoveRight();
    }

    private void PlayerMoveLeft()
    {
        startgame.MoveLeft();
    }

    private void PlayerRoll()
    {
        startgame.RollForward();
    }
    
    private void PlayerAttack()
    {
        startgame.PlayerAttack();
    }
}
