using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private float _Speed = 50;

    private Rigidbody2D _Rigidbody2D;

	private void Awake()
	{
        _Rigidbody2D = GetComponent<Rigidbody2D>();
	}

	void Update()
    {
        float inputX = Input.GetAxis("Horizontal");    
        float inputY = Input.GetAxis("Vertical");

        Vector2 movementVector = new Vector2(_Speed * inputX, _Speed * inputY);

        _Rigidbody2D.velocity = movementVector;
    }
}
