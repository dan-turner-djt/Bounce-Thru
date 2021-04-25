using UnityEngine;
using System.Collections;

public class SpawnBalls : MonoBehaviour {

	public GameObject Ball;
	public GameObject Bomb;

	private float lastSpawnTime = 0;
	private float lastBombSpawnTime = 0;
	public float waitSpawnTime = 3;
	int color = 0;

	void Start () {

		lastSpawnTime = Time.time;
		lastBombSpawnTime = Time.time;
	}

	void Update () {

		if (Time.time > lastSpawnTime + Random.Range (30 - GameObject.FindGameObjectWithTag ("Creator").GetComponent<GameController> ().score, 30)/10) {

			GameObject[] balls = GameObject.FindGameObjectsWithTag ("Ball");

			if (balls.Length < 5) {

				GameObject instance = (GameObject) Instantiate (Ball, new Vector3 (Camera.main.transform.position.x + Random.Range (-1, 2), Camera.main.transform.position.y + 5, -0.5f), Quaternion.identity);

				int newColor = Random.Range (0, 6);

				while (newColor == color) {
					newColor = Random.Range (0, 6);
				}


				if (color == 0) {
					instance.GetComponent<Renderer> ().material.color = Color.red;
					instance.GetComponent<Light>().color = Color.red;
				} else if (color == 1) {
					instance.GetComponent<Renderer> ().material.color = Color.blue;
					instance.GetComponent<Light>().color = Color.blue;
				} else if (color == 2) {
					instance.GetComponent<Renderer> ().material.color = Color.yellow;
					instance.GetComponent<Light>().color = Color.yellow;
				} else if (color == 3) {
					instance.GetComponent<Renderer> ().material.color = Color.green;
					instance.GetComponent<Light>().color = Color.green;
				} else if (color == 4) {
					instance.GetComponent<Renderer> ().material.color = Color.cyan;
					instance.GetComponent<Light>().color = Color.cyan;
				} else {
					instance.GetComponent<Renderer> ().material.color = Color.magenta;
					instance.GetComponent<Light>().color = Color.magenta;
				}

				color = newColor;

				//instance.GetComponent<Renderer> ().material.color = new Color (Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), 0);


				lastSpawnTime = Time.time;
			}

		}

		if (GetComponent <GameController> ().score == 10) {

			lastBombSpawnTime = Time.time;
		}

		if (Time.time > lastBombSpawnTime + Mathf.Max (Random.Range (6, 14) - GetComponent <GameController>().score/12, 2) && GetComponent <GameController>().score >= 10) {

			if (Random.Range (0, 100) > Mathf.Max (70 - GetComponent <GameController>().score, 20)) {

				for (int i = 0; i < Random.Range (1, 4); i++) {

					Instantiate (Bomb, new Vector2 (Camera.main.transform.position.x + Random.Range (-1, 2), Camera.main.transform.position.y + 5 + i + 1), Quaternion.identity);
				}
			}

			GameObject instance = (GameObject) Instantiate (Bomb, new Vector2 (Camera.main.transform.position.x + Random.Range (-1, 2), Camera.main.transform.position.y + 5), Quaternion.identity);

			lastBombSpawnTime = Time.time;
		}


	}
}
