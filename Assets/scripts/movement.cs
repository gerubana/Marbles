using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

	private Animator ani;
	private AnimatorStateInfo currentBaseState;
	static int idleState = Animator.StringToHash ("Base Layer.idle");
	static int locoState = Animator.StringToHash ("Base Layer.Locomotion");

	private Rigidbody rb;
	private Vector3 velocity;

	private float h;
	private bool isMove;

	private float moveSpeed;
	private GameObject player;
	private body player_body_state;
	//移動範圍
	private float move_range_min;
	private float move_range_max;

	// Use this for initialization
	void Start () {
		//Debug.Log ("movement="+Goble_Player.playerName);
		if(Goble_Player.playerName == null)
			Goble_Player.playerName = this.name;
		//Debug.Log ("movement="+Goble_Player.playerName);

		ani = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		player = GameObject.Find(Goble_Player.playerName);
		player_body_state = player.GetComponent<body> ();
		moveSpeed = player_body_state.moveSpeed/10;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		h = Input.GetAxis ("Horizontal");
		isMove = (h!=0) ? true:false;
		if(this.name == Goble_Player.playerName)
			movecontroller (h, isMove);
	}

	private void movecontroller(float h, bool isMove)
	{
		ani.SetFloat ("horizontal", h);
		ani.SetBool ("move", isMove);
		currentBaseState = ani.GetCurrentAnimatorStateInfo(0);
		rb.useGravity = true;

		velocity = new Vector3 (h, 0f, 0f);
		velocity = transform.TransformDirection (velocity);
		velocity *= moveSpeed;

		transform.localPosition += velocity * Time.fixedDeltaTime;

		if (isMove == true)
			player_body_state.moveAudio ();
		else 
			player_body_state.moveAudio_stop ();
		//Debug.Log (h);
	}

	//最大範圍為2.155
	public void RandomMove(float range)
	{
		move_range_min = -range * 2.155f;
		move_range_max = range * 2.155f;
	}
}
