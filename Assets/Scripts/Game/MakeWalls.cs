using UnityEngine;
using System.Collections;

public class MakeWalls : MonoBehaviour {

    public Transform wallPre;

    public float sideWidth;
    public PaddleOnSwipe paddle;

    public Material baseUnlit2;

    GameObject recentObjectCreated;

    public float topWallLength;
    public BallScript ballScript;

    // Use this for initialization
    void Start () {
        MakeWall(
            Vector3.up * Camera.main.orthographicSize,
            new Vector3(topWallLength, 1f, 1f),
            Quaternion.identity,
            true
            );
        recentObjectCreated.name = "Top Wall";
        FollowBall followScript = recentObjectCreated.AddComponent<FollowBall>();
        followScript.Init();
        ballScript.topWallFollowScript = followScript;
        MakeWall(
            Vector3.left * Camera.main.orthographicSize * Camera.main.aspect,
            new Vector3(sideWidth, Camera.main.orthographicSize * 2f, 1f),
            Quaternion.identity,
            true
            
            );

        MakeWall(
            Vector3.right * Camera.main.orthographicSize * Camera.main.aspect,
            new Vector3(sideWidth, Camera.main.orthographicSize*2f, 1f),
            Quaternion.identity,
            true
            );

        
        MakeWall(
            new Vector3(0f, -Camera.main.orthographicSize + Camera.main.orthographicSize*paddle.allowedPlayArea, 1f),
            new Vector3(Camera.main.orthographicSize*2f*Camera.main.aspect, Camera.main.orthographicSize*2f * paddle.allowedPlayArea, 1f),
            Quaternion.identity,
            false
            );
        /*
        MakeWall(
            Vector3.left * Camera.main.orthographicSize * Camera.main.aspect + Vector3.up * Camera.main.orthographicSize,
            new Vector3(2f, 1.5f, 1f),
            Quaternion.Euler(0f, 0f, 45f)
            );

        MakeWall(
            Vector3.right * Camera.main.orthographicSize * Camera.main.aspect + Vector3.up * Camera.main.orthographicSize,
            new Vector3(2f, 1.5f, 1f),
            Quaternion.Euler(0f, 0f, -45f)
            );
        */
    }

    void MakeWall(Vector3 pos, Vector3 scale, Quaternion rotation, bool buildCollider)
    {
        Transform newWall = (Transform)Instantiate(wallPre, pos, rotation);
        recentObjectCreated = newWall.gameObject;
        newWall.localScale = scale;
        newWall.gameObject.isStatic = true;
        newWall.parent = transform;

        if (buildCollider)
        {
            newWall.gameObject.AddComponent<BoxCollider2D>();
        }
        else
        {
            newWall.GetComponent<MeshRenderer>().material = baseUnlit2;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
