using UnityEngine;
using System.Collections;

public class SidesController : MonoBehaviour {



	public GameObject goalLeft;
	public GameObject goalRight;

	public Light lightLeft;
	public Light lightRight;

	public GameObject wallTopLeft;
	public GameObject wallBottomLeft;
	public GameObject wallTopRight;
	public GameObject wallBottomRight;

	private float camHeight;
	private float camWidth;

	private float goalLeftTarget;
	private float goalRightTarget;

	private float dirLeft = 1;
	private float dirRight = -1;
	private float speed;

	private float targetLeft = 6;
	private float targetRight = 1;


	private float upperBoundaryLeft;
	private float lowerBoundaryLeft;
	private float upperBoundaryRight;
	private float lowerBoundaryRight;

	void Start () {

		camHeight = Camera.main.orthographicSize * 2;
		camWidth = Camera.main.aspect * camHeight;

		goalLeft.transform.position = new Vector2 (Camera.main.transform.position.x - camWidth/2 - 0.4f, Camera.main.transform.position.y);
		goalRight.transform.position = new Vector2 (Camera.main.transform.position.x + camWidth/2 + 0.4f, Camera.main.transform.position.y);

		goalLeftTarget = goalLeft.transform.position.y;
		goalRightTarget = goalRight.transform.position.y;

		lightLeft.intensity = 0;
		lightRight.intensity = 0;
	}

	void Update () {

		speed = Mathf.Min (1 + (GameObject.FindGameObjectWithTag ("Creator").GetComponent<GameController> ().score - 10) / 12, 5);

		if (GameObject.FindGameObjectWithTag ("Creator").GetComponent<GameController> ().score < 10) {

			goalLeft.transform.position = new Vector2 (goalLeft.transform.position.x, Mathf.Lerp (goalLeft.transform.position.y, goalLeftTarget, 2 * Time.deltaTime));
			goalRight.transform.position = new Vector2 (goalRight.transform.position.x, Mathf.Lerp (goalRight.transform.position.y, goalRightTarget, 2 * Time.deltaTime));
		} 

		else if (GameObject.FindGameObjectWithTag ("Creator").GetComponent<GameController> ().score < 30 || GameObject.FindGameObjectWithTag ("Creator").GetComponent<GameController> ().score >= 50) {

			goalLeft.transform.Translate (new Vector2 (0, speed * dirLeft * Time.deltaTime));
			goalRight.transform.Translate (new Vector2 (0, speed * dirRight * Time.deltaTime));

			if (goalLeft.transform.position.y >= upperBoundaryLeft || goalLeft.transform.position.y <= lowerBoundaryLeft) {

				goalLeft.transform.Translate (0, speed * -dirLeft * Time.deltaTime, 0);
				dirLeft *= -1;

			}

			if (goalRight.transform.position.y >= upperBoundaryRight || goalRight.transform.position.y <= lowerBoundaryRight) {

				goalRight.transform.Translate (0, speed * -dirRight * Time.deltaTime, 0);
				dirRight *= -1;
			}
		} 

		if (GameObject.FindGameObjectWithTag ("Creator").GetComponent<GameController> ().score >= 30) {


			goalLeft.transform.localScale = new Vector2 (goalLeft.transform.lossyScale.x, Mathf.Lerp (goalLeft.transform.lossyScale.y, targetLeft, Time.deltaTime)); 

			if (goalLeft.transform.lossyScale.y >= 5.5f) {

				targetLeft = 1;
			}
			else if (goalLeft.transform.lossyScale.y <= 1.5f) {

				targetLeft = 6;
			}

			goalRight.transform.localScale = new Vector2 (goalRight.transform.lossyScale.x, Mathf.Lerp (goalRight.transform.lossyScale.y, targetRight, Time.deltaTime)); 

			if (goalRight.transform.lossyScale.y >= 5.5f) {

				targetRight = 1;
			}
			else if (goalRight.transform.lossyScale.y <= 1.5f) {

				targetRight = 6;
			}
		}




		if (goalLeft.transform.position.y < Camera.main.transform.position.y - camHeight / 2 - goalLeft.transform.lossyScale.y / 2 - 0.5f) {

			goalLeft.transform.position = new Vector2 (goalLeft.transform.position.x, Camera.main.transform.position.y + camHeight / 2 + goalLeft.transform.lossyScale.y / 2 + Random.Range (0, 8));

		}

		if (goalRight.transform.position.y < Camera.main.transform.position.y - camHeight / 2 - goalRight.transform.lossyScale.y / 2 - 0.5f) {

			goalRight.transform.position = new Vector2 (goalRight.transform.position.x, Camera.main.transform.position.y + camHeight / 2 + goalRight.transform.lossyScale.y / 2 + Random.Range (0, 8));

		}


		wallTopLeft.transform.position = new Vector3 (goalLeft.transform.position.x, goalLeft.transform.position.y + goalLeft.transform.lossyScale.y / 2 + wallTopLeft.transform.lossyScale.x/2, -0.5f);
		wallBottomLeft.transform.position = new Vector3 (goalLeft.transform.position.x, goalLeft.transform.position.y - goalLeft.transform.lossyScale.y / 2 - wallBottomLeft.transform.lossyScale.x/2, -0.5f);

		wallTopRight.transform.position = new Vector3 (goalRight.transform.position.x, goalRight.transform.position.y + goalRight.transform.lossyScale.y / 2 + wallTopRight.transform.lossyScale.x/2, -0.5f);
		wallBottomRight.transform.position = new Vector3 (goalRight.transform.position.x, goalRight.transform.position.y - goalRight.transform.lossyScale.y / 2 - wallBottomRight.transform.lossyScale.x/2, -0.5f);

		SideLights ();
	}

