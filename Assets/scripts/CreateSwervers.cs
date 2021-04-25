using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class CreateSwervers : MonoBehaviour {

	public Camera mainCamera;

	LineRenderer lineRenderer;

	public GameObject swerver;

	public Vector2 startPos;
	public Vector2 endPos;

	public bool locked = false;

	void Start () {

		lineRenderer = GetComponent <LineRenderer> ();
		lineRenderer.SetWidth (0.15f, 0.15f);
		lineRenderer.SetVertexCount (2);

	}

	void Update () {

		//mainCamera.GetComponent<CameraFollow> ().Move ();

		if (Input.touchCount > 0) {



			if (Input.touches [0].phase == TouchPhase.Began) {

				if (Time.timeScale > 0) {

					locked = false;
				}

				if (!locked) {

					startPos = Input.touches [0].position;
				}


			}

			if (!locked) {

				endPos = Input.touches [0].position;
			}


			if (Input.touches [0].phase == TouchPhase.Ended && !locked) {

				Vector2 spawnPos = Camera.main.ScreenToWorldPoint (new Vector2 (startPos.x + (endPos.x - startPos.x) / 2, startPos.y + (endPos.y - startPos.y) / 2));

				if (GameObject.FindGameObjectsWithTag ("Swerver").Length > 2) {

					Destroy (GameObject.FindGameObjectsWithTag ("Swerver") [0]);
				}

				GameObject instance = (GameObject)Instantiate (swerver, spawnPos, Quaternion.identity);
				instance.transform.localScale = new Vector3 (Mathf.Sqrt (Mathf.Pow ((Camera.main.ScreenToWorldPoint (startPos).x - Camera.main.ScreenToWorldPoint (endPos).x), 2) + Mathf.Pow ((Camera.main.ScreenToWorldPoint (startPos).y - Camera.main.ScreenToWorldPoint (endPos).y), 2)), 0.15f, 1);

				if (instance.transform.localScale.x < 0.4f || instance.transform.position.y > Camera.main.transform.position.y + Camera.main.orthographicSize - 3) {

					Destroy (instance);
				}

				float angle = Mathf.Atan2 (endPos.y - startPos.y, endPos.x - startPos.x) * Mathf.Rad2Deg;
				instance.transform.localRotation = Quaternion.AngleAxis (angle, Vector3.forward);
			}

			if (Input.touches [0].phase == TouchPhase.Ended && !locked) {

				startPos = endPos;


			} 

			lineRenderer.SetPosition (0, new Vector3 (Camera.main.ScreenToWorldPoint (startPos).x, Camera.main.ScreenToWorldPoint (startPos).y, 1));
			lineRenderer.SetPosition (1, new Vector3 (Camera.main.ScreenToWorldPoint (endPos).x, Camera.main.ScreenToWorldPoint (endPos).y, 1));
		
				
		} 

		if (Time.timeScale == 0) {

			endPos = startPos;

			lineRenderer.SetPosition (0, new Vector3 (0, 0, 1));
			lineRenderer.SetPosition (1, new Vector3 (0, 0, 1));
		}

	}
}
