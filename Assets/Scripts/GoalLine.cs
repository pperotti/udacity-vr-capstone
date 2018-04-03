using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalLine : MonoBehaviour {

	AudioSource sound;

	void Start()
	{
		sound = GetComponent<AudioSource> ();
	}

	void OnCollisionEnter(Collision other)
	{
		Debug.Log ("AirHockey.OnCollisionEnter");
		if("disk".Equals (other.gameObject.tag))
		{
			sound.Play ();
		}
	}
}
