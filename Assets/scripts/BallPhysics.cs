using UnityEngine;
using System.Collections;

public class BallPhysics : MonoBehaviour {

	public float speed;
	public float deceleration;
	public float terminalVelocity = -12;
	public Vector2 velocity;
	public float gravity;

	private float angle = 0;

	void Update () {

		if (velocity.y < terminalVelocity) {

			velocity.y = terminalVelocity;
		}

		if (transform.position.y < Camera.main.transform.position.y - Camera.main.orthographicSize - 1) {

			//GameObject.Find ("Creator").GetComponent<GameController> ().lifeTarget -= 20;
			Destroy (gameObject);
		}
	}

	void FixedUpdate () {

		velocity.y -= gravity * Time.fixedDeltaTime;

		velocity.x = Mathf.Lerp (velocity.x, 0, deceleration * Time.fixedDeltaTime);


		transform.Translate (velocity * Time.fixedDeltaTime);

	}


	void OnTriggerEnter2D (Collider2D col) {

		if (col.gameObject.tag == "Swerver") {

			//StartCoroutine (GameObject.Find ("Main Camera").GetComponent <Audio> ().Bounce ());

			angle = col.transform.eulerAngles.z;

			if (angle > 180) {

				angle -= 180;
			}

			float newAngle = 0;

			if (angle > 0 && angle < 90) {

				newAngle = angle + 90;
			} else if (angle >= 90 && angle < 180) {

				newAngle = angle - 90;
			}

			angle = newAngle;

			velocity.x = speed * Mathf.Cos (angle * Mathf.Deg2Rad);
			velocity.y = speed * Mathf.Sin (angle * Mathf.Deg2Rad);

			Destroy (col.gameObject);
		} 

		else if (col.gameObject.tag == "Wall") {

			//StartCoroutine (GameObject.Find ("Main Camera").GetComponent <Audio> ().Bounce ());

			velocity.x = velocity.x * -1.2f;
			velocity.y += 2;
		} 

		else if (col.gameObject.tag == "Goal") {

			if (gameObject.tag == "Ball") {

				GameObject.Find ("Main Camera").GetComponent <Audio> ().Goal ();

				GameObject.FindGameObjectWithTag ("Creator").GetComponent<GameController> ().score++;
				GameObject.FindGameObjectWithTag ("Creator").GetComponent<GameController> ().combo++;

				if (!GameObject.Find ("Creator").GetComponent<GameController> ().gameover) {

					GameObject.Find ("Creator").GetComponent<GameController> ().lifeTarget += 25;
				}

				GameObject.FindGameObjectWithTag ("Creator").GetComponent<SidesController> ().ChangeGoals (col.gameObject);

				object[] parameters = new object [2];

				if (col.name == ("Left")) {

					parameters [0] = "left";
				} 

				else {

					parameters [0] = "right";
				}

				parameters [1] = gameObject.GetComponent <Renderer> ().material.color;

				GameObject.Find ("Creator").GetComponent <SidesController> ().StartCoroutine ("Glow", parameters);
			} 

			else if (gameObject.tag == "Bomb") {

				GameObject.Find ("Main Camera").GetComponent <Audio> ().Explosion ();

				GameObject.Find ("Creator").GetComponent<GameController> ().lifeTarget -= 30;
				Camera.main.GetComponent <CameraFollow> ().StartCoroutine ("Shake");


			}

			Destroy (gameObject);
		

		}



	}
}
