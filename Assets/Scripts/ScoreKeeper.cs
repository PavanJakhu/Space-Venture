using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : NetworkBehaviour
{
    [SyncVar]
    private int score;
    private Text scoreText;

    // Use this for initialization
    void Start()
    {
        scoreText = GetComponent<Text>();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
    }

    public void AddScore(int addScore)
    {
        if (!isServer)
        {
            return;
        }

        score += addScore;
    }

    public int GetScore()
    {
        if (!isServer)
        {
            return -1;
        }
        return score;
    }
}
