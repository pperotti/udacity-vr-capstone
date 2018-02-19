using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HUD : MonoBehaviour 
{
	public InputField inputField;

	public Button startServerButton;
	public Button startClientButton;
	public Button stopServerButton;

	// Use this for initialization
	void Start () 
	{
		Debug.Log ("HUD.Start");
		if (startServerButton != null) 
		{
			Debug.Log ("HUD.startServerButton != null");
			startServerButton.onClick.AddListener(delegate {ClickStartServer();});
		}

		if (startClientButton != null) 
		{
			Debug.Log ("HUD.startClientButton != null");
			startClientButton.onClick.AddListener(delegate {ClickStartClient();});
		}

		if (stopServerButton != null) 
		{
			Debug.Log ("HUD.stopServerButton != null");
			stopServerButton.onClick.AddListener(delegate {ClickDisconnect();});
		}

		startServerButton.gameObject.SetActive (true);
		startClientButton.gameObject.SetActive (true);
		stopServerButton.gameObject.SetActive (false);
	}

	public void ClickStartServer()
	{
		Debug.Log ("HUD.Start Server networkManager.isNetworkActive=" + GameLogic.Instance.IsNetworkActive() 
			+ " networkManager.isActiveAndEnabled=" + GameLogic.Instance.IsNetworkActiveAndEnabled()		
		);

		if (GameLogic.Instance.IsNetworkActive() == false) 
		{
			//FIXME: Use the actual input field
			//GameLogic.Instance.StartServer (inputField.text);
			GameLogic.Instance.StartServer ("localhost");

			UpdateStartServerUI ();
		}
			
	}

	public void UpdateStartServerUI()
	{
		startServerButton.gameObject.SetActive (false);
		startClientButton.gameObject.SetActive (false);
		stopServerButton.gameObject.SetActive (true);
	}

	public void ClickDisconnect()
	{
		Debug.Log ("ClickDisconnect networkManager.isNetworkActive=" + GameLogic.Instance.IsNetworkActive());

		Disk currentDisk = GameLogic.Instance.getDisk ();
		Debug.Log ("currentDisk->" + currentDisk);
		if (GameLogic.Instance.IsNetworkActive() && currentDisk != null) 
		{
			if (currentDisk.isClient) {
				GameLogic.Instance.StopClient ();
			} else if (currentDisk.isServer) {
				GameLogic.Instance.StopHost ();
			}
			UpdateDisconnectUI ();
		}
	}

	public void UpdateDisconnectUI()
	{
		startServerButton.gameObject.SetActive (true);
		startClientButton.gameObject.SetActive (true);
		stopServerButton.gameObject.SetActive (false);
	}

	/**
	 * Method invoked when the user clicked on the Start client button
	 */
	public void ClickStartClient()
	{
		Debug.Log ("Start Client networkManager.isNetworkActive=" + GameLogic.Instance.IsNetworkActive() 
			+ " networkManager.isNetworkActive=" + GameLogic.Instance.IsNetworkActiveAndEnabled()
			+ " ip=" + inputField.text
		);

		if (GameLogic.Instance.IsNetworkActive() == false) 
		{
			GameLogic.Instance.StartClient (inputField.text);

			UpdateStartClientUI ();
		}
	}

	public void UpdateStartClientUI()
	{
		startServerButton.gameObject.SetActive (false);
		startClientButton.gameObject.SetActive (false);
		stopServerButton.gameObject.SetActive (true);
	}

	void OnServerError(NetworkConnection nc, int errorCode)
	{
		Debug.Log ("OnServerError!");
	}

	void OnClientError(NetworkConnection nc, int errorCode) 
	{
		Debug.Log ("OnClientError!");
	}
}
