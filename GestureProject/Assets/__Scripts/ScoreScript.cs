using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
// Class that updates the player score
public class ScoreScript : MonoBehaviour
{
    public TextMeshPro scoreDisplay;
    private int score = 0;
    private List<int> highscores = new List<int>();
    private List<string> highscoresnames = new List<string>();
    private int coinCounter = 0;
    private Vector3 lastPosition;
    public TextMeshPro coinCountDisplay;

    void Start()
    {
        PlayerPrefs.SetInt("Score", 0);
        //set last position to current start position of player
        lastPosition = this.transform.position;

        for (int i = 0; i < 3; i++) {
            //if a highscore exists, then set it to highscore
            if (PlayerPrefs.HasKey("HighScore"+(i+1)))
            {
                highscores.Insert(i, PlayerPrefs.GetInt("HighScore" + (i + 1)));
                highscoresnames.Insert(i, PlayerPrefs.GetString("HighName" + (i + 1)));
            }
            else {
                highscores.Insert(i, 0);
                highscoresnames.Insert(i, "Empty");
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if player z axis is increased beyond previous z axis position then increase the score
        if ((this.transform.position.z - lastPosition.z) > 0 && StartGame.ControlsOn)
        {
            //increase score when player progresses in game
            score++;
        }

        //set last position = new position
        lastPosition = this.transform.position;

        //set score update on TextMeshPro
        scoreDisplay.text = "Score: "+score.ToString();

        //set coin count update on TextMeshPro
        coinCountDisplay.text = coinCounter.ToString();
    }

    //Increase the coin count when user collects a coin
    public void IncreaseCoinCount()
    {
        //increase coin count when coin is collected
        coinCounter++;
    }

    //Generate the user score when game is finished
    public void GenerateFinalScore() {
        //multiply score by coins collected by user
        if (coinCounter != 0) {
            score *= coinCounter;
        }

        //set layers score in playerprefs
        PlayerPrefs.SetInt("Score", score);

        //Check if the score is high enough to be added.
        for (int i = 0; i < highscores.Count; i++)
        {
            if (score > highscores[i])
            {
                highscores.Insert(i, score);
                //if name not set, then set a default anonymous name
                if (PlayerPrefs.GetString("Name") == "")
                {
                    PlayerPrefs.SetString("Name", "anonymous");
                }
                highscoresnames.Insert(i, PlayerPrefs.GetString("Name"));
                break;
            }
        }
        setHighscorePlayerPrefs();
    }   
    
    //reset score when user dies
    public void ResetScore() {
        //reset score to 0
        score = 0;
    }

    //set the highscores as playerprefs
    public void setHighscorePlayerPrefs()
    {
        for (int i = 0; i < 3; i++)
        {
            PlayerPrefs.SetInt("HighScore" + (i + 1), highscores[i]);
            PlayerPrefs.SetString("HighName" + (i + 1), highscoresnames[i]);
        }
    }
}
