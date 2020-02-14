using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public TextMeshPro TextMeshPro;
    public GameObject player;
    private int score = -1;
    public static int coinCounter = 0;
    private Vector3 _lastPosition;
    void Start()
    {
        _lastPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if ((this.transform.position.z - _lastPosition.z) > 0)
        {
            score++;
        }

        _lastPosition = this.transform.position;
        TextMeshPro.text = "Score: "+score.ToString();
    }
}
