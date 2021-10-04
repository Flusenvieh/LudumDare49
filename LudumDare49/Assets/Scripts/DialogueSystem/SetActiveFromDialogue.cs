using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class SetActiveFromDialogue : MonoBehaviour
{
	[SerializeField]
	private List<GameObject> target;
	[SerializeField]
	DialogueRunner dialogueRunner;

	// Start is called before the first frame update
	void Start()
    {
		dialogueRunner.AddCommandHandler("enable", EnableEntity);
    }

	//[YarnCommand("enable")]
	void EnableEntity(string[] parameters)
	{
		target[int.Parse(parameters[0])].SetActive(true);
	}
}
