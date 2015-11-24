using UnityEngine;
using System.Collections;

public class SecurityCameraScript : MonoBehaviour {

	private bool startRaycast = false;

	// Update is called once per frame
	void Update () {
		if (startRaycast) {
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			float dist = Vector3.Distance(transform.position, player.transform.position);
			Vector3 dir = player.transform.position - transform.position;
			float angle = Vector3.Angle(dir.normalized * dist, transform.forward);

			if (angle < 80 * 0.5f) {
				Debug.Log("in vison camera");
			}
		}
	}

	void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag == "Player") {
			startRaycast = true;
		}
	}

	void OnTriggerExit(Collider coll) {
		if (coll.gameObject.tag == "Player") {
			startRaycast = false;
		}
	}
}
