using UnityEngine;
using System.Collections;

public class GameReset : MonoBehaviour {

    public Transform activeBall;
	
	// Update is called once per frame
	void Update () {
        // reset
	    if (activeBall.position.y < -Camera.main.orthographicSize-1f)
        {
            GetComponent<ScoreMgr>().Minus();
            GetComponent<ColorWorld>().NewColor();
            activeBall.GetComponent<BallScript>().Reset();
        }
        else if (activeBall.position.y > Camera.main.orthographicSize + 1f)
        {
            GetComponent<ScoreMgr>().Plus();
            GetComponent<ColorWorld>().NewColor();
            activeBall.GetComponent<BallScript>().Reset();
        }
	}
}
