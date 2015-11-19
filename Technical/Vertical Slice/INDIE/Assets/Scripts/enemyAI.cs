using UnityEngine;
using System.Collections;

public class enemyAI : MonoBehaviour {

	public float chaseSpeed = 2.0f;
	public float patrolSpeed = 1.0f;
	public float chaseWaitTime = 5f;
	public GameObject[] wayPoints;
	public float patrolWaitTime = 2f;

	private int wayPoint;
	private enemySight EnemySight;
	private Transform player;
	private NavMeshAgent navAgent;
	private LastPlayerSighting lastPlayerSighting;
	private float ChaseTimer;
	private float patrolTimer;


	// Use this for initialization
	void Awake () {
		wayPoint = 0;
		patrolTimer = 0;
		EnemySight = GetComponent<enemySight> ();
		navAgent = GetComponent<NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		lastPlayerSighting = GameObject.FindGameObjectWithTag ("GameController").GetComponent<LastPlayerSighting> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (EnemySight.personalLastSighting != lastPlayerSighting.resetPosition) {
			Invoke("Chase", 1.5f);
		} else {
			Patrol();
		}
	}

	void Patrol() {
		navAgent.speed = patrolSpeed;

		if (navAgent.remainingDistance < 0.5f || navAgent.destination == null) {
			if(wayPoint < wayPoints.Length -1){
				wayPoint++;
			}else{
				wayPoint = 0;
			}
			patrolTimer += Time.deltaTime;
			if (patrolTimer > patrolWaitTime) {
				
				navAgent.destination = wayPoints[wayPoint].transform.position;
				patrolTimer = 0f;
			}
		}
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
