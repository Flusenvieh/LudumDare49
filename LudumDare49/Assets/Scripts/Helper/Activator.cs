using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    [SerializeField]
    private GameObject _ObjectToActivate;

	private bool inside=false;

	private void Update()
	{
		if(inside && Input.GetKeyDown(KeyCode.E))
		{
			_ObjectToActivate.SetActive(true);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			inside = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			inside = false;
		}
	}
}
