using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
	int currentSceneIndex, nxtSceneIndex;

	public static GameMaster Instance;

	void Start() {
		currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		nxtSceneIndex = currentSceneIndex + 1;

		Instance = this;
	}

	void Update() {
		int count = 0;
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
			count += obj.activeSelf ? 1 : 0;	
		if (count == 0 && currentSceneIndex != 0) {
			LoadNext();
		}
	}

	public void LoadNext() {
		AudioManager.Instance.Play("Success");
		SceneManager.LoadScene(nxtSceneIndex);
	}

	public void Reset() {
		AudioManager.Instance.Play("Fail");
		SceneManager.LoadScene(currentSceneIndex);
	}
}
