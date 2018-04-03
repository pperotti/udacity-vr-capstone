using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HUD : MonoBehaviour 
{
	//Input Text to be used on platforms that supports InputFields and Keyboards (MACs/PCs)
	public InputField inputField;

	//Text To be used for VR
	public Text inputData;

	public Button startServerButton;
	public Button startClientButton;
	public Button stopServerButton;
	public Button gameIpButton;

	// Use this for initialization
	void Start () 
	{
		Debug.Log ("AirHockey.HUD.Start");
		if (startServerButton != null) 
		{
			Debug.Log ("AirHockey.HUD.startServerButton != null");
			startServerButton.onClick.AddListener(delegate {ClickStartServer();});
		}

		if (startClientButton != null) 
		{
			Debug.Log ("AirHockey.HUD.startClientButton != null");
			startClientButton.onClick.AddListener(delegate {ClickStartClient();});
		}

		if (stopServerButton != null) 
		{
			Debug.Log ("AirHockey.HUD.stopServerButton != null");
			stopServerButton.onClick.AddListener(delegate {ClickDisconnect();});
		}

		startServerButton.gameObject.SetActive (true);
		startClientButton.gameObject.SetActive (true);
		stopServerButton.gameObject.SetActive (false);
		gameIpButton.gameObject.SetActive (false);
	}

	public void ClickStartServer()
	{
		Debug.Log ("AirHockey.HUD.Start Server networkManager.isNetworkActive=" + GameLogic.Instance.IsNetworkActive() 
			+ " networkManager.isActiveAndEnabled=" + GameLogic.Instance.IsNetworkActiveAndEnabled()		
		);

		//Get the IP data to use for binding
		string data = getIp();
		if (GameLogic.Instance.IsNetworkActive() == false && data != null) 
		{
			GameLogic.Instance.StartServer (data);
			UpdateStartServerUI ();
		}
			
	}

	private string getIp() {
		string data = null; 
		if (inputField != null) 
		{
			data = inputField.text;
		} else if (inputData != null) 
		{
			data = inputData.text;
		}
		return data;
	}

	public void UpdateStartServerUI()
	{
		startServerButton.gameObject.SetActive (false);
		startClientButton.gameObject.SetActive (false);
		stopServerButton.gameObject.SetActive (true);
		gameIpButton.gameObject.SetActive (true);

		addGameIpButton ();
	}

	public void ClickDisconnect()
	{
		Debug.Log ("AirHockey.GL.ClickDisconnect networkManager.isNetworkActive=" + GameLogic.Instance.IsNetworkActive());
		if (GameLogic.Instance.IsNetworkActive()) 
		{
			GameLogic.Instance.OnDisconnect ();
			UpdateDisconnectUI ();
		}
	}

	public void UpdateDisconnectUI()
	{
		Debug.Log ("AirHockey.HUD.UpdateDisconnectUI()");
		startServerButton.gameObject.SetActive (true);
		startClientButton.gameObject.SetActive (true);
		stopServerButton.gameObject.SetActive (false);
		gameIpButton.gameObject.SetActive (false);
	}

	/**
	 * Method invoked when the user clicked on the Start client button
	 */
	public void ClickStartClient()
	{
		Debug.Log ("AirHockey.HUD.Start Client networkManager.isNetworkActive=" + GameLogic.Instance.IsNetworkActive() 
			+ " networkManager.IsNetworkActiveAndEnabled=" + GameLogic.Instance.IsNetworkActiveAndEnabled()
		);
		string data = getIp();
		if (!GameLogic.Instance.IsNetworkActive ()) {
			Debug.Log ("AirHockey.HUD.StartClient starting... " + data);
			GameLogic.Instance.StartClient (data);

			UpdateStartClientUI ();
		} else {
			Debug.Log ("AirHockey.HUD.StartClient cannot start...");
		}
	}

	public void UpdateStartClientUI()
	{
		Debug.Log ("AirHockey.HUD.UpdateStartClientUI()");
		startServerButton.gameObject.SetActive (false);
		startClientButton.gameObject.SetActive (false);
		stopServerButton.gameObject.SetActive (true);
		gameIpButton.gameObject.SetActive (true);

		addGameIpButton();
	}

	void addGameIpButton() {
		string ipUsed = "Game IP: ";
		if (GameLogic.Instance.IsNetworkActive ()) {
			ipUsed += GameLogic.Instance.getGameNetworkAddress ();
		} else {
			ipUsed += "Not available";
		}
		gameIpButton.GetComponentInChildren<Text>().text = ipUsed;
		Debug.Log ("AirHockey.HUD.addGameIpButton=" + GameLogic.Instance.getGameNetworkAddress ());
	}

	void OnServerError(NetworkConnection nc, int errorCode)
	{
		Debug.Log ("AirHockey.HUD.OnServerError!");
		GameLogic.Instance.ShowNetworkErrortDialog();
	}

	void OnClientError(NetworkConnection nc, int errorCode) 
	{
		Debug.Log ("AirHockey.HUD.OnClientError!");
		GameLogic.Instance.ShowNetworkErrortDialog();
	}
}
