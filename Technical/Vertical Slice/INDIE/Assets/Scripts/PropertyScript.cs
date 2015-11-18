using UnityEngine;
using System.Collections;

public class PropertyScript : MonoBehaviour {

	public int coins;
	public int keyCode;
	


	public int getCoins() {
		int c = coins;
		coins = 0;
		if (coins <= 0 && keyCode <= 0) {
			gameObject.tag = "Untagged";
		}
		return c;
	}
}
