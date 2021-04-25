using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour {

	GameObject player;

	void Start () {

		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update () {

		if (Vector2.Distance (transform.position, player.transform.position) < 1.5f) {

			transform.position = Vector2.Lerp (transform.position, player.transform.position, 2f * Time.deltaTime);
		}
	}

	void OnTriggerEnter2D (Collider2D col) {

		if (col.gameObject == player) {

			Destroy (gameObject);
		}
	}

}
