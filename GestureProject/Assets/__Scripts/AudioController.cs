﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
// Class which handles Audio for in game
public class AudioController : MonoBehaviour
{
    #region == Private Variables == 

    [SerializeField]
    private AudioSource sourceMusic;
    [SerializeField]
    private AudioSource sourceSFX;

    // Dictionary which holds all audio files in memory.
    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    #endregion

    // Singleton design pattern to get instance of class
    public static AudioController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Use this for initialization
    void Start()
    {
        // Load in all clips into memory, and start background music.
        LoadAllAudioClips();
        PlayAudio("background_2");
    }

    void Update()
    {
        // Check if game audio is off using player prefs, if so mute audiosource.
        // This turns the music on or off.
        if (PlayerPrefs.GetString("Music") == "True")
        {
            sourceMusic.mute = false;
            sourceSFX.mute = false;
        }
        else
        {
            sourceMusic.mute = true;
            sourceSFX.mute = true;
        }
    }

    // Play background music from Dictionary.
    public void PlayAudio(string fileName)
    {
        sourceMusic.clip = audioClips[fileName];
        sourceMusic.Play();
    }
    
    // Play audio just once E.g collectables, jump etc.
    public void PlayAudioOnce(string fileName)
    {
        sourceSFX.PlayOneShot(audioClips[fileName]);
    }

    // Play additional audio that will loop E.g Run along with background music.
    public void PlayLoopAudio(string fileName)
    {
        sourceSFX.clip = audioClips[fileName];
        sourceSFX.Play();
    }

    private void LoadAllAudioClips()
    {
        // Get all file names from Resources folder, and get file information about each.
        // Code adapted from: https://answers.unity.com/questions/16433/get-list-of-all-files-in-a-directory.html
        DirectoryInfo dir = new DirectoryInfo("Assets/Resources/Audio");
        FileInfo[] fileInfo = dir.GetFiles();

        // Loop through each file in the directory/array.
        foreach (FileInfo file in fileInfo)
        {
            // Exlude meta files
            if (!file.Name.Contains(".meta"))
            {
                // Remove file extension from file name, get file from resouces and add to dictionary.
                string[] fileName = file.Name.Split('.');
                
                AudioClip temp = Resources.Load("Audio/" + fileName[0]) as AudioClip;
                audioClips.Add(fileName[0], temp);
            }
        }
    }
}
