using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class ColorWorld : MonoBehaviour {

    System.Random rand;

    Color color;
    public bool generate;

    float maxColorValueRecip = 1f / 255f;

    public Material baseUnlit;
    public Material baseUnlit2;

    public Text text;

	// Use this for initialization
	void Start () {
        rand = new System.Random();

        NewColor();
	}

    void Update()
    {
        if (generate)
        {
            NewColor();
            
        }
    }

    public void NewColor()
    {
        int highestIndex = 0;
        float highestColVal = 0f;
        float[] cols = new float[3];
        for (int i = 0; i < 3; i++)
        {
            float colorVal = rand.Next(25, 225) * maxColorValueRecip;

            if (colorVal > highestColVal)
            {
                highestColVal = colorVal;
                highestIndex = i;
            }


            cols[i] = colorVal;
        }

        cols[highestIndex] = 1f;
        Color color = new Color(cols[0], cols[1], cols[2], 1f);

        baseUnlit.color = color;
        Camera.main.backgroundColor = color * 0.25f;
        baseUnlit2.color = Camera.main.backgroundColor * 1.08f;

        text.color = baseUnlit.color * 0.6f;
    }
}
