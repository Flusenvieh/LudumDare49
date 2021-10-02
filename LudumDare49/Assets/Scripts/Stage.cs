using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stage
{
    public List<GameObject> Rooms = new List<GameObject>();

    public void Activate()
	{
        foreach(var room in Rooms)
		{
			room.SetActive(true);
		}
	}

	public void Deactivate()
	{
		foreach (var room in Rooms)
		{
			room.SetActive(false);
		}
	}
}
