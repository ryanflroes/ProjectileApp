using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

	private System.Action ActionGameStart;
	private System.Action ActionGameOver;

	public float Score;
	public Transform player;

	[SerializeField]
	private MouseLook mouseLook;
	[SerializeField]
	private LineRenderer lineRenderer;
	[SerializeField]
	private SpawnOnButton spawnOnButton;

	private CanvasController _canvasController = null;

	public CanvasController canvasController {
		get {
			return _canvasController ?? (_canvasController = GetComponent<CanvasController> ());
		}
	}

	private Camera _camera2D = null;

	public Camera camera2D {
		get {
			return _camera2D ?? (_camera2D = transform.FindChild ("2DCamera").GetComponent<Camera> ());
		}
	}

	#region Arrow and Quiver

	[SerializeField]
	private Transform quiver = null;

	[SerializeField]
	private GameObject arrow = null;

	private List<GameObject> arrowBank = new List<GameObject> ();

	private const int maxNumArrow = 8;

	#endregion

	#region Enemies

	[SerializeField]
	private GameObject enemy;

	[SerializeField]
	private Transform enemyContainer;

	private List<GameObject> enemies = new List<GameObject> ();

	private const int maxEnemies = 5;

	#endregion

	public bool StillHaveArrow ()
	{
		return arrowBank.Count != 0;
	}

	// Get arrow from bank
	public GameObject GetArrow {
		get {
			if (arrowBank.Count != 0) {
				GameObject _arrow = arrowBank [0];
				arrowBank.RemoveAt (0);
				return _arrow;
			}
			return null;
		}
	}

	// Initialization of delegate, Arrows, enemies, leaderboard
	void Awake ()
	{
		InitDelagate ();
		InstantiateArrows ();
		InstantiateEnemies ();
		InitLeaderBoard ();
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.P)) {
			Debug.Log ("Values deleted");
			LocalSave.DeleteAllValues ();
		}

		if (Input.GetKeyDown (KeyCode.C)) {
			if (camera2D.isActiveAndEnabled)
				camera2D.gameObject.SetActive (false);
			else
				camera2D.gameObject.SetActive (true);
		}
	}

	// Delagates
	private void InitDelagate ()
	{
		ActionGameStart += GameStart;
		ActionGameOver += GameOver;
	}

	// Load leader board to canvas
	private void InitLeaderBoard ()
	{
		canvasController.SetLeaderboard ();
	}

	// Lister for start game button
	public void GameStartInpspector ()
	{
		if (ActionGameStart != null) {
			ActionGameStart ();
		}
	}

	void GameStart ()
	{
		canvasController.HideMainMenu ();
		player.gameObject.SetActive (true);
	}

	void GameOver ()
	{
		mouseLook.enabled = false;
		lineRenderer.enabled = false;
		spawnOnButton.enabled = false;

		float remainingArrow = arrowBank.Count * 25;
		Score += remainingArrow;
		canvasController.ShowGameOverPopup (Score);
	}

	// Lister to main menu button
	// Save score and Reload scene
	public void RestartGame ()
	{
		LocalSave.SaveScore (canvasController.GetInitial (), canvasController.GetFinalScoreValue ());
		SceneManager.LoadScene ("Scene");
	}

	private void InstantiateArrows ()
	{
		for (int arrowNum = 1; arrowNum <= maxNumArrow; arrowNum++) {
			GameObject _arrow = (GameObject)Instantiate (arrow);
			_arrow.transform.position = Vector3.zero;
			_arrow.transform.parent = quiver;
			_arrow.name = "Arrow";
			_arrow.SetActive (false);
			arrowBank.Add (_arrow);
		}
	}

	private void InstantiateEnemies ()
	{
		for (int enemyCount = 1; enemyCount <= maxEnemies; enemyCount++) {
			GameObject _enemy = (GameObject)Instantiate (enemy);
			_enemy.transform.parent = enemyContainer;
			_enemy.transform.localPosition = GetRandonPosition ();
			_enemy.name = "Enemy " + enemyCount.ToString (); 
			enemies.Add (_enemy);
		}
	}


	private Vector2 xRange = new Vector2 (-42f, 42f);
	private Vector2 zRange = new Vector2 (0f, 33f);

	// Random position for enemies
	private Vector3 GetRandonPosition ()
	{
		Vector3 newVec = new Vector3 (Random.Range (xRange.x, xRange.y), 2, Random.Range (zRange.x, zRange.y));
		return newVec;
	}

	// Remove enemy when hit by an arrow
	public void RemoveEnemyAndUpdateScore (GameObject _enemy)
	{
		if (enemies.Count != null) {
			enemies.Remove (_enemy);
			canvasController.UpdateEnemy (enemies.Count);
			canvasController.UpdateScore (Score);

			if (enemies.Count == 0) {
				StartCoroutine ("GameOverCountDown");
			}
		} 
	}

	public void UpdateArrow ()
	{
		if (arrowBank.Count != null) {
			canvasController.UpdateArrow (arrowBank.Count);
			if (arrowBank.Count == 0) {
				StartCoroutine ("GameOverCountDown");
			}
		}
	}

	// Time wait for empty arrows and zero enemies
	private IEnumerator GameOverCountDown ()
	{
		yield return new WaitForSeconds (2);
		if (ActionGameOver != null) {
			ActionGameOver ();
		}
		yield return 0;
	}
}
