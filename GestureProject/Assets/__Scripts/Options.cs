using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Class that deals with in game options
public class Options : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Check if sound is on or not.
        CheckSound();
    }

    // Check if sound option has been saved before.
    // E.g if not the first time playing the game, or sound has never been turned off.
    // If so then change toggle switch to false if sound is off.
    // If not then set playerPref to true, for again.
    private void CheckSound()
    {
        //PlayerPrefs.DeleteAll();

        // MUSIC
        if (PlayerPrefs.HasKey("Music"))
        {
            // Change sound game object in world. 
            if (PlayerPrefs.GetString("Music") == "True")
            {
                // Sound is on
            }
            else
            {
                // Sound is off
            }
        }
        else
        {
            PlayerPrefs.SetString("Music", "True");
        }
    }
    
    public void OnMouseDown()
    {
        // Change sound game object in world.
        if (PlayerPrefs.GetString("Music") == "True")
        {
            // Sound is on so turn off
            PlayerPrefs.SetString("Music", "False");
        }
        else if (PlayerPrefs.GetString("Music") == "False")
        {
            // Sound is off so turn on
            PlayerPrefs.SetString("Music", "True");
        }
    }
}
