using UnityEngine;
using System.Collections;

public class CameraLerpTo : MonoBehaviour {

	public Transform mainCamera;
	public Transform OrginalPos;

	private Vector3 moveVector;
	void Start() {
		moveVector = Vector3.zero;
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
	}
	
	void Update () {
		mainCamera.position = Vector3.Lerp(mainCamera.position, transform.position, 0.05f);

		if (Input.GetKey(KeyCode.Mouse1)) {
			moveVector.x = Input.GetAxis("Mouse X")/2f;
			moveVector.z = Input.GetAxis("Mouse Y")/2f;

			transform.position += moveVector;
		} else {
			transform.position = Vector3.Lerp(transform.position, OrginalPos.position, 0.1f);
		}
		
	}
}