﻿using UnityEngine;
using System.Collections;

public class enemyAI : MonoBehaviour {

	public float chaseSpeed = 4.0f;
	public float patrolSpeed = 1.0f;
	public float chaseWaitTime = 5f;
	public GameObject[] wayPoints;
	public float patrolWaitTime = 2f;

	private Animator anim;
	private bool alerted;
	private int health;
	private int wayPoint;
	private enemySight EnemySight;
    private enemyShooting Shooting;
	private Transform player;
	private NavMeshAgent navAgent;
	private LastPlayerSighting lastPlayerSighting;
	private float ChaseTimer;
	private float patrolTimer;


	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator>();
		alerted = false;
		health = 10;
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
			alerted = true;
        } else {
			Patrol();
			alerted = false;
		}

	}

	void AnimatorControl() {
		if (navAgent.velocity.magnitude > 0.2f) {
			anim.SetBool("Walking", true);
		} else {
			anim.SetBool("Walking", false); 
		}
	}

	void Patrol() {
		navAgent.speed = patrolSpeed;
        navAgent.stoppingDistance = 0.5f;

		if (navAgent.remainingDistance < 0.5f || navAgent.destination == null) {
			patrolTimer += Time.deltaTime;
			if (patrolTimer > patrolWaitTime) {
				if (wayPoint < wayPoints.Length - 1) {
					wayPoint += 1;
				} else {
					wayPoint = 0;
				}
				navAgent.destination = wayPoints[wayPoint].transform.position;
				patrolTimer = 0f;
			}
		}
	}

	void Chase(){
		Vector3 sightingDeltaPos = EnemySight.personalLastSighting - transform.position;
		if (sightingDeltaPos.sqrMagnitude > 4f)
			navAgent.destination = EnemySight.personalLastSighting;

            navAgent.stoppingDistance = 4;
            navAgent.speed = chaseSpeed;

        if (navAgent.remainingDistance < navAgent.stoppingDistance) {
			ChaseTimer += Time.deltaTime;

            // If enemy losses sight of player it waits sometime and then resets player sighting positions and returns patrolling
			if (ChaseTimer >= chaseWaitTime) {
				lastPlayerSighting.position = lastPlayerSighting.resetPosition;
				EnemySight.personalLastSighting = lastPlayerSighting.resetPosition;
				ChaseTimer = 0;
			}

		} else
			ChaseTimer = 0;

	}

	public void GetDamage(int i) {
		health -= i;
		if(health <= 0 || !alerted) {
			//die
			Destroy(gameObject);

		}
	}

}