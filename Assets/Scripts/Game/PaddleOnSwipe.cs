using UnityEngine;
using System.Collections;

public class PaddleOnSwipe : MonoBehaviour {

    float hfLength;

    [Range(0.1f, 10f)]
    public float maxHfLength;

    [Range(0.1f, 2f)]
    public float width;

    [Range(0f, 2f)]
    public float swipeThreshold;

    [Range(1f, 100f)]
    public float openSpeed;

    [Range(0f, 1f)]
    public float allowedPlayArea;

    Vector3 pinEnd;
    Vector3 dragEnd;
    bool isForming;

    public Transform rightEdge;
    public Transform leftEdge;

    Vector3 dragDir;

    // Use this for initialization
    void Start () {
        transform.localScale = Vector3.zero;
        leftEdge.localScale = Vector3.zero;
        rightEdge.localScale = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            Vector3 touch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touch.z = 0f;

            if (touch.y <= (-Camera.main.orthographicSize + Camera.main.orthographicSize*2f*allowedPlayArea))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    pinEnd = touch;
                    dragEnd = touch;

                    isForming = false;

                    transform.localScale = Vector3.zero;
                    leftEdge.localScale = Vector3.zero;
                    rightEdge.localScale = Vector3.zero;

                    GetComponent<BoxCollider2D>().size = Vector2.zero;
                    GetComponent<BoxCollider2D>().enabled = false;

                    leftEdge.GetComponent<CircleCollider2D>().enabled = false;
                    rightEdge.GetComponent<CircleCollider2D>().enabled = false;

                    hfLength = 0f;
                }

                if (isForming == false)
                {
                    dragEnd = touch;

                    float distBetweenTouches = Vector3.Distance(pinEnd, dragEnd);
                    if (distBetweenTouches > swipeThreshold)
                    {
                        isForming = true;

                        transform.position = pinEnd;
                        leftEdge.position = pinEnd;
                        rightEdge.position = pinEnd;

                        leftEdge.localScale = Vector3.one;
                        rightEdge.localScale = Vector3.one;

                        dragDir = (dragEnd - pinEnd).normalized;
                        transform.rotation = Quaternion.FromToRotation(Vector3.right, dragDir);
                        if (Vector3.Dot(Vector3.right, dragDir) == -1)
                        {
                            transform.rotation = Quaternion.identity;
                        }
                    }
                }
            }
        }
	}

    void FixedUpdate()
    {
        UpdatePaddle();
    }

    void UpdatePaddle()
    {
        if (isForming)
        {
            hfLength = Mathf.Lerp(hfLength, maxHfLength, Time.deltaTime * openSpeed);
            transform.localScale = new Vector3(hfLength*2f, width, 1f);

            leftEdge.position = pinEnd - hfLength * dragDir;
            rightEdge.position = pinEnd + hfLength * dragDir;

            GetComponent<BoxCollider2D>().size = Vector2.one;
        }

        if (isForming && hfLength > 0.1f)
        {
            GetComponent<BoxCollider2D>().enabled = true;
            leftEdge.GetComponent<CircleCollider2D>().enabled = true;
            rightEdge.GetComponent<CircleCollider2D>().enabled = true;
        }
    }
}
