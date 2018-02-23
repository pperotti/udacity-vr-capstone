using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBehavior : MonoBehaviour {

	//Method to change to a different scene.
	public void ChangeToScene(string sceneName) {
		if (sceneName != null && sceneName.Length > 0) {
			SceneManager.LoadScene (sceneName);
		}
	}
}
