using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAnim : MonoBehaviour {
    public float velocity = 3.0f;
    private CharacterController charController;
    private Animator animator;
    void Start () {
        charController = gameObject.GetComponent<CharacterController>();
	animator = gameObject.GetComponent<Animator>();
    }
    void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 moveDirection =  new Vector3(0, 0, 0);;
        if (charController.isGrounded) {
            moveDirection = new Vector3(h, 0, v);
        }
	float MovingSpeed = velocity * moveDirection.magnitude;
	if (MovingSpeed > 0.01f) {
	    animator.SetFloat("MovingSpeed", MovingSpeed);
	    transform.LookAt(transform.position + moveDirection);
	} else {
	    animator.SetFloat("MovingSpeed", 0.0f);
	}
        moveDirection.y += Physics.gravity.y;
        charController.Move(velocity * Time.deltaTime * moveDirection);
    }
}
