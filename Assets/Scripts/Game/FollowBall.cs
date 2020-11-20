using UnityEngine;
using System.Collections;

public class FollowBall : MonoBehaviour {

    Transform ball;
    public float followSpeed;
    public ScoreMgr scoreMgr;

	// Use this for initialization
	void Start () {
        ball = GameObject.Find("Ball").transform;
	}
	
    public void Init()
    {
        followSpeed = 8f;
    }

	// Update is called once per frame
	void Update () {
        float toMove = followSpeed * Time.deltaTime;

        if (transform.position.x < ball.position.x)
        {
            if (transform.position.x + toMove > ball.position.x)
            {
                toMove = ball.position.x - transform.position.x;
            }
        }
        else if (transform.position.x > ball.position.x)
        {
            toMove *= -1f;
            if (transform.position.x + toMove < ball.position.x)
            {
                toMove = ball.position.x - transform.position.x;
            }
        }
        else
        {
            toMove = 0f;
        }

        transform.Translate(Vector3.right * toMove);
    }
}
