using UnityEngine;
using System.Collections;
using System;

public class BallScript : MonoBehaviour {

    bool reset;
    float currScale;

    public float resetSpeed;
    public float initialSpeed;
    public float bounceMult;
    public float maxVel;

    public System.Random rand;

    public ScoreMgr scoreMgr;
    public FollowBall topWallFollowScript;

	// Use this for initialization
	void Start () {
        rand = new System.Random();

        Reset();
	}

    public void Reset()
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
        transform.position = Vector3.zero;
        transform.localScale = Vector3.zero;

        reset = true;
        currScale = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    void FixedUpdate()
    {
        if (reset)
        {
            currScale = Mathf.Lerp(currScale, 1f, Time.deltaTime * resetSpeed);
            transform.localScale = new Vector3(currScale, currScale, 1f);
        }

        if (reset && (1f - currScale) <= 0.01f)
        {
            transform.localScale = Vector3.one;

            // shoot ball
            GetComponent<Rigidbody2D>().isKinematic = false;

            float randOffset = (float)rand.NextDouble() - 0.5f;
            GetComponent<Rigidbody2D>().velocity = (Vector3.down + Vector3.right*randOffset*2f).normalized * initialSpeed;

            reset = false;

            topWallFollowScript.followSpeed = 1 + scoreMgr.score * 1.5f;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        float bumpSpeed = 1f;
        foreach (ContactPoint2D point in col.contacts)
        {
            if (point.collider.name == "Top Wall" || point.collider.name == "Paddle")
            {
                bumpSpeed *= bounceMult;
            }
        }
        GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity * bumpSpeed;
    

        // fix to max speed
        if (GetComponent<Rigidbody2D>().velocity.magnitude > maxVel)
        {
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * maxVel;
        }
        
    }
}
