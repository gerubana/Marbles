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

	public float moveSpeed = 1.0f;

	// Use this for initialization
	void Start () {
		ani = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		h = Input.GetAxis ("Horizontal");
		isMove = (h!=0) ? true:false;
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

		Debug.Log (h);
	}
}
