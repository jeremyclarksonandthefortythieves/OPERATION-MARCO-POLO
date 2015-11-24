using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	private bool destroyed = false;
	public GameObject soundTrig;
	public bool shotFromPlayer = false;

	void SoundTrigger(float rad) {
		if (!destroyed && shotFromPlayer) { 
			GameObject st = Instantiate(soundTrig, gameObject.transform.position, transform.rotation) as GameObject;
			st.GetComponent<SphereCollider>().radius = rad;
			destroyed = true;
		}
		Destroy(gameObject);
	}

	void OnCollisionEnter(Collision coll) {
		switch (coll.gameObject.tag) {
			default:
				SoundTrigger(10f);
				break;
			case "Enemy":
				SoundTrigger(2f);
				break;
		}
	}
}
