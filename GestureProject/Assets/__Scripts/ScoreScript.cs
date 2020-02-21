using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public TextMeshPro TextMeshPro;
    private int score = 0;
    private int highScore = 0;
    private int coinCounter = 0;
    private Vector3 lastPosition;
    void Start()
    {
        //set last position to current start position of player
        lastPosition = this.transform.position;
        //if a highscore exists, then set it to highscore
        if (PlayerPrefs.HasKey("HighScore"))
        {
            //get high score value from player preferences - adopted from https://unity3d.com/de/learn/tutorials/topics/scripting/high-score-playerprefs
            highScore = PlayerPrefs.GetInt("HighScore");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if player z axis is increased beyond previous z axis position then increase the score
        if ((this.transform.position.z - lastPosition.z) > 0 && StartGame.ControlsOn)
        {
            score++;
        }

        //set last position = new position
        lastPosition = this.transform.position;

        //set score update on TextMeshPro
        TextMeshPro.text = "Score: "+score.ToString();
    }

    //Increase the coin count when user collects a coin
    public void IncreaseCoinCount()
    {
        coinCounter++;
    }

    //Generate the user score when game is finished
    public void GenerateFinalScore() {
        //multiply score by coins collected by user
        score *= coinCounter;

        //if users score is higher than the current high score
        if (score > highScore)
        {
            //set new high score to high score value
            highScore = score;

            //Keep high score value in player preferences even if game is exited
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }   
    
    //reset score when user dies
    public void ResetScore() {
        score = 0;
    }
}
