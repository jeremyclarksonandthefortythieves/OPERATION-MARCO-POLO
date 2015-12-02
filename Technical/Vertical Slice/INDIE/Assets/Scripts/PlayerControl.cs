﻿using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

	private float speed;
	private Vector3 moveDirection;
	private Rigidbody rb;
	private ArrayList[] keyCodes;
	public bool controlsEnabled;
	private GameObject hidingObject;
	private GameObject gameController;
	private int health = 2;

	//upgrades
	public int bulletDamage;
	public bool silencerEnabled;
	public int smokeAmount = 0;
	public int distractionAmount = 0;

	public int exp;
	public GameObject walkieTalkie;
    public bool hiding;
	public int money;
	public bool sneaking;
	public GameObject soundTrigger;
	public GameObject bullet;
	public GameObject smokeGrenade;
	public GameObject distractionMine;

	void Start() {
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
	}

	void FixedUpdate() {
		if (controlsEnabled) {
			Control();
			InteractiveObject();
		} else {
			Debug.Log("no controls");
			//press E again to 
			if (Input.GetKeyDown(KeyCode.E)) {
				hiding = false;
				controlsEnabled = true;
				gameObject.GetComponent<MeshRenderer>().enabled = true;
				//transform.position += hidingObject.transform.forward;
				hidingObject = null;
			}
		}
	}

	//player movement
	void Control() {
		if (Input.GetKey(KeyCode.W)) moveDirection.z = 1f * speed;
		if (Input.GetKey(KeyCode.A)) moveDirection.x = -1f * speed;
		if (Input.GetKey(KeyCode.S)) moveDirection.z = -1f * speed;
		if (Input.GetKey(KeyCode.D)) moveDirection.x = 1f * speed;
		if (Input.GetKey(KeyCode.D)) moveDirection.x = 1f * speed;
		if (Input.GetKeyDown(KeyCode.Alpha1) && distractionAmount > 0) Shoot("smoke");
		if (Input.GetKeyDown(KeyCode.Alpha2) && distractionAmount > 0) Shoot("distraction");
		if (Input.GetKeyDown(KeyCode.Mouse0)) Shoot("bullet");
		if (Input.GetKeyDown(KeyCode.LeftShift)) {
			if (!sneaking) {
				sneaking = true;
				speed = 2f;
			} else {
				sneaking = false;
				speed = 5f;
			}
		}



		rb.velocity = moveDirection;


		moveDirection = new Vector3(0, 0, 0);

		//Where the mouse is pointing and where the player should look
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit)) {
			transform.LookAt(new Vector3(hit.point.x, 0.5f, hit.point.z));
		}
	}

	//instantiates a gameobject which he shoots forward
	void Shoot(string type) {
		switch (type) {
			case "bullet":
				GameObject _bullet = Instantiate(bullet, transform.position + (transform.forward * 0.5f), transform.rotation) as GameObject;
				_bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 250f);
				_bullet.GetComponent<BulletScript>().shotFromPlayer = true;
				_bullet.GetComponent<BulletScript>().damage = 5;

				GameObject soundTrig = Instantiate(soundTrigger, transform.position, transform.rotation) as GameObject;
				soundTrig.GetComponent<SphereCollider>().radius = 5f;
				break;
			case "smoke":
				GameObject smokeNade = Instantiate(smokeGrenade, transform.position + (transform.forward * 0.5f), transform.rotation) as GameObject;
				smokeNade.GetComponent<Rigidbody>().AddForce(transform.forward * 250f);
				break;

			case "distraction":
				GameObject disMine = Instantiate(distractionMine, transform.position + (transform.forward * 0.5f), transform.rotation) as GameObject;
				disMine.GetComponent<Rigidbody>().AddForce(transform.forward * 250f);
				break;

		}
	}

	void Hide(GameObject obj) {
		controlsEnabled = false;
		gameObject.GetComponent<MeshRenderer>().enabled = false;
	}
	
	//player gets exp for completing objectives		
	//if he completes 2 he gets a token(money) to buy upgrades
	public void GetExp() {
		exp += 1;
		if (exp >= 2) {
			exp = 0;
			money += 1;
		}

	}

	
	void OnTriggerEnter(Collider coll) {

		//if player hits a soundtrigger. sound about story will play
		if (coll.gameObject.tag == "SoundTrigger") {
			coll.GetComponent<AudioSource>().Play();
			GameObject wT = Instantiate(walkieTalkie);
			Destroy(wT, coll.GetComponent<AudioSource>().clip.length);

		}
	}

	/*every frame checks if the player is looking at an interactive object
	  if so he press E and he will use that object
	  */

	public void GetDamage() {
		health -= 1;
		if (health <= 0) Debug.Log("gameover");
	}

	void InteractiveObject() {
		Vector3 forward = transform.TransformDirection(Vector3.forward);
		RaycastHit hitInfo;
		if (Physics.Raycast(transform.position, forward, out hitInfo, 1f)) {
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
						Hide(hitInfo.collider.gameObject);
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
}
