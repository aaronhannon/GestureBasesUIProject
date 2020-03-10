using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceControl : MonoBehaviour
{
    private KeywordRecognizer speechRecognizer;
    private Dictionary<string, Action> voiceActions = new Dictionary<string, Action>();
    private StartGame startgame;
    private Pause pause;

    void Start()
    {
        startgame = gameObject.GetComponent<StartGame>();
        pause = GameObject.Find("Main Camera").GetComponent<Pause>();

        voiceActions.Add("start game", StartGame);
        voiceActions.Add("pause game", PauseGame);
        voiceActions.Add("unpause game", PauseGame);
        voiceActions.Add("sound on", ChangeSound);
        voiceActions.Add("sound off", ChangeSound);
        voiceActions.Add("reset game", ResetGame);
        voiceActions.Add("exit game", ExitGame);

        speechRecognizer = new KeywordRecognizer(voiceActions.Keys.ToArray());
        speechRecognizer.OnPhraseRecognized += SpeechRecognizer_OnPhraseRecognized;
        speechRecognizer.Start();
    }

    private void SpeechRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs speech)
    {
        voiceActions[speech.text].Invoke();
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
        Debug.Log("Exit Game");
    }

    private void ResetGame()
    {
        Debug.Log("Reset Game");
    }

    private void ChangeSound()
    {
        Debug.Log("Sound on/off");
    }
}
