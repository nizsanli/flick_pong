using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreMgr : MonoBehaviour {

    public int score;

    public Text scoreText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = score.ToString();
	}

    public void Plus()
    {
        score++;
    }

    public void Minus()
    {
        score--;

        if (score < 0)
        {
            score = 0;
        }
    }
}
