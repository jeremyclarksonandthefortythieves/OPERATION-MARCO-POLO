using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

	private float speed;
	private Vector3 moveDirection;
	private Rigidbody rb;
	public int money;
	private ArrayList[] keyCodes;

	public bool sneaking;
	public GameObject bullet;
	public GunType gun;

	public enum GunType
	{
		AssualtRifle, Shotgun
	};

	void Start() {
		sneaking = false;
		money = 0;
		speed = 5f;
		rb = gameObject.GetComponent<Rigidbody>();
		moveDirection = new Vector3(0, 0, 0);
		gun = GunType.AssualtRifle;
	}

	void Update() {
		Control();
		InteractiveObject();
	}

	void Control() {
		if (Input.GetKey(KeyCode.W)) moveDirection.z = 1f * speed;
		if (Input.GetKey(KeyCode.A)) moveDirection.x = -1f * speed;
		if (Input.GetKey(KeyCode.S)) moveDirection.z = -1f * speed;
		if (Input.GetKey(KeyCode.D)) moveDirection.x = 1f * speed;
		if (Input.GetKey(KeyCode.D)) moveDirection.x = 1f * speed;
		if (Input.GetKey(KeyCode.Alpha1)) gun = GunType.AssualtRifle;
		if (Input.GetKey(KeyCode.Alpha2)) gun = GunType.Shotgun;
		if (Input.GetKeyDown(KeyCode.Mouse0)) Shoot();


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
			case GunType.AssualtRifle:
				GameObject _bullet = Instantiate(bullet, transform.position + transform.forward, transform.rotation) as GameObject;
				_bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 50f);
				break;

			case GunType.Shotgun:
				GameObject[] bullets = new GameObject[4];
				for (int i = 0; i < bullets.Length; i++) {
					bullets[i] = Instantiate(bullet, transform.position + transform.forward + (transform.right * ((i - 2f) / 5f)), transform.rotation) as GameObject;
					bullets[i].GetComponent<Rigidbody>().AddForce((transform.forward * 500f) + (transform.right * ((i - 1.5f) * 20f)));
				}
				break;
		}
	}

	void InteractiveObject() {
		//If and lootable object is in range. show it
		Vector3 forward = transform.TransformDirection(Vector3.forward);
		RaycastHit hitInfo;
		if (Physics.Raycast(transform.position, forward, out hitInfo, 0.5f)) {
			if (hitInfo.collider.gameObject.tag == "LootAble") {
				//for now it shows in the debug
				Debug.Log("Press [E]");
				if (Input.GetKey(KeyCode.E)) {
					PropertyScript lootScript = hitInfo.collider.gameObject.GetComponent<PropertyScript>();
					money = lootScript.getCoins();
					Debug.Log("Looted");
					hitInfo.collider.gameObject.tag = "Untagged";
				}
			}
		}
	}
}