using UnityEngine;
using System.Collections;

public class AddForceOnStart : MonoBehaviour
{
	// Force when activitating the arrow
	public float force = 100.0f;
	public ForceMode forceMode;

	private Rigidbody rbody;

	// Use this for initialization
	void Start ()
	{
		rbody = GetComponent<Rigidbody> ();
		rbody.AddForce (transform.forward * force, forceMode);
//		rbody.velocity;
	}

	void LateUpdate ()
	{
		if (gameObject.active && rbody.velocity != Vector3.zero) {
			transform.forward = rbody.velocity;	
		}
	}

	void OnCollisionEnter ()
	{
		gameObject.SetActive (false);
	}
}
