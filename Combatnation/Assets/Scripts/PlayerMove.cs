using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    public float velocity = 3.0f;
    private CharacterController charController;
    void Start () {
	charController = gameObject.GetComponent<CharacterController>();
    }
    void Update () {
	float h = Input.GetAxis("Horizontal");
	float v = Input.GetAxis("Vertical");
	Vector3 moveDirection =  new Vector3(0, 0, 0);;
	if (charController.isGrounded) {
	    moveDirection = new Vector3(h, 0, v);
	}
	moveDirection.y += Physics.gravity.y;
	charController.Move(velocity * Time.deltaTime * moveDirection);
    }
}