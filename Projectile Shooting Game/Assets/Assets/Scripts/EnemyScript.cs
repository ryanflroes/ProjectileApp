using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{

	private GameManager _gManager;

	private GameManager gManager {
		get {
			return _gManager ?? (_gManager = transform.parent.root.GetComponent<GameManager> ());
		}
	}

	[SerializeField]
	private Material _red;

	private Renderer renderer;

	private float _scoreDistance = 0;

	private bool notHit = false;

	void Awake ()
	{
		renderer = GetComponent<Renderer> ();
	}

	// Enemy behaviour when hit by arrow
	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.name == "Arrow" && !notHit) {
			float distance = Vector3.Distance (gManager.player.position, transform.position);
			_scoreDistance = (int)distance;
			renderer.material = _red;
			gManager.Score += _scoreDistance;
			gManager.RemoveEnemyAndUpdateScore (gameObject);
			notHit = true;
		}
	}
}
