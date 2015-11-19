﻿using UnityEngine;
using System.Collections;

public class enemySight : MonoBehaviour {

	public float fieldOfView = 110f;
	public bool playerInSight;
	public Vector3 personalLastSighting;

	private NavMeshAgent navAgent;
	private SphereCollider col;
	private LastPlayerSighting lastPlayerSighting;
	private GameObject player;
	private Vector3 previousSighting;

	// Use this for initialization
	void Awake () {
		navAgent = GetComponent<NavMeshAgent> ();
		col = GetComponent<SphereCollider> ();
		lastPlayerSighting = GameObject.FindGameObjectWithTag ("GameController").GetComponent<LastPlayerSighting> ();
		player = GameObject.FindGameObjectWithTag ("Player");

		personalLastSighting = lastPlayerSighting.resetPosition;
		previousSighting = lastPlayerSighting.resetPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (lastPlayerSighting.position != previousSighting)
			personalLastSighting = lastPlayerSighting.position;

		previousSighting = lastPlayerSighting.position;
	//	float dist = Vector3.Distance(transform.position, player.transform.position);
		//Vector3 direction = player.transform.position - transform.position;
	//	Debug.DrawRay(transform.position, direction.normalized * dist, Color.red);
	}

	void OnTriggerStay(Collider other)
	{
		

		if (other.gameObject == player) {
		
			playerInSight = false;
			float dist = Vector3.Distance(transform.position, player.transform.position);
			Vector3 dir = player.transform.position - transform.position;
			float angle = Vector3.Angle(dir.normalized * dist, transform.forward);

			//Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
			//transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 1f);

			if(player.GetComponent<PlayerControl>().sneaking == false) {
				personalLastSighting = player.transform.position;
			}

			Debug.DrawRay(transform.position, dir.normalized * dist, Color.red);

			if (angle < fieldOfView * 0.5f) {
				RaycastHit hit;
				Debug.Log("player in sight");
				if (Physics.Raycast(transform.position, dir.normalized * dist, out hit, col.radius)) {
					if (hit.collider.gameObject == player) {
						transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir, Vector3.up), 0.1f);
						playerInSight = true;
						Debug.Log("player seen");
						lastPlayerSighting.position = player.transform.position;

					}

				}

			}
		}

	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == player)
			playerInSight = false;

	}

	float CalculatePathLenght(Vector3 targetPosition)
	{
		NavMeshPath path = new NavMeshPath ();
		if (navAgent.enabled)
			navAgent.CalculatePath (targetPosition, path);

		Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

		allWayPoints [0] = transform.position;

		allWayPoints [allWayPoints.Length - 1] = targetPosition;

		for (int i = 0; i < allWayPoints.Length - 1; i++) {

			allWayPoints[i + 1] = path.corners[i];
		}

		float pathLength = 0;

		for (int i = 0; i < allWayPoints.Length - 1; i++) {
			pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);

		}

		return pathLength;
	}
}