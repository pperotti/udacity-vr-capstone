using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : AirHockeyNetworkBehaviour
{
	
	public const float movementSpeed = 10f;

	/**
	 * MAX limit that can be used to scroll horizontally.
	 */
	private const float horizontalLimit = 3.5f;


	private float increment = 0f;

	/**
	 * The disk used during the game.
	 */
	private Disk disk;

	/**
	 * Is this player the host or client.
	 */
	bool isHost = false;

	/**
	 * Determine whether the drawing is handled by the Update method or network manager.
	 */
    bool painted = false;

	/**
	 * The current RigidBody asociated with the player.
	 */
	private Rigidbody playerRigidBody;

    void Start()
    {
		Debug.Log ("Player.Start");

		playerRigidBody = GetComponent<Rigidbody> ();
		GameObject diskObject = GameObject.FindGameObjectWithTag("disk");
		disk = diskObject.GetComponent<Disk>();

		Debug.Log ("Player.Started");
    }

    public override void OnStartServer()
    {
		Debug.Log ("Player.OnStartServer");
		isHost = true;

		if (isLocalPlayer) 
		{
			GameLogic.Instance.RegisterLocalPlayer (this);
		}

		increment = GameLogic.Instance.getIncrement ();
		Debug.Log ("Player.increment=" + increment);
    }

	public override void OnStartLocalPlayer()
    {
		Debug.Log ("Player.OnStartLocalPlayer");

		prepareSpawnPoint();

		if (isLocalPlayer)
		{
			GameLogic.Instance.RegisterLocalPlayer (this);
		}

		increment = GameLogic.Instance.getIncrement ();
		Debug.Log ("Player.increment=" + increment);
    }

	void Destroy()
	{
		Debug.Log ("Player.Destroy()");
		GameLogic.Instance.UnRegisterLocalPlayer ();
	}

    void Update()
    {
        if (!painted)
		{
            painted = true;            
		}

		if (isLocalPlayer) 
		{
			OVRInput.Update ();

			GetComponent<MeshRenderer> ().material.color = Color.red;

			float inputX = 0.0f;

			bool isMoveDiskKeyPressed = false;

			if (GameLogic.Instance.getUseController()) {
				Vector2 axis = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
				inputX = axis.x; // + movementSpeed + Time.deltaTime;
				isMoveDiskKeyPressed = OVRInput.Get(OVRInput.Button.One);
			} else {
				inputX = Input.GetAxis ("Horizontal");
				isMoveDiskKeyPressed = Input.GetKeyDown (KeyCode.Space);
			}

			//Handle Input
			if (isHost)
			{
				handleHostInput (inputX);
			} 
			else 
			{
				handleClientInput (inputX);
			}

			if (isHost
				&& isMoveDiskKeyPressed
				&& disk.GetComponent<Rigidbody>().velocity == Vector3.zero) {
				moveDisk ();
			}
		} 
    }

	void OnDestroy() {
		print("DragPlayer destroyed");
	}

	void handleHostInput(float inputX) {
		if (inputX != 0) {

			var posX = playerRigidBody.transform.localPosition.x;
			var offsetX = (inputX>0) ? increment : -increment;

			handleInput (inputX, offsetX, posX);
		}
	}

	bool isLeft(float inputX) {
		return (isHost) ? inputX < 0 : inputX > 0;
	}

	bool isRight(float inputX) {
		return (isHost) ? inputX > 0 : inputX < 0;
	}

	void handleInput(float inputX, float offsetX, float posX) {
		if (isLeft(inputX) && posX - increment < -horizontalLimit) { //Left
			transform.localPosition = new Vector3 (
				-horizontalLimit, 
				transform.localPosition.y,
				transform.localPosition.z);
		} else if (isRight(inputX) && posX + increment > horizontalLimit) { //Right			
			transform.localPosition = new Vector3 (
				horizontalLimit, 
				transform.localPosition.y,
				transform.localPosition.z);
		} else {
			transform.Translate (offsetX, 0, 0);
		}
	}

	public void MoveLeft()
	{
		float offsetX = transform.localPosition.x - increment;
		Debug.Log ("Player.MoveRight -> offsetX=" + offsetX);
		if (offsetX < -horizontalLimit)
		{
			offsetX = -horizontalLimit;
		}
		transform.localPosition = new Vector3 (
			offsetX, 
			transform.localPosition.y,
			transform.localPosition.z);
	}

	public void MoveRight()
	{
		float offsetX = transform.localPosition.x + increment;
		Debug.Log ("Player.MoveRight -> offsetX=" + offsetX);
		if (offsetX > horizontalLimit)
		{
			offsetX = horizontalLimit;
		}
		transform.localPosition = new Vector3 (
			offsetX, 
			transform.localPosition.y,
			transform.localPosition.z);
	}

	void handleClientInput(float inputX) {
		if (inputX != 0) {

			var posX = playerRigidBody.transform.localPosition.x;
			var offsetX = (inputX>0) ? -increment : increment;

			handleInput (inputX, offsetX, posX);
		}
	}

	void prepareSpawnPoint()
    {		
		if (isHost) {
			GetComponent<MeshRenderer> ().material.color = Color.red;
		}

		GameObject localPlayerPosition;
		if (isHost) {
			localPlayerPosition = GameObject.FindGameObjectWithTag ("clientPlayer");
		} else {
			localPlayerPosition = GameObject.FindGameObjectWithTag ("hostPlayer");
		}

		transform.localPosition = new Vector3 (
			localPlayerPosition.transform.localPosition.x, 
			localPlayerPosition.transform.localPosition.y,
			localPlayerPosition.transform.localPosition.z);
    }

    void moveDisk()
    {
		var players = GameObject.FindGameObjectsWithTag("Player");
        // if (players.Length == 2)
        {
			disk.AddImpulse ();
        }
    }
}
