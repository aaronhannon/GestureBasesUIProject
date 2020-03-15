using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreboardScript : MonoBehaviour
{
    public TextMeshProUGUI scoreDisplay1;
    public TextMeshProUGUI scoreDisplay2;
    public TextMeshProUGUI scoreDisplay3;
    public TextMeshProUGUI scoreDisplayplayer;
    private List<int> highscores = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        updateScoreUI();
    }

    private void updateScoreUI()
    {
        scoreDisplay1.text = PlayerPrefs.GetInt("HighScore1").ToString();
        scoreDisplay2.text = PlayerPrefs.GetInt("HighScore2").ToString();
        scoreDisplay3.text = PlayerPrefs.GetInt("HighScore3").ToString();
        scoreDisplayplayer.text = PlayerPrefs.GetInt("Score").ToString();
    }

}
