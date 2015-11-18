using UnityEngine;
using System.Collections;

public class enemyAI : MonoBehaviour {

	public float chaseSpeed = 2.0f;
	public float chaseWaitTime = 5f;

	private enemySight EnemySight;
	private Transform player;
	private NavMeshAgent navAgent;
	private LastPlayerSighting lastPlayerSighting;
	private float ChaseTimer;

	// Use this for initialization
	void Awake () {
		EnemySight = GetComponent<enemySight> ();
		navAgent = GetComponent<NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		lastPlayerSighting = GameObject.FindGameObjectWithTag ("GameController").GetComponent<LastPlayerSighting> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (EnemySight.personalLastSighting != lastPlayerSighting.resetPosition)
			Chase ();
	}

	void Chase()
	{
		Vector3 sightingDeltaPos = EnemySight.personalLastSighting - transform.position;

		if (sightingDeltaPos.sqrMagnitude > 4f)
			navAgent.destination = EnemySight.personalLastSighting;

		navAgent.speed = chaseSpeed;

		if (navAgent.remainingDistance < navAgent.stoppingDistance) {
			ChaseTimer += Time.deltaTime;

			if (ChaseTimer >= chaseWaitTime) {
				lastPlayerSighting.position = lastPlayerSighting.resetPosition;
				EnemySight.personalLastSighting = lastPlayerSighting.resetPosition;
				ChaseTimer = 0;
			}

		} else
			ChaseTimer = 0;

	}
}
