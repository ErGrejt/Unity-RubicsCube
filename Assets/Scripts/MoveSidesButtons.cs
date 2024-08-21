using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveSidesButtons : MonoBehaviour
{
    private MovingWalls _walls;
    private RotatingSides _sides;
    private CubeData cube = CubeData.Instance;
	public TMP_Text text;

	public float buttonCooldown = 0.08f;
	private bool buttonsLocked = false;
	//Blocking button
	private IEnumerator LockButtons()
	{
		buttonsLocked = true;
		yield return new WaitForSeconds(buttonCooldown);
		buttonsLocked = false;
	}
	public void UpRight()
    {
		if (buttonsLocked) return;
		text.text = "Up w prawo";
        Scripts();
		_walls.RotateUpClockwise(cube);
        _sides.MoveUpRight();
		StartCoroutine(LockButtons());
	}
	public void UpLeft()
	{
		if (buttonsLocked) return;
		text.text = "Up w lewo";
		Scripts();
		_walls.RotateUpCounterClockwise(cube);
		_sides.MoveUpLeft();
		StartCoroutine(LockButtons());
	}
	public void DownRight()
	{
		if (buttonsLocked) return;
		text.text = "Down w prawo";
		Scripts();
		_walls.RotateDownClockwise(cube);
		_sides.MoveDownRight();
		StartCoroutine(LockButtons());
	}
	public void DownLeft()
	{
		if (buttonsLocked) return;
		text.text = "Down w lewo";
		Scripts();
		_walls.RotateDownCounterClockwise(cube);
		_sides.MoveDownLeft();
		StartCoroutine(LockButtons());
	}
	public void FrontRight()
	{
		if (buttonsLocked) return;
		text.text = "Front w prawo";
		Scripts();
		_walls.RotateFrontClockwise(cube);
		_sides.MoveFrontRight();
		StartCoroutine(LockButtons());
	}
	public void FrontLeft()
	{
		if (buttonsLocked) return;
		text.text = "Front w lewo";
		Scripts();
		_walls.RotateFrontCounterClockwise(cube);
		_sides.MoveFrontLeft();
		StartCoroutine(LockButtons());
	}
	public void BackRight()
	{
		if (buttonsLocked) return;
		text.text = "Back w prawo";
		Scripts();
		_walls.RotateBackClockwise(cube);
		_sides.MoveBackRight();
		StartCoroutine(LockButtons());
	}
	public void BackLeft()
	{
		if (buttonsLocked) return;
		text.text = "Back w lewo";
		Scripts();
		_walls.RotateBackCounterClockwise(cube);
		_sides.MoveBackLeft();
		StartCoroutine(LockButtons());
	}
	public void RightRight()
	{
		if (buttonsLocked) return;
		text.text = "Right w prawo";
		Scripts();
		_walls.RotateRightClockwise(cube);
		_sides.MoveRightRight();
		StartCoroutine(LockButtons());
	}
	public void RightLeft()
	{
		if (buttonsLocked) return;
		text.text = "Right w lewo";
		Scripts();
		_walls.RotateRightCounterClockwise(cube);
		_sides.MoveRightLeft();
		StartCoroutine(LockButtons());
	}
	public void LeftRight()
	{
		if (buttonsLocked) return;
		text.text = "Left w prawo";
		Scripts();
		_walls.RotateLeftClockwise(cube);
		_sides.MoveLeftRight();
		StartCoroutine(LockButtons());
	}
	public void LeftLeft()
	{
		if (buttonsLocked) return;
		text.text = "Lewo w lewo";
		Scripts();
		_walls.RotateLeftCounterClockwise(cube);
		_sides.MoveLeftLeft();
		StartCoroutine(LockButtons());
	}
	private void Scripts()
    {
		GameObject test = GameObject.Find("CubeBig");
		_walls = test.GetComponent<MovingWalls>();
		GameObject test2 = GameObject.Find("CubeBig");
		_sides = test2.GetComponent<RotatingSides>();
	}
}
