using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class CanvasController : MonoBehaviour
{

	[SerializeField]
	private GameObject mainMenuPanel;

	[SerializeField]
	private GameObject gameOverPanel;

	[SerializeField]
	private Text scoreText;

	[SerializeField]
	private Text arrowCountText;

	[SerializeField]
	private Text enemyCountText;

	[SerializeField]
	private Text finalScore;

	[SerializeField]
	private InputField initial;

	[SerializeField]
	private List<Text> top;

	private int finalScoreValue;

	public void HideMainMenu ()
	{
		mainMenuPanel.SetActive (false);
	}

	public void ShowGameOverPopup (float score)
	{
		finalScore.text = score.ToString ();
		finalScoreValue = (int)score;
		gameOverPanel.SetActive (true);
	}

	public void UpdateScore (float score)
	{
		scoreText.text = "Score: " + score;
	}

	public void UpdateArrow (int arrow)
	{
		arrowCountText.text = "Arrows: " + arrow;
	}

	public void UpdateEnemy (int enemy)
	{
		enemyCountText.text = "Enemies: " + enemy;
	}

	public string GetInitial ()
	{
		return initial.text;
	}

	public int GetFinalScoreValue ()
	{
		return finalScoreValue;
	}

	// Initialize the leader board at main menu
	public void SetLeaderboard ()
	{
		List <LocalSave.PlayerInfo> topScores = LocalSave.LoadTop ();
		for (int i = 0; i < topScores.Count; i++) {
			top [i].text = (i + 1).ToString () + "." + topScores [i].name + " " + topScores [i].score;
		}
	}
}
