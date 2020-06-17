using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : PauseMenu
{

	[SerializeField] private Text scoreText = null;

    // Start is called before the first frame update
    protected override void Start()
    {
		base.Start();

		if (scoreText != null)
		{
			// Clear our string
			scoreText.text = "";
			// Fill our string
			scoreText.text = GameObject.FindWithTag("ScoreText").GetComponent<Text>()?.text;
		}

    }

}
