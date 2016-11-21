using UnityEngine;
using System.Collections;

[AddComponentMenu("Spawn/Spawn On Button")]
public class SpawnOnButton : MonoBehaviour
{
	public GameObject objectToSpawn;
	public string buttonName = "Fire1";

	private GameManager gManager = null;

	void Awake(){
		gManager =  transform.parent.root.GetComponent<GameManager>();
	}

	void Update ()
	{
		if(Input.GetButtonDown(buttonName))
		{
			if (gManager.StillHaveArrow()){
				GameObject arrow = gManager.GetArrow;
				arrow.transform.parent = gManager.transform;
				arrow.transform.position = transform.position;	
				arrow.transform.rotation = transform.rotation;
				arrow.SetActive(true);
				gManager.UpdateArrow();
			}
		}
	}
}
