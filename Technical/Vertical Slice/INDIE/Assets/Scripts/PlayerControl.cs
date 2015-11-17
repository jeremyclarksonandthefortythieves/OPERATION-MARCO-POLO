using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	private float speed;
	private Vector3 moveDirection;
	private Rigidbody rb;

	public GameObject bullet;
	public GunType gun;

	public enum GunType
	{
		AssualtRifle,Shotgun
	};

	// Use this for initialization
	void Start () {
		speed = 5f;
		rb = gameObject.GetComponent<Rigidbody> ();
		moveDirection = new Vector3 (0, 0, 0);
		gun = GunType.AssualtRifle;
	}
	
	// Update is called once per frame
	void Update () {
		Control ();
	}

	void Control(){
		if (Input.GetKey (KeyCode.W)) moveDirection.z = 1f * speed;
		if (Input.GetKey (KeyCode.A)) moveDirection.x = -1f * speed;
		if (Input.GetKey (KeyCode.S)) moveDirection.z = -1f * speed;
		if (Input.GetKey (KeyCode.D)) moveDirection.x = 1f * speed;
		if (Input.GetKey (KeyCode.D)) moveDirection.x = 1f * speed;
		if (Input.GetKey (KeyCode.Alpha1)) gun = GunType.AssualtRifle;
		if (Input.GetKey (KeyCode.Alpha2)) gun = GunType.Shotgun;
		
		if (Input.GetKeyDown (KeyCode.Mouse0)) Shoot ();
		

		rb.velocity = transform.TransformDirection (moveDirection);
		

		moveDirection = new Vector3 (0, 0, 0);

		//Where the mouse is pointing and where the player should look
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit)){
			if(hit.collider.gameObject.tag == "Terrain"){
				transform.LookAt(new Vector3(hit.point.x, 0.5f, hit.point.z));
			}
		}
	}

	void Shoot(){
		switch (gun) {
			case GunType.AssualtRifle:
				GameObject _bullet = Instantiate (bullet, transform.position + transform.forward, transform.rotation) as GameObject;
				_bullet.GetComponent<Rigidbody> ().AddForce (transform.forward * 500f);
				break;

			case GunType.Shotgun:
				GameObject[] bullets = new GameObject[4];
				for(int i=0;i<bullets.Length;i++){
					bullets[i] = Instantiate (bullet, transform.position + transform.forward + (transform.right *((i-2f)/5f)), transform.rotation) as GameObject;
					bullets[i].GetComponent<Rigidbody> ().AddForce ((transform.forward *500f) + (transform.right * ((i-1.5f)*20f)) );
				}
				break;
		}

		
	}
}
