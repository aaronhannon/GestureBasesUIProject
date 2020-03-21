using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Class that handles updating of player scoreboard UI
public class ScoreboardScript : MonoBehaviour
{
    public TextMeshProUGUI scoreDisplay1;
    public TextMeshProUGUI scoreDisplay2;
    public TextMeshProUGUI scoreDisplay3;
    public TextMeshProUGUI scoreDisplayplayer;
    public TextMeshProUGUI highName1;
    public TextMeshProUGUI highName2;
    public TextMeshProUGUI highName3;
    public TextMeshProUGUI playerName;
    private List<int> highscores = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        //update UI with score and name values
        updateScoreUI();
    }

    //method to update UI for scoreboard
    private void updateScoreUI()
    {
        //set all scoreboard values to the player pref values stored
        scoreDisplay1.text = PlayerPrefs.GetInt("HighScore1").ToString();
        scoreDisplay2.text = PlayerPrefs.GetInt("HighScore2").ToString();
        scoreDisplay3.text = PlayerPrefs.GetInt("HighScore3").ToString();
        scoreDisplayplayer.text = PlayerPrefs.GetInt("Score").ToString();
        highName1.text = PlayerPrefs.GetString("HighName1");
        highName2.text = PlayerPrefs.GetString("HighName2");
        highName3.text = PlayerPrefs.GetString("HighName3");
        playerName.text = PlayerPrefs.GetString("Name");
    }

}
