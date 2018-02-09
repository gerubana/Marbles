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
	private Vector3 goal_pos;
	//private bool autoMove;

	// Use this for initialization
	void Start () {
		//Debug.Log ("movement="+Goble_Player.playerName);
		if(Goble_Player.playerName == null)
			Goble_Player.playerName = this.name;
		//Debug.Log ("movement="+Goble_Player.playerName);
		if (Goble_Player.playerName == this.name || Goble_Player.playerName == this.name)
			this.GetComponent<body>().AI = false;

		//WWW player1_data = new WWW("http://gerubana.byethost4.com/play1.json");

		//Debug.Log (this.GetComponent<body>().AI);

		ani = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		player = GameObject.Find(Goble_Player.playerName);
		player_body_state = player.GetComponent<body> ();
		moveSpeed = player_body_state.moveSpeed/10;

		if (this.GetComponent<body> ().AI)
			RandomMove (0.5f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (this.name == Goble_Player.playerName) 
		{
			h = Input.GetAxis ("Horizontal");
			isMove = (h != 0) ? true : false;
			movecontroller (h, isMove);
		}

		if (this.GetComponent<body> ().AI) {
			float tmp_dis = Mathf.Abs(goal_pos.x - this.transform.position.x);

			if (goal_pos.x != this.transform.position.x && tmp_dis >= 0.01) {
				if (goal_pos.x > this.transform.position.x)
					h = 1;
				else
					h = -1;
			} else {
				h = 0;
				RandomMove (0.5f);
			}

			/*Debug.Log ("goal_pos.x="+goal_pos.x);
			Debug.Log ("this.x="+this.transform.position.x);
			Debug.Log ("h="+h);*/

			isMove = (h != 0) ? true : false;
			movecontroller (h, isMove);
		}
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
			this.GetComponent<body>().moveAudio ();
		else 
			this.GetComponent<body>().moveAudio_stop ();
		//Debug.Log (h);
	}


	//最大範圍為2.155
	public void RandomMove(float range)
	{
		move_range_min = -range * 2.155f;
		move_range_max = range * 2.155f;

		goal_pos = new Vector3 (Random.Range (move_range_min, move_range_max), this.transform.position.y, this.transform.position.z);

	}
}