	public void SideLights () {

		lightLeft.transform.position = new Vector3 (goalLeft.transform.position.x, goalLeft.transform.position.y, 1);
		lightRight.transform.position = new Vector3 (goalRight.transform.position.x, goalRight.transform.position.y, 1);


	}

	public IEnumerator Glow (object[] parameters) {

		string side = parameters [0].ToString();
		Color color = (Color) parameters [1];

		Light light;

		if (side == "left") {
			 
			light = GameObject.Find ("LightLeft").GetComponent<Light> ();
		} 

		else {

			light = GameObject.Find ("LightRight").GetComponent<Light> ();
		}

		light.color = color;

		float elapsed = 0;
		float time = 0.5f;

		while (elapsed < time) {

			light.intensity = Mathf.Min (light.intensity + 1f * elapsed / time, 0.5f);
			elapsed += Time.deltaTime;
			yield return null;
		}

		elapsed = 0;
		time = 1;

		while (elapsed < time) {

			light.intensity -= 0.2f * elapsed / time;
			elapsed += Time.deltaTime;
			yield return null;
		}
	}

	public void ChangeGoals (GameObject goal) {

		float lowerRange = Camera.main.transform.position.y - camHeight / 2 + goalLeft.transform.lossyScale.y / 2 + 0.5f;
		float upperRange = Camera.main.transform.position.y + camHeight / 2 - goalLeft.transform.lossyScale.y / 2 - 1;

		if (GameObject.FindGameObjectWithTag ("Creator").GetComponent<GameController> ().score < 5) {

			if (goal.name == "Left") {

				goalLeftTarget = Random.Range (lowerRange * 100, upperRange * 100) / 100;
			} 

			else {

				goalRightTarget = Random.Range (lowerRange * 100, upperRange * 100) / 100;
			}
		} 

		else if (GameObject.FindGameObjectWithTag ("Creator").GetComponent<GameController> ().score >= 10) {

			goalLeftTarget = upperRange;
			goalRightTarget = lowerRange;

			upperBoundaryLeft = Camera.main.transform.position.y + camHeight / 2 - goalLeft.transform.lossyScale.y / 2 - 1;
			lowerBoundaryLeft = Camera.main.transform.position.y - camHeight / 2 + goalLeft.transform.lossyScale.y / 2 + 0.5f;

			upperBoundaryRight = Camera.main.transform.position.y + camHeight / 2 - goalRight.transform.lossyScale.y / 2 - 1;
			lowerBoundaryRight = Camera.main.transform.position.y - camHeight / 2 + goalRight.transform.lossyScale.y / 2 + 0.5f;


		}

		else {

			goalLeftTarget = Random.Range (lowerRange * 100, upperRange * 100) / 100;
			goalRightTarget = Random.Range (lowerRange * 100, upperRange * 100) / 100;
		}


	}


}
