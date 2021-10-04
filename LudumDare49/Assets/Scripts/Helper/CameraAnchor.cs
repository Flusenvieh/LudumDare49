using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnchor : MonoBehaviour
{
    [SerializeField]
    private float _ZoomFactor;

	private Camera _Camera;

	private void Awake()
	{
		_Camera = Camera.main;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			_Camera.transform.position = transform.position;
			_Camera.orthographicSize = 10 / _ZoomFactor;
		}
	}
}
