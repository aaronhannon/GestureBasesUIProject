using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public TextMeshPro TextMeshPro;
    private int score = -1;
    private int highScore = 0;
    private int coinCounter = 0;
    private Vector3 lastPosition;
    private ArrayList numbers;
    private GameObject camera;
    void Start()
    {

        camera = GameObject.Find("Main Camera");


        numbers = new ArrayList();

        for (int i = 0; i <= 9; i++)
        {

            numbers.Add(Resources.Load("Numbers/" + i.ToString()) as GameObject);



        }

        //char[] toChar = test.ToCharArray();

        //float x = 5f;



        ////foreach(Transform child in scorecontainer.transform)
        ////{
        ////    Destroy(child.gameObject);
        ////}

        //foreach (char letter in toChar)
        //{


        //    Instantiate(numbers.ToArray()[Int32.Parse(letter.ToString())] as GameObject, new Vector3(x, 10f, -30f), Quaternion.Euler(0, 180, 0)).transform.parent = scorecontainer.transform;
        //    x += 1f;
        //}


        //foreach (GameObject number in numbers)
        //{
        //    Instantiate(number, new Vector3(0f, 5f, 0f), Quaternion.Euler(0, 180, 0)).transform.parent = scorecontainer.transform;
        //}
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
        

        //
        //Instantiate(scorecontainer);
        //if player z axis is increased beyond previous z axis position then increase the score
        if ((this.transform.position.z - lastPosition.z) > 0)
        {
            score++;
        }

        //set last position = new position
        lastPosition = this.transform.position;

        //set score update on TextMeshPro
        TextMeshPro.text = "Score: "+score.ToString();




        float x = 3f;

        //Debug.Log(toChar);

        //foreach (char letter in toChar)
        //{


        //    Instantiate(numbers.ToArray()[Int32.Parse(letter.ToString())] as GameObject, new Vector3(x, 10f, -30f), Quaternion.Euler(0, 180, 0)).transform.parent = scorecontainer.transform;
        //    x += 1f;
        //}
        //if (GameObject.Find("ScoreC") == null)
        //{
            
        //    GameObject scorec = new GameObject("ScoreC");
        //    scorec.transform.parent = camera.transform;

        //    string s = "2032";
        //    //Debug.Log("SCORE TEST: " + s);
        //    char[] toChar = s.ToCharArray();

        //    //foreach (var item in toChar)
        //    //{
        //    //    Debug.Log(item);
        //    //}
        //    foreach (char letter in toChar)
        //    {


        //        Instantiate(numbers.ToArray()[Int32.Parse(letter.ToString())] as GameObject, new Vector3(x, 10f, -32f), Quaternion.Euler(0, 180, 0)).transform.parent = scorec.transform;
        //        x += 1f;
        //    }
        //}
        //else
        //{
        //    Destroy(GameObject.Find("ScoreC"));
        //}


    }

    //Increase the coin count when user collects a coin
    public void IncreaseCoinCount()
    {
        coinCounter++;
    }

    //Generate the user score when game is finished
    public void GenerateScore() {
        //multiply score by coins collected by user
        score = score * coinCounter;

        //if users score is higher than the current high score
        if (score > highScore)
        {
            //set new high score to high score value
            highScore = score;

            print("new high score: " + highScore);

            //Keep high score value in player preferences even if game is exited
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }   
    
    //reset score when user dies
    public void ResetScore() {
        score = 0;
    }
}
