using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EndGameData
{
	public int score;
	public bool isWin;
}
public class WinScreen : MonoBehaviour
{
	[SerializeField]
	private Text scoreTxt;
	[SerializeField]
	private Text gameStatusTxt;
	[SerializeField]
	private Button restartBtn;

	public void Init(EndGameData endData, Action OnRestart)
	{
		gameStatusTxt.text = endData.isWin ? "You Win" : "You Lose";
		scoreTxt.text = string.Format ("Score: {0}", endData.score);
		restartBtn.onClick.AddListener (() => { OnRestart (); });
		gameObject.SetActive (true);
	}
}
