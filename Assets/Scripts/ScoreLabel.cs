using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreLabel : MonoBehaviour {

    public static int score = 0;

    private Text label;

	void Start ()
    {
        label = gameObject.GetComponent<Text>();
	}
	
	void Update ()
    {
        label.text = "Score: " + score;
    }
}
