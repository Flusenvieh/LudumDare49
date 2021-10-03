using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private float _Speed = 50;

    private Rigidbody2D _Rigidbody2D;
    private Animator _Animator;

	private void Awake()
	{
        _Rigidbody2D = GetComponent<Rigidbody2D>();
        _Animator = GetComponent<Animator>();
	}

	void Update()
    {
        float inputX = Input.GetAxis("Horizontal");    
        float inputY = Input.GetAxis("Vertical");

        Vector2 movementVector = new Vector2(_Speed * inputX, _Speed * inputY);

        _Rigidbody2D.velocity = movementVector;

        if (Input.GetKeyDown(KeyCode.A))
        {
            _Animator.SetTrigger("StartWalkLeft");
            _Animator.ResetTrigger("StopWalkLeft");
            _Animator.ResetTrigger("StopWalkFront");
            _Animator.ResetTrigger("StopWalkRight");
            _Animator.ResetTrigger("StopWalkBack");
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            _Animator.SetTrigger("StopWalkLeft");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            _Animator.SetTrigger("StartWalkBack");
            _Animator.ResetTrigger("StopWalkLeft");
            _Animator.ResetTrigger("StopWalkFront");
            _Animator.ResetTrigger("StopWalkRight");
            _Animator.ResetTrigger("StopWalkBack");
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            _Animator.SetTrigger("StopWalkBack");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _Animator.SetTrigger("StartWalkRight");
            _Animator.ResetTrigger("StopWalkLeft");
            _Animator.ResetTrigger("StopWalkFront");
            _Animator.ResetTrigger("StopWalkRight");
            _Animator.ResetTrigger("StopWalkBack");
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            _Animator.SetTrigger("StopWalkRight");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _Animator.SetTrigger("StartWalkFront");
            _Animator.ResetTrigger("StopWalkLeft");
            _Animator.ResetTrigger("StopWalkFront");
            _Animator.ResetTrigger("StopWalkRight");
            _Animator.ResetTrigger("StopWalkBack");
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            _Animator.SetTrigger("StopWalkFront");
        }
    }
}
