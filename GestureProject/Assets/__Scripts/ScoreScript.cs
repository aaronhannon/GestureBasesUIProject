using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public TextMeshPro TextMeshPro;
    public GameObject player;
    private int score = -1;
    private int coinCounter = 0;
    private Vector3 lastPosition;
    void Start()
    {
        lastPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if ((this.transform.position.z - lastPosition.z) > 0)
        {
            score++;
        }

        lastPosition = this.transform.position;
        TextMeshPro.text = "Score: "+score.ToString();
    }

    public void IncreaseCoinCount()
    {
        coinCounter++;
    }

    public void GenerateScore() {
        score = score * coinCounter;
    }   
    
    public void ResetScore() {
        score = 0;
    }
}
