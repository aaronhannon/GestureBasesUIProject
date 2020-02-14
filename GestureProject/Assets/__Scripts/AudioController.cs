using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    #region == Private Variables == 

    private AudioSource source;

    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

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
    }

    private void loadAllAudioClips()
    {
        DirectoryInfo dir = new DirectoryInfo("Assets/Resources/Audio");
        FileInfo[] info = dir.GetFiles();

        foreach (FileInfo f in info)
        {
            if (!f.Name.Contains(".meta"))
            {
                string[] fileName = f.Name.Split('.');

                AudioClip temp = Resources.Load("Audio/" + fileName[0]) as AudioClip;
                audioClips.Add(f.Name, temp);
            }
        }
    }
}
