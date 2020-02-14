using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    #region == Private Variables == 

    private AudioSource source;

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
    }
}
