﻿using UnityEngine;
using System.Collections;

public class ParticleRemoveScript : MonoBehaviour {

	private ParticleSystem ps;

	// Use this for initialization
	void Start () {
		ps = GetComponent<ParticleSystem> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (!ps.IsAlive()) {
			Destroy(this.gameObject);
		}
	}
}
