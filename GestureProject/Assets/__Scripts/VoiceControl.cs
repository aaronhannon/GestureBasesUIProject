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
    
    void Start()
    {
        voiceActions.Add("start game", PlayGame);
        voiceActions.Add("pause game", PauseGame);
        voiceActions.Add("sound", ChangeSound);
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

    private void PlayGame()
    {
        Debug.Log("Play Game");
    }

    private void PauseGame()
    {
        Debug.Log("Pause Game");
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
