using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject player;

	public float speed;

	void Start () {

		Screen.orientation = ScreenOrientation.Portrait;

	}

	public IEnumerator Shake () {

		float elapsed = 0;
		float duration = 1;
		float magnitude = 0.1f;

		Vector3 originalPos = Camera.main.transform.position;

		while (elapsed < duration && !GameObject.Find ("Creator").GetComponent <GameController>().gameover) {

			elapsed += Time.deltaTime;

			float percentComplete = elapsed / duration;
			float damper = 1 - Mathf.Clamp (4 * percentComplete - 3, 0, 1);

			float x = Random.value * 2 - 1;
			float y = Random.value * 2 - 1;

			x *= magnitude * damper;
			y *= magnitude * damper;

			Camera.main.transform.position = new Vector3 (originalPos.x + x, originalPos.y + y, originalPos.z);

			yield return null;

		}

		Camera.main.transform.position = originalPos;
	}

}
