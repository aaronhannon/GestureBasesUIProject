using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class VoiceControl : MonoBehaviour
{
    private KeywordRecognizer speechRecognizer;
    private Dictionary<string, Action> voiceActions = new Dictionary<string, Action>();
    private StartGame startgame;
    private Pause pause;
    private Options options;

    void Start()
    {
        startgame = gameObject.GetComponent<StartGame>();
        pause = GameObject.Find("Main Camera").GetComponent<Pause>();
        options = GameObject.Find("VikingSoundHorn").GetComponent<Options>();

        AddAllVoiceCommands();

        speechRecognizer = new KeywordRecognizer(voiceActions.Keys.ToArray());
        speechRecognizer.OnPhraseRecognized += SpeechRecognizer_OnPhraseRecognized;
        speechRecognizer.Start();
    }

    private void SpeechRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs speech)
    {
        voiceActions[speech.text].Invoke();
    }

    private void AddAllVoiceCommands()
    {
        voiceActions.Add("start game", StartGame);
        voiceActions.Add("play game", StartGame);
        voiceActions.Add("start", StartGame);
        voiceActions.Add("play", StartGame);

        voiceActions.Add("pause game", PauseGame);
        voiceActions.Add("unpause game", PauseGame);
        voiceActions.Add("pause", PauseGame);
        voiceActions.Add("unpause", PauseGame);

        voiceActions.Add("sound", ChangeSound);
        voiceActions.Add("sound on", ChangeSound);
        voiceActions.Add("sound off", ChangeSound);
        voiceActions.Add("turn on sound", ChangeSound);
        voiceActions.Add("turn off sound", ChangeSound);

        voiceActions.Add("reset game", ResetGame);
        voiceActions.Add("restart game", ResetGame);
        voiceActions.Add("reset", ResetGame);
        voiceActions.Add("restart", ResetGame);

        voiceActions.Add("exit game", ExitGame);
        voiceActions.Add("close game", ExitGame);
        voiceActions.Add("exit", ExitGame);
        voiceActions.Add("close", ExitGame);
    }

    private void StartGame()
    {
        startgame.OnMouseDown();
    }

    private void PauseGame()
    {
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
        options.OnMouseDown();
    }
}
