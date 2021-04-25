using UnityEngine;
using System.Collections;

public class SwerverController : MonoBehaviour {

	void OnBecameInvisible () {

		Destroy (gameObject);
	}

	void Update () {

		if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270) {

			transform.eulerAngles = new Vector3 (0, 0, transform.eulerAngles.z + 180);
		}

	}


}
