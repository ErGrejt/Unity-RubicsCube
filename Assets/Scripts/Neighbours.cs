using System.Collections.Generic;
using UnityEngine;

public class NeighborChecker : MonoBehaviour
{
	//public GameObject centralObject;
	public float gridSpacing = 1.0f;  
	public List<GameObject> neighbors = new List<GameObject>();
	//Bools
	[Space(20)]
	public bool RightWall = false;
	public bool FrontWall = false;
	public bool UpWall = false;

	public List<GameObject> CheckNeighbors(string centralObjectString, string wall)
	{
		GameObject centralObject = null;
		switch(wall)
		{
			case "UP":
				UpWall = true; break;
			case "DOWN":
				UpWall= true; break;
			case "LEFT":
				RightWall= true; break;
			case "RIGHT":
				RightWall = true; break;
			case "FRONT":
				FrontWall = true; break;
			case "BACK":
				FrontWall = true; break;
		}
		switch(centralObjectString)
		{
			case "frontCenter":
				centralObject = GameObject.Find("FrontCenter");
				break;
			case "upCenter":
				centralObject = GameObject.Find("UpCenter");
				break;
			case "backCenter":
				centralObject = GameObject.Find("BackCenter");
				break;
			case "downCenter":
				centralObject = GameObject.Find("DownCenter");
				break;
			case "leftCenter":
				centralObject = GameObject.Find("GreenCenter");
				break;
			case "rightCenter":
				centralObject = GameObject.Find("RightCenter");
				break;
		}

		Vector3 centralPosition = centralObject.transform.position;
		List<Vector3> neighborOffsets = new List<Vector3>();
		if(RightWall)
		{
			neighborOffsets.Add(new Vector3(-1, -1, 0));
			neighborOffsets.Add(new Vector3(-1, 1, 0));
			neighborOffsets.Add(new Vector3(1, -1, 0));
			neighborOffsets.Add(new Vector3(1, 1, 0));
			neighborOffsets.Add(new Vector3(0, -1, 0));
			neighborOffsets.Add(new Vector3(0, 1, 0));
			neighborOffsets.Add(new Vector3(-1, 0, 0));
			neighborOffsets.Add(new Vector3(1, 0, 0));
		}
		if(FrontWall)
		{
			neighborOffsets.Add(new Vector3(0, 0, -1));
			neighborOffsets.Add(new Vector3(0, 0, 1));
			neighborOffsets.Add(new Vector3(0, -1, 1));
			neighborOffsets.Add(new Vector3(0, 1, -1));
			neighborOffsets.Add(new Vector3(0, 1, 1));
			neighborOffsets.Add(new Vector3(0, 0, -1));
			neighborOffsets.Add(new Vector3(0, 0, 1));
			neighborOffsets.Add(new Vector3(0, -1, 0));
			neighborOffsets.Add(new Vector3(0, 1, 0));
			neighborOffsets.Add(new Vector3(0, -1, -1));
		}
		if (UpWall)
		{
			neighborOffsets.Add(new Vector3(0, 0, -1));
			neighborOffsets.Add(new Vector3(0, 0, 1));
			neighborOffsets.Add(new Vector3(-1, 0, -1));
			neighborOffsets.Add(new Vector3(-1, 0, 1));
			neighborOffsets.Add(new Vector3(1, 0, -1));
			neighborOffsets.Add(new Vector3(1, 0, 1));
			neighborOffsets.Add(new Vector3(0, 0, -1));
			neighborOffsets.Add(new Vector3(0, 0, 1));
			neighborOffsets.Add(new Vector3(-1, 0, 0));
			neighborOffsets.Add(new Vector3(1, 0, 0));
		}
		neighbors.Clear(); 

		foreach (var offset in neighborOffsets)
		{
			Vector3 worldOffset = centralObject.transform.TransformDirection(offset * gridSpacing);
			Vector3 neighborPosition = centralPosition + worldOffset;
			Collider[] hitColliders = Physics.OverlapSphere(neighborPosition, 0.1f);
			foreach (var hitCollider in hitColliders)
			{
				if (hitCollider.gameObject != centralObject && !neighbors.Contains(hitCollider.gameObject))
				{
					neighbors.Add(hitCollider.gameObject);
				}
			}
		}
		FrontWall = false;
		UpWall = false;
		RightWall = false;
		return neighbors;
	}
}
