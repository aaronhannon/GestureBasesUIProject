using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    #region == Private Variables == 

    private AudioSource source;

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
        // Get AudioSource component and start background music playing.
        source = GetComponent<AudioSource>();
        loadAllAudioClips();

        playAudio("background_2");
    }

    // Play any sound from Dictionary.
    public void playAudio(string fileName)
    {
        source.clip = audioClips["background_2"];
        source.Play();
    }

    private void loadAllAudioClips()
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
