using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour {

	/*public AudioSource explosion;
	public AudioSource goal;
	public AudioSource bounce;
	public AudioSource button;*/

	int goal;
	int explosion;
	int button;

	void Awake () {

		goal =  AudioCenter.loadSound ("goal");
		explosion =  AudioCenter.loadSound ("explosion");
		button =  AudioCenter.loadSound ("button");
	}

	public void Explosion () {

		if (PlayerPrefs.GetInt ("SoundOn") == 1) {

			AudioCenter.playSound (explosion);
		}

		//GameObject.Find ("Creator").GetComponent<GameController> ().combo = 0;

	}

	public void Goal () {

		//goal.pitch = 0.5f * Mathf.Pow (1.05946f, Semitones());

		if (PlayerPrefs.GetInt ("SoundOn") == 1) {

			AudioCenter.playSound (goal);
		}

	}

	public void Bounce () {

		//goal.pitch = 0.5f * Mathf.Pow (1.05946f, Semitones());

		if (PlayerPrefs.GetInt ("SoundOn") == 1) {

			//bounce.Play ();
		}
	}

	public void Button () {

		//goal.pitch = 0.5f * Mathf.Pow (1.05946f, Semitones());

		if (PlayerPrefs.GetInt ("SoundOn") == 1) {

			AudioCenter.playSound (button);
		}

	}
		

	/*int Semitones () {

		float combo = GameObject.Find ("Creator").GetComponent<GameController> ().combo;

		if (combo == 0) {
			return 0;
		} else if (combo == 1) {
			return 2;
		} else if (combo == 2) {
			return 4;
		} else if (combo == 3) {
			return 5;
		} else if (combo == 4) {
			return 7;
		} else if (combo == 5) {
			return 9;
		} else if (combo == 6) {
			return 11;
		} else if (combo == 7) {
			return 12;
		}

		GameObject.Find ("Creator").GetComponent<GameController> ().combo = 0;
		return 0;



	}*/
}
