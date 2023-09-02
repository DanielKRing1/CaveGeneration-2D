using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement {

	private float jumpForce;
	public bool canJump;

	// Use this for initialization
	void Start () {
		base.Start ();

		this.speed = 3f;
		this.jumpForce = 3f;
	}
	
	void FixedUpdate () {
		MoveX ();
		Jump ();
	}

	void MoveX(){
		if (Input.GetKey (KeyCode.A)) {
			MoveLeft ();
		} else if (Input.GetKey (KeyCode.D)) {
			MoveRight ();
		}
	}

	void Jump(){
		CheckCanJump ();
		if(Input.GetKey(KeyCode.W) && canJump){
			rb.AddForce (new Vector2 (0, jumpForce), ForceMode2D.Impulse);
		}
	}
	void CheckCanJump(){
		if(Physics2D.IsTouchingLayers(this.GetComponent<Collider2D>(), LayerMask.GetMask("Ground"))){
			canJump = true;
		}else{
			canJump = false;
		}
	}
}
