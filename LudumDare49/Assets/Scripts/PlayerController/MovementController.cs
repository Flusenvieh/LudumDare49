using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private float _Speed = 50;

    private Rigidbody2D _Rigidbody2D;
    private Animator _Animator;

    [SerializeField]
    DialogueRunner _DialogueRunner;
    [SerializeField]
    DialogueUI _DialogueUI;

    private bool _WalkLeft, _WalkRight, _WalkFront, _WalkBack;

	private void Awake()
	{
        _Rigidbody2D = GetComponent<Rigidbody2D>();
        _Animator = GetComponent<Animator>();
        _Animator.SetBool("Left", true);
	}

	void Update()
    {
        float inputX = Input.GetAxis("Horizontal");    
        float inputY = Input.GetAxis("Vertical");

        Vector2 movementVector = _DialogueRunner.IsDialogueRunning ? new Vector2(0, 0) : new Vector2(_Speed * inputX, _Speed * inputY);

        _Rigidbody2D.velocity = movementVector;

        _Animator.SetFloat("Horizontal", inputX);
        _Animator.SetFloat("Vertical", inputY);
        _Animator.SetFloat("Speed", _Rigidbody2D.velocity.magnitude);

        if(inputX>0)
		{
            _Animator.SetBool("Right", true);
            _Animator.SetBool("Left", false);
            _Animator.SetBool("Front", false);
            _Animator.SetBool("Back", false);
		}else if (inputX < 0)
        {
            _Animator.SetBool("Right", false);
            _Animator.SetBool("Left", true);
            _Animator.SetBool("Front", false);
            _Animator.SetBool("Back", false);
        }else if (inputY > 0)
        {
            _Animator.SetBool("Right", false);
            _Animator.SetBool("Left", false);
            _Animator.SetBool("Front", false);
            _Animator.SetBool("Back", true);
        }else if (inputY < 0)
        {
            _Animator.SetBool("Right", false);
            _Animator.SetBool("Left", false);
            _Animator.SetBool("Front", true);
            _Animator.SetBool("Back", false);
        }

        if (_DialogueRunner.IsDialogueRunning && Input.GetKeyDown(KeyCode.Space))
        {
            _DialogueUI.MarkLineComplete();
        }
    }
}
