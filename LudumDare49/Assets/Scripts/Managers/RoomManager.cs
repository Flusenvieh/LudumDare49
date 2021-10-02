using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    private List<Stage> RoomTilesInStages = new List<Stage>();

	private int CurrentStage = 0;

	private void Awake()
	{
		if(RoomTilesInStages.Count>0)
		{
			RoomTilesInStages[CurrentStage].Activate();
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			NextStage();
		}
	}

	private void NextStage()
	{
		if(CurrentStage != RoomTilesInStages.Count-1)
		{
			RoomTilesInStages[CurrentStage].Deactivate();
			CurrentStage++;
			RoomTilesInStages[CurrentStage].Activate();
		}
	}
}
