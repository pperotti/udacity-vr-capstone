using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameLogic : MonoBehaviour 
{

	public static GameLogic Instance { get; private set; }

	/**
	 * Client & Host Scores
	 */
	public int clientScore;
	public int hostScore;

	/**
	 * Dialog to present results.
	 */
	public GameObject resultDialog;

	/**
	 * HUD.
	 */
	public GameObject hudPanel;
	private HUD hud;

	/**
	 * Score Panel
	 */
	public GameObject scorePanel;

	/**
	 * Current Disk in place
	 */
	private Disk disk;

	/**
	 * Network manager to control all the networking.
	 */
	private NetworkManager networkManager;

	/**
	 * Players involved in the game.
	 */
	private Player localPlayer;

	public int maxScore = 1;

	/**
	 * Offset to use when left or right click are pressed.
	 */
	public float incrementOffset = 0.05f;

	//Add description
	public float speed = 2.0f;

	/**
	 * Determine whether the platform in use is GearVR
	 */
	public bool isGearVR = false;

	public void Awake()
	{
		Debug.Log ("AirHockey.GL.Awake");
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (gameObject);
		} 
		else 
		{
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	public void Start () 
	{
		Debug.Log ("AirHockey.GL.Start incrementOffset=" + incrementOffset);
		resultDialog.SetActive (false);
		networkManager = GetComponent<NetworkManager> ();
		hud = hudPanel.GetComponent <HUD> ();
		Debug.Log ("AirHockey.GL.Start hud=" + hud);
	}

	public void IncrementClientScore()
	{
		++clientScore;
		Debug.Log ("AirHockey.GL.incrementClientScore=" + clientScore);
	}

	public void IncrementHostScore()
	{
		++hostScore;
		Debug.Log ("AirHockey.GL.incrementHostScore=" + hostScore);	
	}

	public void CheckHostScore()
	{
		if (IsGameOver()) 
		{
			if (DidHostWin()) {
				ShowHostWinDialog ();
			} else {
				ShowHostLostDialog ();
			}
		} 
	}

	public void CheckClientScore() 
	{
		if (IsGameOver()) 
		{
			if (DidClientWin()) {
				ShowClientWinDialog ();
			} else {
				ShowClientLostDialog ();
			}
		} 
	}

	private void resetScores() 
	{
		hostScore = 0;
		clientScore = 0;

		scorePanel.transform.Find ("hostScore").GetComponent<UnityEngine.UI.Text> ().text = "Host: 0";
		scorePanel.transform.Find ("clientScore").GetComponent<UnityEngine.UI.Text> ().text = "Client: 0";
	}

	public bool IsGameOver()
	{
		Debug.Log ("AirHockey.GL.IsGameOver=" + (clientScore == maxScore || hostScore == maxScore));
		Debug.Log ("AirHockey.GL.clientScore=" + clientScore + " hostScore=" + hostScore);
		return clientScore == maxScore || hostScore == maxScore;
	}

	public bool DidClientWin()
	{
		return clientScore == maxScore;
	}

	public bool DidHostWin()
	{
		return hostScore == maxScore;
	}

	private void ShowDialog(string message, UnityEngine.Color color, UnityEngine.Events.UnityAction action)
	{
		resultDialog.transform.Find ("ResultLabel").GetComponent<UnityEngine.UI.Text> ().color = color;
		resultDialog.transform.Find ("ResultLabel").GetComponent<UnityEngine.UI.Text> ().text = message;
		resultDialog.transform.Find ("Quit").GetComponent<UnityEngine.UI.Button> ().onClick.AddListener (action);
		resultDialog.SetActive (true);
	}

	private void HideDialog()
	{
		resultDialog.SetActive (false);
	}

	public void ShowClientWinDialog()
	{	
		ShowDialog ("Client Won!", Color.green, delegate {
			resultDialog.SetActive (false);
			hud.UpdateDisconnectUI();
			resetScores();
		});
	}

	public void ShowClientLostDialog()
	{
		ShowDialog ("Client Lost!", Color.red, delegate {
			resultDialog.SetActive (false);
			hud.UpdateDisconnectUI();
			resetScores();
		});
	}

	public void ShowHostWinDialog() 
	{
		ShowDialog ("Host Win!", Color.green, delegate {
			resultDialog.SetActive (false);
			hud.UpdateDisconnectUI();
			resetScores();
		});
	}

	public void ShowHostLostDialog()
	{
		ShowDialog ("Host Lost!", Color.red, delegate {
			resultDialog.SetActive (false);
			hud.UpdateDisconnectUI();
			resetScores();
		});
	}

	public void StartServer(string networkAddress)
	{
		Debug.Log ("AirHockey.GL.StartServer networkAddress=" + networkAddress);
		networkManager.networkAddress = networkAddress;
		networkManager.StartHost ();
	}

	public void StartClient(string networkAddress) 
	{
		Debug.Log ("AirHockey.GL.StartClient networkAddress=" + networkAddress);
		networkManager.networkAddress = networkAddress;
		networkManager.networkPort = 7777;
		networkManager.StartClient ();
	}

	public void StopHost() 
	{
		Debug.Log ("AirHockey.GL.StopHost");
		NetworkManager.singleton.StopHost ();
	}

	public void StopClient()
	{
		Debug.Log ("AirHockey.GL.Disconnect");
		networkManager.StopClient ();
	}

	public bool IsNetworkActive()
	{
		Debug.Log ("AirHockey.GL.IsNetworkActive() -> " + networkManager.isNetworkActive);
		return networkManager.isNetworkActive;
	}

	public bool IsNetworkActiveAndEnabled()
	{
		Debug.Log ("AirHockey.GL.IsNetworkActiveAndEnabled() -> " + networkManager.isActiveAndEnabled);
		return networkManager.isActiveAndEnabled;
	}

	public void setDisk(Disk currentDisk)
	{
		disk = currentDisk;
	}

	public Disk getDisk()
	{
		return disk;
	}

	public void RegisterLocalPlayer(Player player) 
	{
		this.localPlayer = player;
		Debug.Log ("AirHockey.GL.RegisterLocalPlayer");
	}

	public void UnRegisterLocalPlayer()
	{
		this.localPlayer = null;
		Debug.Log ("AirHockey.GL.UnRegisterLocalPlayer");
	}

	public void OnLeftClick()
	{
		Debug.Log ("AirHockey.GL.OnLeftClick");
		if (localPlayer != null) 
		{
			localPlayer.MoveLeft ();
		}
	}

	public void OnRightClick()
	{
		Debug.Log ("AirHockey.GL.OnRightClick");
		if (localPlayer != null) 
		{
			localPlayer.MoveRight ();
		}
	}

	public float getIncrement() 
	{
		return incrementOffset;
	}

	public void OnDisconnect() 
	{
		Debug.Log ("AirHockey.GL.OnDisconnect disk.isServer=" + disk.isServer + " disk.isClient=" + disk.isClient);
		if (disk != null && disk.isServer) {
			GameLogic.Instance.StopHost ();
		} else if (disk != null && disk.isClient) {
			GameLogic.Instance.StopClient ();
		}
	}

	public bool isGearVRUsed() {
		return isGearVR;
	}

	public string getGameNetworkAddress() {
		return networkManager.networkAddress.ToString ();
	}

	public void ShowNetworkErrortDialog()
	{
		ShowDialog ("There was a problem with your network. Please restart the game!", Color.red, delegate {
			hud.UpdateDisconnectUI();
			resetScores();
		});
	}

}
