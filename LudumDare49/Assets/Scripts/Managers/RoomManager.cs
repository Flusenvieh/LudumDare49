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
	}

    private void Update()
	{
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			NextStage(new string[0]);
		}
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
}
