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
        Debug.Log("Start Game");
    }
}
