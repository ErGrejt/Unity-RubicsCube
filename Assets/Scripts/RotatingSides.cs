using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSides : MonoBehaviour
{
    private NeighborChecker neighborChecker = new NeighborChecker();
    public GameObject frontCenter;
    public GameObject rightCenter;
	public GameObject upCenter;
	public GameObject downCenter;
	public GameObject leftCenter;
	public GameObject backCenter;
	//Obrót
	private Quaternion targetRotation;
	public bool isRotating = false;
	public float rotationSpeed = 420f;
	private float rotationProgress = 0f;
	//Bools
	private bool frontMoving;
	private bool rightMoving;
	private bool leftMoving;
	private bool backMoving;
	private bool upMoving;
	private bool downMoving;
	//Bools moving right-left
	private bool frontMovingRight;
	private bool frontMovingLeft;
	private bool backMovingLeft;
	private bool backMovingRight;
	private bool rightMovingRight;
	private bool rightMovingLeft;
	private bool leftMovingRight;
	private bool leftMovingLeft;
	private bool upMovingLeft;
	private bool upMovingRight;
	private bool downMovingRight;
	private bool downMovingLeft;
	void Update()
	{
		if (isRotating)
		{
			float step = rotationSpeed * Time.deltaTime;
			//Front
			if(frontMovingRight) frontCenter.transform.Rotate(step, 0, 0);
			if(frontMovingLeft) frontCenter.transform.Rotate(-step, 0, 0);
			//Right
			if(rightMovingRight) rightCenter.transform.Rotate(0, 0, step);
			if(rightMovingLeft) rightCenter.transform.Rotate(0, 0, -step);
			//Back
			if(backMovingRight) backCenter.transform.Rotate(-step, 0, 0);
			if(backMovingLeft) backCenter.transform.Rotate(step, 0, 0);
			//Left
			if (leftMovingRight) leftCenter.transform.Rotate(0, 0, -step);
			if (leftMovingLeft) leftCenter.transform.Rotate(0, 0, step);
			//Up
			if (upMovingRight) upCenter.transform.Rotate(0, step, 0);
			if (upMovingLeft) upCenter.transform.Rotate(0, -step, 0);
			//Down
			if (downMovingRight) downCenter.transform.Rotate(0, -step, 0);
			if (downMovingLeft) downCenter.transform.Rotate(0, step, 0);

			rotationProgress += step;

			if (rotationProgress >= 90f)
			{
				if(frontMoving) frontCenter.transform.rotation = targetRotation;
				if(rightMoving) rightCenter.transform.rotation = targetRotation;
				if(backMoving) backCenter.transform.rotation = targetRotation;
				if(leftMoving) leftCenter.transform.rotation = targetRotation;
				if(upMoving) upCenter.transform.rotation = targetRotation;
				if(downMoving) downCenter.transform.rotation = targetRotation;
				//Ruch
				isRotating = false;
				//Która œciana
				frontMoving = false;
				rightMoving = false;
				upMoving = false;
				downMoving = false;
				backMoving = false;
				leftMoving = false;
				//Prawo czy lewo
				frontMovingRight = false;
				frontMovingLeft = false;
				
				rightMovingLeft = false;
				rightMovingRight = false;

				leftMovingLeft = false;
				leftMovingRight = false;

				backMovingLeft = false;
				backMovingRight = false;

				upMovingLeft = false;
				upMovingRight = false;

				downMovingLeft = false;
				downMovingRight = false;
				rotationProgress = 0f;
			}
		}
	}
	//Front right
	public void MoveFrontRight()
    {
		if (isRotating) return;
		if(neighborChecker == null)
		{
			GameObject find = GameObject.Find("CubeBig");
			neighborChecker = find.GetComponent<NeighborChecker>();
		}
		List<GameObject> neighbors = neighborChecker.CheckNeighbors("frontCenter", "FRONT");
		frontCenter = GameObject.Find("FrontCenter");
		foreach (var neighbor in neighbors)
		{
			neighbor.transform.parent = frontCenter.transform;
		}
		targetRotation = frontCenter.transform.rotation * Quaternion.Euler(90, 0, 0);
		frontMoving = true;
		frontMovingRight = true;
		isRotating = true;
		StartCoroutine(ResetParentAfterRotation(neighbors));
	}
	//Front left
	public void MoveFrontLeft()
	{
		if (isRotating) return;
		List<GameObject> neighbors = neighborChecker.CheckNeighbors("frontCenter", "FRONT");
		frontCenter = GameObject.Find("FrontCenter");
		foreach (var neighbor in neighbors)
		{
			neighbor.transform.parent = frontCenter.transform;
		}
		targetRotation = frontCenter.transform.rotation * Quaternion.Euler(-90, 0, 0);
		frontMoving = true;
		frontMovingLeft = true;
		isRotating = true;
		StartCoroutine(ResetParentAfterRotation(neighbors));
	}
	//Back right
	public void MoveBackRight()
	{
		if (isRotating) return;
		List<GameObject> neighbors = neighborChecker.CheckNeighbors("backCenter", "BACK");
		backCenter = GameObject.Find("BackCenter");
		foreach (var neighbor in neighbors)
		{
			neighbor.transform.parent = backCenter.transform;
		}
		targetRotation = backCenter.transform.rotation * Quaternion.Euler(-90, 0, 0);
		backMoving = true;
		backMovingRight = true;
		isRotating = true;
		StartCoroutine(ResetParentAfterRotation(neighbors));
	}
	//Back left
	public void MoveBackLeft()
	{
		if (isRotating) return;
		List<GameObject> neighbors = neighborChecker.CheckNeighbors("backCenter", "BACK");
		backCenter = GameObject.Find("BackCenter");
		foreach (var neighbor in neighbors)
		{
			neighbor.transform.parent = backCenter.transform;
		}
		targetRotation = backCenter.transform.rotation * Quaternion.Euler(90, 0, 0);
		backMoving = true;
		backMovingLeft = true;
		isRotating = true;
		StartCoroutine(ResetParentAfterRotation(neighbors));
	}
	//Right right
	public void MoveRightRight()
    {
		if (isRotating) return; 
		List<GameObject> neighbors = neighborChecker.CheckNeighbors("rightCenter", "RIGHT");
		rightCenter = GameObject.Find("RightCenter");
		foreach (var neighbor in neighbors)
		{
			neighbor.transform.parent = rightCenter.transform;
		}
		targetRotation = rightCenter.transform.rotation * Quaternion.Euler(0, 0, 90);
		rightMoving = true;
		rightMovingRight = true;
		isRotating = true;
		StartCoroutine(ResetParentAfterRotation(neighbors));
	}
	//Right left
	public void MoveRightLeft()
	{
		if (isRotating) return;
		List<GameObject> neighbors = neighborChecker.CheckNeighbors("rightCenter", "RIGHT");
		rightCenter = GameObject.Find("RightCenter");
		foreach (var neighbor in neighbors)
		{
			neighbor.transform.parent = rightCenter.transform;
		}
		targetRotation = rightCenter.transform.rotation * Quaternion.Euler(0, 0, -90);
		rightMoving = true;
		rightMovingLeft = true;
		isRotating = true;
		StartCoroutine(ResetParentAfterRotation(neighbors));
	}
	//Left Right
	public void MoveLeftRight()
	{
		if (isRotating) return;
		List<GameObject> neighbors = neighborChecker.CheckNeighbors("leftCenter", "LEFT");
		leftCenter = GameObject.Find("GreenCenter");
		foreach (var neighbor in neighbors)
		{
			neighbor.transform.parent = leftCenter.transform;
		}
		targetRotation = leftCenter.transform.rotation * Quaternion.Euler(0, 0, -90);
		leftMoving = true;
		leftMovingRight = true;
		isRotating = true;
		StartCoroutine(ResetParentAfterRotation(neighbors));
	}
	//Left Left
	public void MoveLeftLeft()
	{
		if (isRotating) return;
		List<GameObject> neighbors = neighborChecker.CheckNeighbors("leftCenter", "LEFT");
		leftCenter = GameObject.Find("GreenCenter");
		foreach (var neighbor in neighbors)
		{
			neighbor.transform.parent = leftCenter.transform;
		}
		targetRotation = leftCenter.transform.rotation * Quaternion.Euler(0, 0, 90);
		leftMoving = true;
		leftMovingLeft = true;
		isRotating = true;
		StartCoroutine(ResetParentAfterRotation(neighbors));
	}
	//Up Right
	public void MoveUpRight()
	{
		if (isRotating) return;
		List<GameObject> neighbors = neighborChecker.CheckNeighbors("upCenter", "UP");
		upCenter = GameObject.Find("UpCenter");
		foreach (var neighbor in neighbors)
		{
			neighbor.transform.parent = upCenter.transform;
		}
		targetRotation = upCenter.transform.rotation * Quaternion.Euler(0, 90, 0);
		upMoving = true;
		upMovingRight = true;
		isRotating = true;
		StartCoroutine(ResetParentAfterRotation(neighbors));
	}
	//Up Left
	public void MoveUpLeft()
	{
		if (isRotating) return;
		List<GameObject> neighbors = neighborChecker.CheckNeighbors("upCenter", "UP");
		upCenter = GameObject.Find("UpCenter");
		foreach (var neighbor in neighbors)
		{
			neighbor.transform.parent = upCenter.transform;
		}
		targetRotation = upCenter.transform.rotation * Quaternion.Euler(0, -90, 0);
		upMoving = true;
		upMovingLeft = true;
		isRotating = true;
		StartCoroutine(ResetParentAfterRotation(neighbors));
	}
	//Down Right
	public void MoveDownRight()
	{
		if (isRotating) return;
		List<GameObject> neighbors = neighborChecker.CheckNeighbors("downCenter", "DOWN");
		downCenter = GameObject.Find("DownCenter");
		foreach (var neighbor in neighbors)
		{
			neighbor.transform.parent = downCenter.transform;
		}
		targetRotation = downCenter.transform.rotation * Quaternion.Euler(0, -90, 0);
		downMoving = true;
		downMovingRight = true;
		isRotating = true;
		StartCoroutine(ResetParentAfterRotation(neighbors));
	}
	//Down Left
	public void MoveDownLeft()
	{
		if (isRotating) return;
		List<GameObject> neighbors = neighborChecker.CheckNeighbors("downCenter", "DOWN");
		downCenter = GameObject.Find("DownCenter");
		foreach (var neighbor in neighbors)
		{
			neighbor.transform.parent = downCenter.transform;
		}
		targetRotation = downCenter.transform.rotation * Quaternion.Euler(0, 90, 0);
		downMoving = true;
		downMovingLeft = true;
		isRotating = true;
		StartCoroutine(ResetParentAfterRotation(neighbors));
	}
	private IEnumerator ResetParentAfterRotation(List<GameObject> neighbors)
	{
		while (isRotating)
		{
			yield return null;
		}
		foreach (var neighbor in neighbors)
		{
			neighbor.transform.parent = GameObject.Find("CubeBig").transform;
		}
	}
}
