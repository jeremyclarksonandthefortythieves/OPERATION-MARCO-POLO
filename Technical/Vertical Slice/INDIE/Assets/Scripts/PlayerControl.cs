using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

	private float speed;
	private Vector3 moveDirection;
	private Rigidbody rb;
	private ArrayList[] keyCodes;
	public bool controlsEnabled;
	private GameObject hidingObject;

	public bool hiding;
	public int money;
	public bool sneaking;
	public GameObject soundTrigger;
	public GameObject bullet;
	public GunType gun;

	public enum GunType {
		Pistol, Shotgun
	};

	void Start() {
		controlsEnabled = true;
		hiding = false;
		sneaking = false;
		money = 0;
		speed = 5f;
		rb = gameObject.GetComponent<Rigidbody>();
		moveDirection = new Vector3(0, 0, 0);
		gun = GunType.Pistol;
	}

	void FixedUpdate() {
		if (controlsEnabled) {
			Control();
			InteractiveObject();
		} else {
			Debug.Log("no controls");
			if (Input.GetKeyDown(KeyCode.E)) {
				hiding = false;
				controlsEnabled = true;
				gameObject.GetComponent<MeshRenderer>().enabled = true;
				//transform.position += hidingObject.transform.forward;
				hidingObject = null;
			}
		}
	}

	void Control() {
		if (Input.GetKey(KeyCode.W)) moveDirection.z = 1f * speed;
		if (Input.GetKey(KeyCode.A)) moveDirection.x = -1f * speed;
		if (Input.GetKey(KeyCode.S)) moveDirection.z = -1f * speed;
		if (Input.GetKey(KeyCode.D)) moveDirection.x = 1f * speed;
		if (Input.GetKey(KeyCode.D)) moveDirection.x = 1f * speed;
		if (Input.GetKey(KeyCode.Alpha1)) gun = GunType.Pistol;
		if (Input.GetKey(KeyCode.Alpha2)) gun = GunType.Shotgun;
		if (Input.GetKeyDown(KeyCode.Mouse0)) Shoot();
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


	void Shoot() {
		switch (gun) {
			case GunType.Pistol:
				GameObject _bullet = Instantiate(bullet, transform.position + transform.forward, transform.rotation) as GameObject;
				_bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 750f);
				_bullet.GetComponent<BulletScript>().shotFromPlayer = true;

				GameObject soundTrig = Instantiate(soundTrigger, transform.position, transform.rotation) as GameObject;
				soundTrig.GetComponent<SphereCollider>().radius = 5f;
				break;

			/*case GunType.Shotgun:
				GameObject[] bullets = new GameObject[4];
				for (int i = 0; i < bullets.Length; i++) {
					bullets[i] = Instantiate(bullet, transform.position + transform.forward + (transform.right * ((i - 2f) / 5f)), transform.rotation) as GameObject;
					bullets[i].GetComponent<Rigidbody>().AddForce((transform.forward * 500f) + (transform.right * ((i - 1.5f) * 20f)));
				}
				break;*/
		}
	}

	void Hide(GameObject obj) {
		controlsEnabled = false;
		gameObject.GetComponent<MeshRenderer>().enabled = false;
	}
			

	void InteractiveObject() {
		//raycasts for objects if interactable
		Vector3 forward = transform.TransformDirection(Vector3.forward);
		RaycastHit hitInfo;
		Debug.DrawRay(transform.position, forward);
		if (Physics.Raycast(transform.position, forward, out hitInfo, 1f)) {
			Debug.Log(hitInfo.collider.gameObject.tag);
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

			}
		}
	}
}
