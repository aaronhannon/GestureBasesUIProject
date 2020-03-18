using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class DeathVoiceControl : MonoBehaviour
{
    #region == Private variables ==
    private KeywordRecognizer speechRecognizer;
    private Dictionary<string, Action> voiceActions = new Dictionary<string, Action>();
    private StartGame startgame;
    private Pause pause;
    private Options options;
    private Boolean nameset = false;

    #endregion

    void Start()
    {
        // All all voice commands to Dictionary
        AddAllVoiceCommands();

        //start speech recognisers for game controls
        SetupSpeechRecogniser();
    }



    private void SetupSpeechRecogniser()
    {
        // Add keywords from dictionary to KeywordRecognizer and start listening for commands.
        speechRecognizer = new KeywordRecognizer(voiceActions.Keys.ToArray());
        speechRecognizer.OnPhraseRecognized += SpeechRecognizer_OnPhraseRecognized;
        speechRecognizer.Start();
    }

    // On Phrase detected call the method if the command is found.
    private void SpeechRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs speech)
    {
        voiceActions[speech.text].Invoke();
    }

    private void AddAllVoiceCommands()
    {
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
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void ResetGame()
    {
        PhraseRecognitionSystem.Shutdown();
        SceneManager.LoadScene(0);
    }
}
