using System.Collections;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
	public Transform targetObject;
	private float CorrectY = 4.42f;
	public void TurnCubeRight()
	{
		TurnCube(-90f);
	}
	public void TurnCubeLeft()
	{
		TurnCube(90f);
	}
	public void TurnCubeUp()
	{
		CorrectY = -5.76f;
		TurnCameraUpDown(-30f);
	}
	public void TurnCubeDown()
	{
		CorrectY = 4.42f;
		TurnCameraUpDown(30f);
	}
	private void TurnCube(float number)
	{
		transform.RotateAround(targetObject.position, Vector3.up, number);
		Vector3 directionToTarget = targetObject.position - transform.position;
		Vector3 newPosition = targetObject.position - directionToTarget.normalized
			* Vector3.Distance(transform.position, targetObject.position);
		newPosition.y = CorrectY;
		transform.position = newPosition;
	}
	private void TurnCameraUpDown(float number)
	{
		transform.position = new Vector3(transform.position.x, CorrectY, transform.position.z);
		transform.rotation = Quaternion.Euler(number, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
	}
}
