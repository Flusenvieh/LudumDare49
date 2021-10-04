using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    private List<Stage> RoomTilesInStages = new List<Stage>();
	[SerializeField] 
	private Yarn.Unity.DialogueRunner dialogueRunner;

	private int CurrentStage = 0;

	private void Awake()
	{
		if(RoomTilesInStages.Count>0)
		{
			RoomTilesInStages[CurrentStage].Activate();
		}

		dialogueRunner.AddCommandHandler("nextStage", NextStage);
		dialogueRunner.AddCommandHandler("destroy", DestroyEntity);
	}

    private void Update()
	{
		//if (Input.GetKeyDown(KeyCode.RightArrow))
		//{
		//	NextStage(new string[0]);
		//}
	}

	//[YarnCommand("nextStage")]
	private void NextStage(string[] parameters)
	{
		if(CurrentStage != RoomTilesInStages.Count-1)
		{
			RoomTilesInStages[CurrentStage].Deactivate();
			CurrentStage++;
			RoomTilesInStages[CurrentStage].Activate();

			Debug.Log("Advancing stage");
		}
	}

	[YarnCommand("destroy")]
	void DestroyEntity(string[] parameters)
	{
		GameObject go = GameObject.Find(parameters[0]);

		if (parameters[1] == "true")
		{
			go.SetActive(false);
		}
		else
		{
			Destroy(go.GetComponent<StartDialogueNode>());
		}
	}
}
