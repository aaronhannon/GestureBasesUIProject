using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;
using TMPro;

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

    private DictationRecognizer dictationRecognizer;
    #endregion

    void Start()
    {
        playerName.enabled = false;
        progressText.enabled = false;
        loadingimage.enabled = false;
        loadingimageprogress.enabled = false;

        PlayerPrefs.SetString("Name", "");
        // Get scripts to access methods.
        startgame = gameObject.GetComponent<StartGame>();
        pause = GameObject.Find("Main Camera").GetComponent<Pause>();
        options = GameObject.Find("VikingSoundHorn").GetComponent<Options>();

        // All all voice commands to Dictionary
        AddAllVoiceCommands();

        //SetupSpeechRecogniser();

        dictationRecognizer = new DictationRecognizer();
        dictationRecognizer.DictationResult += DictationRecognizer_DictationResult;
        dictationRecognizer.DictationHypothesis += DictationRecognizer_DictationHypothesis;
        //dictationRecognizer.DictationComplete += DictationRecognizer_DictationComplete;
        dictationRecognizer.Start();

        Invoke("DicationTimeout", 10);

    }

    private void DictationRecognizer_DictationResult(string name, ConfidenceLevel confidence)
    {
        Debug.Log("Name set as: " + name);
        if (name.Length > 11) {
            //get a substring of name less than 9 so that it fits the UI
            name = name.Substring(0, 9);
        }
        //set user voice returned name in player prefs
        PlayerPrefs.SetString("Name", name);
        playerName.text = name;

        //display name panel after result
        displayNamePanel();

        //after name is set turn off panel after 2 seconds
        Invoke("TurnOffPanel", 2);

        //shutdown dictation recogniser as cannot use at same time as voice controls
        ShutDownDictationRecogniser();

        //start speech recognisers for game controls
        SetupSpeechRecogniser();
    }

    private void DictationRecognizer_DictationHypothesis(string text)
    {
        Debug.Log("Thinking");
        progressText.enabled = true;
        loadingimage.enabled = true;
        loadingimageprogress.enabled = true;
    }

    private void DictationRecognizer_DictationComplete(DictationCompletionCause cause)
    {
        Debug.Log("complete");
    }

    private void SetupSpeechRecogniser()
    {
        // Add keywords from dictionary to KeywordRecognizer and start listening for commands.
        speechRecognizer = new KeywordRecognizer(voiceActions.Keys.ToArray());
        speechRecognizer.OnPhraseRecognized += SpeechRecognizer_OnPhraseRecognized;
        speechRecognizer.Start();
    }

    private void ShutDownDictationRecogniser()
    {
        //stop the dication recogniser and dispose of resources it holds
        dictationRecognizer.Stop();
        dictationRecognizer.Dispose();
    }


    // On Phrase detected call the method if the command is found.
    private void SpeechRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs speech)
    {
        voiceActions[speech.text].Invoke();
    }

    private void AddAllVoiceCommands()
    {
        // Start game
        voiceActions.Add("start game", StartGame);
        voiceActions.Add("play game", StartGame);
        voiceActions.Add("start", StartGame);
        voiceActions.Add("play", StartGame);
        voiceActions.Add("begin", StartGame);
        voiceActions.Add("begin game", StartGame);

        // Pause game
        voiceActions.Add("pause game", PauseGame);
        voiceActions.Add("unpause game", PauseGame);
        voiceActions.Add("pause", PauseGame);
        voiceActions.Add("unpause", PauseGame);
        voiceActions.Add("resume", PauseGame);
        voiceActions.Add("resume game", PauseGame);

        // Turn sound on/off
        voiceActions.Add("sound", ChangeSound);
        voiceActions.Add("sound on", ChangeSound);
        voiceActions.Add("sound off", ChangeSound);
        voiceActions.Add("turn on sound", ChangeSound);
        voiceActions.Add("turn off sound", ChangeSound);
        voiceActions.Add("turn sound on", ChangeSound);
        voiceActions.Add("turn sound off", ChangeSound);
        voiceActions.Add("volume on", ChangeSound);
        voiceActions.Add("volume off", ChangeSound);

        // Reset game
        voiceActions.Add("reset game", ResetGame);
        voiceActions.Add("restart game", ResetGame);
        voiceActions.Add("reset", ResetGame);
        voiceActions.Add("restart", ResetGame);

        // Exit game
        voiceActions.Add("exit game", ExitGame);
        voiceActions.Add("close game", ExitGame);
        voiceActions.Add("exit", ExitGame);
        voiceActions.Add("close", ExitGame);
        voiceActions.Add("quit game", ExitGame);
        voiceActions.Add("quit", ExitGame);

        // Exit game
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

        // Other game commands
        voiceActions.Add("jump", PlayerJump);
        voiceActions.Add("left", PlayerMoveLeft);
        voiceActions.Add("right", PlayerMoveRight);
        voiceActions.Add("crouch", PlayerRoll);
        voiceActions.Add("roll", PlayerRoll);
        voiceActions.Add("attack", PlayerAttack);
        voiceActions.Add("swipe", PlayerAttack);
    }

    private void StartGame()
    {
        // Call script to start game.
        startgame.OnMouseDown();
    }

    private void PauseGame()
    {
        // Call script to pause game.
        pause.PauseGame();
    }
    
    private void ExitGame()
    {
        Application.Quit();
    }

    private void ResetGame()
    {
        startgame.SetMovementState(false);
        PhraseRecognitionSystem.Shutdown();
        SceneManager.LoadScene(0);
    }

    private void ChangeSound()
    {
        // Turn sound on or off.
        options.OnMouseDown();
    }

    private void TurnOffPanel()
    {
        // Turn panel off when finished
        namePanel.SetActive(false);
    }

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

    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
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

    private void displayNamePanel()
    {
        playerName.enabled = true;
        progressText.enabled = false;
        loadingimage.enabled = false;
        loadingimageprogress.enabled = false;
    }

    private void DicationTimeout()
    {
        ShutDownDictationRecogniser();
        TurnOffPanel();
        SetupSpeechRecogniser();

    }

}
