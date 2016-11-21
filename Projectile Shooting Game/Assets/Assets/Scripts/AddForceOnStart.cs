﻿using UnityEngine;
using System.Collections;

public class AddForceOnStart : MonoBehaviour
{
	// Force when activitating the arrow
	public float force = 100.0f;
	public ForceMode forceMode;

	// Use this for initialization
	void Start ()
	{
		GetComponent<Rigidbody> ().AddForce (transform.forward * force, forceMode);
	}
}
