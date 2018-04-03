using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumericKeyboard : MonoBehaviour {

	//Text
	public UnityEngine.UI.Text ipText;

	public void onButtonSelected(string value) {
		Debug.Log ("AirHockey.Numkeyboard.onButtonSelected: 1" + ipText);
		if (ipText != null)
		{			
			ipText.text += value;
		}
	}

	public void onBackPressed() {
		Debug.Log ("AirHockey.NumKeyboard.onBackPressed");
		if (ipText != null)
		{
			if (ipText.text.Length == 1) {
				ipText.text = "";
			} 
			else if (ipText.text.Length > 1) 
			{
				ipText.text = ipText.text.Substring(0, ipText.text.Length - 1);	
			}
		}
	}
}
