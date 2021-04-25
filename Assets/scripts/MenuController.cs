using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour {

	public AudioSource music;

	public GameObject playButton, quitButton, soundButton;
	public Sprite soundOnImage, soundOffImage;

	public GameObject otherCreator;

	int soundOn;
	int highscore = 0;

	int goal;
	int explosion;
	int button;

	int color = 0;

	float lastGlowTime = 0;

	void Awake () {

		/*AudioCenter.unloadSound (goal);
		AudioCenter.unloadSound (explosion);
		AudioCenter.unloadSound (button);*/

		Time.timeScale = 1;

		if (PlayerPrefs.GetInt ("SoundOn") == 0) {

			PlayerPrefs.SetInt ("SoundOn", 1);
		}

		lastGlowTime = Time.time;
	}

	void Start () {

		//otherCreator.GetComponent <GameController> ().soundOn = true;
		highscore = PlayerPrefs.GetInt("HighScore");

		soundOn = PlayerPrefs.GetInt ("SoundOn");

		if (soundOn == 1) {

			music.Play ();
		}

	}
		

	public void OnPlay () {

		Time.timeScale = 1;
		Application.LoadLevel ("game");
	}

	public void OnQuit () {

		Application.Quit ();
	}

	public void OnMusic () {

		if (soundOn == 1) {

			soundOn = 2;
			PlayerPrefs.SetInt ("SoundOn", soundOn);
			music.Stop ();
		} 

		else {

			soundOn = 1;
			PlayerPrefs.SetInt ("SoundOn", soundOn);
			music.Play ();
		}
	}

	void Update () {

		GameObject.Find ("HighscoreText").GetComponent <Text> ().text = highscore.ToString ();

		if (soundOn == 1) {

			soundButton.GetComponent <Image> ().overrideSprite = soundOnImage;
		} 
		else {

			soundButton.GetComponent <Image> ().overrideSprite = soundOffImage;
		}

		if (Time.time > lastGlowTime + 3) {

			StartCoroutine ("Glow");

			lastGlowTime = Time.time;
		}



	}

	public IEnumerator Glow () {


		Light light = GameObject.Find ("Light").GetComponent<Light> ();;

		int newColor = Random.Range (0, 6);

		while (newColor == color) {
			newColor = Random.Range (0, 6);
		}


		if (color == 0) {

			light.color = Color.red;

		} else if (color == 1) {

			light.color = Color.blue;

		} else if (color == 2) {

			light.color = Color.yellow;

		} else if (color == 3) {

			light.color = Color.green;

		} else if (color == 4) {

			light.color = Color.cyan;

		} else {

			light.color = Color.magenta;
		}

		color = newColor;

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
}
