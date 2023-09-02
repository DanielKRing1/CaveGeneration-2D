using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour {

	protected Rigidbody2D rb;

	protected float speed;

	// Use this for initialization
	protected void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
	}

	protected void MoveRight(){
		this.rb.velocity = new Vector2 (speed, this.rb.velocity.y);
	}
	protected void MoveLeft(){
		this.rb.velocity = new Vector2 (-1 * speed, this.rb.velocity.y);
	}

}
