using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	public AudioSource music;

	public bool gameover = false;

	public float score = 0;
	public float life = 100;
	public float lifeTarget = 100;
	public float combo = 0;

	int soundOn;

	public Image bar;
	public GameObject pauseButton, menuPanel, restartButton, soundButton;

	public Sprite soundOnImage, soundOffImage;

	void Awake () {

		Time.timeScale = 1;

		if (PlayerPrefs.GetInt ("HighScore") == null) {

			PlayerPrefs.SetInt ("HighScore", 0);
		}
	}

	void Start () {

		OnUnPause ();

		soundOn = PlayerPrefs.GetInt ("SoundOn");

		if (soundOn == 1) {

			music.Play ();
		}

		/*GameObject.Find ("ScoreLabel").SetActive (false);
		GameObject.Find ("HighScoreLabel").SetActive (false);
		GameObject.Find ("HighScoreText").SetActive (false);*/
	}

	public void OnPause () {

		GameObject.Find ("Main Camera").GetComponent <Audio> ().Button ();

		music.volume = 0.3f;

		pauseButton.SetActive (false);
		menuPanel.SetActive (true);
		Time.timeScale = 0;
		GetComponent <CreateSwervers> ().locked = true;

		GameObject.Find ("HighScoreText").GetComponent <Text> ().text = PlayerPrefs.GetInt ("HighScore").ToString();
	}

	public void OnUnPause () {

		GameObject.Find ("Main Camera").GetComponent <Audio> ().Button ();

		music.volume = 0.6f;

		if (gameover) {

			Application.LoadLevel ("game");
		}

		pauseButton.SetActive (true);
		menuPanel.SetActive (false);

		Time.timeScale = 1;
	}

	public void OnRestart () {

		Application.LoadLevel("menu");
	}

	public void OnMusic () {

		if (soundOn == 1) {

			soundOn = 2;
			PlayerPrefs.SetInt ("SoundOn", soundOn);

			music.Stop ();
		} 

		else {

			GameObject.Find ("Main Camera").GetComponent <Audio> ().Button ();

			soundOn = 1;
			PlayerPrefs.SetInt ("SoundOn", soundOn);

			music.Play ();
		}
	}

	void Update () {


		if (soundOn == 1) {

			soundButton.GetComponent <Image> ().overrideSprite = soundOnImage;
		} 
		else {

			soundButton.GetComponent <Image> ().overrideSprite = soundOffImage;
		}

		GameObject.Find ("ScoreText").GetComponent <Text> ().text = score.ToString();


		lifeTarget -= Mathf.Min ((6 + score/5), 20) * Time.deltaTime;

		if (lifeTarget > 100) {

			lifeTarget = 100;
		}

		/*if (combo == 8) {

			lifeTarget = 100;
			combo = 0;
		} 
		else if (combo > 8) {

			combo = 1;
		}*/
	
		life = Mathf.Lerp (life, lifeTarget, 6 * Time.deltaTime);



		bar.fillAmount = life / 100;

		if (life <= 25) {

			bar.color = Color.red;
		} 

		else {

			bar.color = Color.white;
		}

		if (life <= 0 && !gameover) {

			gameover = true;
			Time.timeScale = 0.5f;

			Invoke ("GameOverStart", 2);

			//StartCoroutine ();
		}
	}

	void GameOverStart () {

		StartCoroutine (GameOver ());
	}

	IEnumerator GameOver () {
		
		OnPause ();


		if (score > PlayerPrefs.GetInt ("HighScore")) {

			PlayerPrefs.SetInt ("HighScore", (int) score);

			GameObject.Find ("HighscoreLabel").GetComponent <Text> ().text = "New best!";
			GameObject.Find ("HighScoreText").GetComponent <Text> ().text = PlayerPrefs.GetInt ("HighScore").ToString();
		}

		yield return null;
	}
}
