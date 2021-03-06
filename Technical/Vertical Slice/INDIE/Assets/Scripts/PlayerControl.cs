﻿using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

	private float speed;
	private Vector3 moveDirection;
	private Rigidbody rb;
	private ArrayList[] keyCodes;
	private GameObject hidingObject;
	private GameObject gameController;
	private int health = 1;
	private Animator anim;

	//upgrades
	public float bulletDamage = 2.0f;
	public bool silencerEnabled;
	public int smokeAmount = 0;
	public int distractionAmount = 0;

	public GameObject smokeParticle;
	public bool controlsEnabled;
	public int exp;
	public GameObject walkieTalkie;
	public bool hiding;
	public int money;
	public bool sneaking;
	public GameObject soundTrigger;
	public GameObject bullet;
	public GameObject grenade;

	void Start() {
		anim = GetComponent<Animator>();
		bulletDamage = 10;
		silencerEnabled = false;
		gameController = GameObject.FindGameObjectWithTag("GameController");
		controlsEnabled = true;
		hiding = false;
		sneaking = false;
		money = 0;
		speed = 5f;
		rb = gameObject.GetComponent<Rigidbody>();
		moveDirection = new Vector3(0, 0, 0);
		gameController.GetComponent<LoadSaveScript>().LoadSkillStats();

		Destroy(GameObject.FindGameObjectWithTag("Door1"), 22);
	}

	void FixedUpdate() {
		if (controlsEnabled) {
			Control();
			InteractiveObject();
		} else {
			anim.SetBool("Walking", false);

			//press E again to 
			if (Input.GetKeyDown(KeyCode.E)) {
				hiding = false;
				controlsEnabled = true;
				//gameObject.GetComponent<MeshRenderer>().enabled = true;

				MeshRenderer[] mesh = gameObject.GetComponentsInChildren<MeshRenderer>();
				foreach (MeshRenderer m in mesh) {
					m.enabled = true;
				}
				SkinnedMeshRenderer[] mesh1 = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
				foreach (SkinnedMeshRenderer m in mesh1) {
					m.enabled = true;
				}
				//transform.position += hidingObject.transform.forward;
				hidingObject = null;
			}
		}
		AnimationHandler();
	}

	//player movement
	//also sets bools in the animator. so the animator knows what animation it should play
	void Control() {
		if (Input.GetKey(KeyCode.W)) moveDirection.z = 1f * speed;
		if (Input.GetKey(KeyCode.A)) moveDirection.x = -1f * speed;
		if (Input.GetKey(KeyCode.S)) moveDirection.z = -1f * speed;
		if (Input.GetKey(KeyCode.D)) moveDirection.x = 1f * speed;
		if (Input.GetKey(KeyCode.D)) moveDirection.x = 1f * speed;
		if (Input.GetKeyDown(KeyCode.Alpha1) && smokeAmount > 0) Shoot("smoke");
		if (Input.GetKeyDown(KeyCode.Alpha2) && distractionAmount > 0) Shoot("distraction");
		if (Input.GetKeyDown(KeyCode.Mouse0)) Shoot("bullet");
		if (Input.GetKeyDown(KeyCode.LeftShift)) {
			if (!sneaking) {
				sneaking = true;
				anim.SetBool("Crouching", true);
				speed = 2f;
			} else {
				sneaking = false;
				anim.SetBool("Crouching", false);
				speed = 5f;
			}
		}



		rb.velocity = moveDirection;
		

		moveDirection = new Vector3(0, 0, 0);

		//Where the mouse is pointing and where the player should look
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit)) {
			transform.LookAt(new Vector3(hit.point.x, 0f, hit.point.z));
		}
	}

	/*every frame checks if the player is looking at an interactive object
	  if so he press E and he will use that object
	  */
	void InteractiveObject() {
		Vector3 forward = transform.TransformDirection(Vector3.forward);
		RaycastHit hitInfo;
		if (Physics.Raycast(transform.position + (transform.up * 0.5f), forward, out hitInfo, 1f)) {
			switch (hitInfo.collider.gameObject.tag) {
				case "LootAble":
					if (Input.GetKey(KeyCode.E)) {
						PropertyScript lootScript = hitInfo.collider.gameObject.GetComponent<PropertyScript>();
						money = lootScript.getCoins();
						Debug.Log("Looted");
						hitInfo.collider.gameObject.tag = "Untagged";
					}
					break;
				case "Password":
					if (Input.GetKey(KeyCode.E)) {
						controlsEnabled = false;

						hitInfo.collider.gameObject.GetComponent<PasswordScript>().GetPassword();
					}
					break;
				case "LockedObject":
					if (Input.GetKey(KeyCode.E)) {
						controlsEnabled = false;

						hitInfo.collider.gameObject.GetComponent<TerminalScript>().UseTerminal();
					}
					break;
				case "HidingObject":
					if (Input.GetKey(KeyCode.E) && !hiding) {
						hidingObject = hitInfo.collider.gameObject;
						hiding = true;
						controlsEnabled = false;
						if (Application.loadedLevel == 3) gameController.GetComponent<LevelController>().SetObjective1();
						MeshRenderer[] mesh = gameObject.GetComponentsInChildren<MeshRenderer>();
						foreach (MeshRenderer m in mesh) {
							m.enabled = false;
						}
						SkinnedMeshRenderer[] mesh1 = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
						foreach (SkinnedMeshRenderer m in mesh1) {
							m.enabled = false;
						}
					}
					break;
				case "CompleteLevel":
					if (Input.GetKey(KeyCode.E)) {
						gameController.GetComponent<LevelController>().CompleteLevel();

					}
					break;

			}
		}
	}

	void AnimationHandler() {
		if (sneaking) {
			anim.SetBool("Crouching", true);
		} else {
			anim.SetBool("Crouching", false);
		}
		Vector3 vel = GetComponent<Rigidbody>().velocity;

		if (vel.magnitude > 0.5f) {
			anim.SetBool("Walking", true);

			//checks with dot product if character moves right,left,forward or backwards
			//Forwards, Direction 1
			if (Vector3.Dot(vel, transform.forward) > Vector3.Dot(vel, transform.right) &&
				Vector3.Dot(vel, transform.forward) > Vector3.Dot(vel, -transform.right) &&
				Vector3.Dot(vel, transform.forward) > Vector3.Dot(vel, -transform.forward)) {
				anim.SetInteger("Direction", 1);

			}
			//Backwards, Direction 2
			if (Vector3.Dot(vel, -transform.forward) > Vector3.Dot(vel, transform.right) &&
				Vector3.Dot(vel, -transform.forward) > Vector3.Dot(vel, -transform.right) &&
				Vector3.Dot(vel, -transform.forward) > Vector3.Dot(vel, transform.forward)) {
				anim.SetInteger("Direction", 2);
			}
			//Right, Direction 3
			if (Vector3.Dot(vel, transform.right) > Vector3.Dot(vel, -transform.right) &&
				Vector3.Dot(vel, transform.right) > Vector3.Dot(vel, -transform.forward) &&
				Vector3.Dot(vel, transform.right) > Vector3.Dot(vel, transform.forward)) {
				anim.SetInteger("Direction", 3);

			}
			//Left, Direction 4
			if (Vector3.Dot(vel, -transform.right) > Vector3.Dot(vel, transform.right) &&
				Vector3.Dot(vel, -transform.right) > Vector3.Dot(vel, -transform.forward) &&
				Vector3.Dot(vel, -transform.right) > Vector3.Dot(vel, transform.forward)) {
				anim.SetInteger("Direction", 4);
			}

		} else {
			anim.SetBool("Walking", false);
		}



	}

	//instantiates a gameobject which he shoots forward
	void Shoot(string type) {
		switch (type) {
			case "bullet":
				GameObject _bullet = Instantiate(bullet, transform.position + transform.forward + transform.up, transform.rotation) as GameObject;
				_bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 750f);
				_bullet.GetComponent<BulletScript>().shotFromPlayer = true;
				_bullet.GetComponent<BulletScript>().damage = Mathf.RoundToInt(bulletDamage);

				GameObject soundTrig = Instantiate(soundTrigger, transform.position, transform.rotation) as GameObject;
				GetComponent<AudioSource>().Play();
				if (silencerEnabled) {
					soundTrig.GetComponent<SphereCollider>().radius = 1f;
				} else {
					soundTrig.GetComponent<SphereCollider>().radius = 5f;
				}

				break;
			case "smoke":
				smokeAmount -= 1;
				GameObject smokeNade = Instantiate(grenade, transform.position + (transform.forward ), transform.rotation) as GameObject;
				smokeNade.GetComponent<Rigidbody>().AddForce(transform.forward * 350);
				GameObject smoke = Instantiate(smokeParticle, smokeNade.transform.position, transform.rotation) as GameObject;
				smoke.transform.SetParent(smokeNade.transform);
				Destroy(smokeNade, 6f);
				break;

			case "distraction":
				distractionAmount -= 1;
				GameObject disMine = Instantiate(grenade, transform.position + (transform.forward), transform.rotation) as GameObject;
				disMine.GetComponent<Rigidbody>().AddForce(transform.forward * 350f);
				disMine.tag = "DistractionObject";
                disMine.AddComponent<SphereCollider>();
				disMine.GetComponent<SphereCollider>().isTrigger = true;
				disMine.GetComponent<SphereCollider>().radius = 25f;
				Destroy(disMine, 5f);
				break;

		}
	}



	//void Hide(GameObject obj) {
	//	controlsEnabled = false;
	//	gameObject.GetComponent<MeshRenderer>().enabled = false;
	//}

	//player gets exp for completing objectives		
	//if he completes 2 he gets a token(money) to buy upgrades
	public void GetExp() {
		exp += 2;
		if (exp >= 2) {
			exp = 0;
			money += 3;
		}

	}


	void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag == "EndLevel") {
			gameController.GetComponent<LevelController>().CompleteLevel();

		}
	}

	

	public void GetDamage() {
		health -= 1;
		if (health <= 0) Application.LoadLevel(Application.loadedLevel);
	}


}
