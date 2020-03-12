﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class VoiceControl : MonoBehaviour
{
    #region == Private variables ==
    private KeywordRecognizer speechRecognizer;
    private Dictionary<string, Action> voiceActions = new Dictionary<string, Action>();
    private StartGame startgame;
    private Pause pause;
    private Options options;
    #endregion

    void Start()
    {
        // Get scripts to access methods.
        startgame = gameObject.GetComponent<StartGame>();
        pause = GameObject.Find("Main Camera").GetComponent<Pause>();
        options = GameObject.Find("VikingSoundHorn").GetComponent<Options>();

        // All all voice commands to Dictionary
        AddAllVoiceCommands();

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
        // Start game
        voiceActions.Add("start game", StartGame);
        voiceActions.Add("play game", StartGame);
        voiceActions.Add("start", StartGame);
        voiceActions.Add("play", StartGame);

        // Pause game
        voiceActions.Add("pause game", PauseGame);
        voiceActions.Add("unpause game", PauseGame);
        voiceActions.Add("pause", PauseGame);
        voiceActions.Add("unpause", PauseGame);

        // Turn sound on/off
        voiceActions.Add("sound", ChangeSound);
        voiceActions.Add("sound on", ChangeSound);
        voiceActions.Add("sound off", ChangeSound);
        voiceActions.Add("turn on sound", ChangeSound);
        voiceActions.Add("turn off sound", ChangeSound);

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
        SceneManager.LoadScene(0);
    }

    private void ChangeSound()
    {
        // Turn sound on or off.
        options.OnMouseDown();
    }
}