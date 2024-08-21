using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowCross : MonoBehaviour
{
	private MovingWalls _walls;
	private CubeData cube = CubeData.Instance;
	private Cross _cross = new Cross();
	private string colors;
	public void SolveYellowCross()
	{
		GameObject test = GameObject.Find("CubeBig");
		_walls = test.GetComponent<MovingWalls>();
		YellowCrossSolver();
		CheckCrossRotation(false);
		if (!(cube.FRONT[7] == 'r' && cube.RIGHT[7] == 'b' && cube.BACK[7] == 'o' && cube.LEFT[7] == 'g'))
		{
			YellowTwoEdgesAlgoritm();
		}
		_walls.WriteMoves(4);
	}
	private void YellowCrossSolver()
	{
		string cross = CheckYellowWall();
		switch (cross)
		{
			case "LineVert":
				_walls.TurnDownRight(cube);
				YellowCrossAlgoritm();
				break;
			case "LineHor":
				YellowCrossAlgoritm();
				break;
			case "RightUpCorner":
				_walls.TurnDownRight(cube);
				YellowCrossAlgoritm();
				YellowCrossAlgoritm();
				break;
			case "RightDownCorner":
				YellowCrossAlgoritm();
				YellowCrossAlgoritm();
				break;
			case "LeftDownCorner":
				_walls.TurnDownLeft(cube);
				YellowCrossAlgoritm();
				YellowCrossAlgoritm();
				break;
			case "LeftUpCorner":
				_walls.TurnDownLeft(cube);
				_walls.TurnDownLeft(cube);
				YellowCrossAlgoritm();
				YellowCrossAlgoritm();
				break;
			case "Dot":
				YellowCrossAlgoritm();
				YellowCrossSolver();
				break;
		}
	}
	private string CheckYellowWall()
	{
		if (cube.DOWN[1] == 'y' && cube.DOWN[3] == 'y' && cube.DOWN[5] == 'y' && cube.DOWN[7] == 'y') return "Cross";
		if (cube.DOWN[1] == 'y' && cube.DOWN[7] == 'y') return "LineVert";
		if (cube.DOWN[3] == 'y' && cube.DOWN[5] == 'y') return "LineHor";
		if (cube.DOWN[1] == 'y' && cube.DOWN[5] == 'y') return "RightUpCorner";
		if (cube.DOWN[5] == 'y' && cube.DOWN[7] == 'y') return "RightDownCorner";
		if (cube.DOWN[7] == 'y' && cube.DOWN[3] == 'y') return "LeftDownCorner";
		if (cube.DOWN[3] == 'y' && cube.DOWN[1] == 'y') return "LeftUpCorner";
		//Chyba trzeba dodaæ sprawdzanie czy jest jeden klocek ¿ó³ty, do testowania pozniej
		if (cube.DOWN[1] != 'y' && cube.DOWN[3] != 'y' && cube.DOWN[5] != 'y' && cube.DOWN[7] != 'y') return "Dot";
		return "Error";
	}
	private void YellowCrossAlgoritm()
	{
		_walls.TurnFrontRight(cube);
		_walls.TurnLeftRight(cube);
		_walls.TurnDownRight(cube);
		_walls.TurnLeftLeft(cube);
		_walls.TurnDownLeft(cube);
		_walls.TurnFrontLeft(cube);
	}
	private void CheckCrossRotation(bool reverse)
	{
		string yellowCrossColors = $"{cube.FRONT[7]}{cube.RIGHT[7]}{cube.BACK[7]}{cube.LEFT[7]}";
		if (reverse)
		{
			yellowCrossColors = yellowCrossColors[yellowCrossColors.Length - 1] + yellowCrossColors.Substring(0, yellowCrossColors.Length - 1);
		}
		if (yellowCrossColors.Contains("og"))
		{
			int position = 0;
			int i = 0;
			char color;
			do
			{
				color = _cross.GetAdjacentColor(cube, "DOWN", i);
				if (color == 'o') position = i;
				i++;
			} while (position == 0);
			MoveToCorrectPosition(color, position);
			colors = "og";
		}
		else if (yellowCrossColors.Contains("gr"))
		{
			int position = 0;
			int i = 0;
			char color;
			do
			{
				color = _cross.GetAdjacentColor(cube, "DOWN", i);
				if (color == 'g') position = i;
				i++;
			} while (position == 0);
			MoveToCorrectPosition(color, position);
			colors = "gr";
		}
		else if (yellowCrossColors.Contains("rb"))
		{
			int position = 0;
			int i = 0;
			char color;
			do
			{
				color = _cross.GetAdjacentColor(cube, "DOWN", i);
				if (color == 'r') position = i;
				i++;
			} while (position == 0);
			MoveToCorrectPosition(color, position);
			colors = "rb";
		}
		else if (yellowCrossColors.Contains("bo"))
		{
			int position = 0;
			int i = 0;
			char color;
			do
			{
				color = _cross.GetAdjacentColor(cube, "DOWN", i);
				if (color == 'b') position = i;
				i++;
			} while (position == 0);
			MoveToCorrectPosition(color, position);
			colors = "bo";
		}
		else
		{
			if (reverse)
			{
				ScrambleYellowCross();
				CheckCrossRotation(false);
			}
			if (!reverse) CheckCrossRotation(true);
		}
	}
	private void ScrambleYellowCross()
	{
		_walls.TurnLeftRight(cube);
		_walls.TurnDownRight(cube);
		_walls.TurnLeftLeft(cube);
		_walls.TurnDownRight(cube);
		_walls.TurnLeftRight(cube);
		_walls.TurnDownRight(cube);
		_walls.TurnDownRight(cube);
		_walls.TurnLeftLeft(cube);
		_walls.TurnDownRight(cube);
	}
	private void MoveToCorrectPosition(char color, int position)
	{
		switch (position)
		{
			case 1:
				switch (color)
				{
					case 'b':
						_walls.TurnDownRight(cube);
						break;
					case 'g':
						_walls.TurnDownLeft(cube);
						break;
					case 'o':
						_walls.TurnDownRight(cube);
						_walls.TurnDownRight(cube);
						break;
				}
				break;
			case 3:
				switch (color)
				{
					case 'r':
						_walls.TurnDownRight(cube);
						break;
					case 'b':
						_walls.TurnDownRight(cube);
						_walls.TurnDownRight(cube);
						break;
					case 'o':
						_walls.TurnDownLeft(cube);
						break;
				}
				break;
			case 5:
				switch (color)
				{
					case 'r':
						_walls.TurnDownLeft(cube);
						break;
					case 'g':
						_walls.TurnDownRight(cube);
						_walls.TurnDownRight(cube);
						break;
					case 'o':
						_walls.TurnDownRight(cube);
						break;
				}
				break;
			case 7:
				switch (color)
				{
					case 'r':
						_walls.TurnDownRight(cube);
						_walls.TurnDownRight(cube);
						break;
					case 'b':
						_walls.TurnDownLeft(cube);
						break;
					case 'g':
						_walls.TurnDownRight(cube);
						break;
				}
				break;
		}
	}
	private void YellowTwoEdgesAlgoritm()
	{
		switch (colors)
		{
			case "og":
				_walls.TurnRightRight(cube);
				_walls.TurnDownLeft(cube);
				_walls.TurnRightRight(cube);
				_walls.TurnDownRight(cube);
				_walls.TurnRightRight(cube);
				_walls.TurnDownRight(cube);
				_walls.TurnRightRight(cube);
				_walls.TurnDownLeft(cube);
				_walls.TurnRightLeft(cube);
				_walls.TurnDownLeft(cube);
				_walls.TurnRightRight(cube);
				_walls.TurnRightRight(cube);
				_walls.TurnDownRight(cube);
				break;
			case "gr":
				_walls.TurnBackRight(cube);
				_walls.TurnDownLeft(cube);
				_walls.TurnBackRight(cube);
				_walls.TurnDownRight(cube);
				_walls.TurnBackRight(cube);
				_walls.TurnDownRight(cube);
				_walls.TurnBackRight(cube);
				_walls.TurnDownLeft(cube);
				_walls.TurnBackLeft(cube);
				_walls.TurnDownLeft(cube);
				_walls.TurnBackRight(cube);
				_walls.TurnBackRight(cube);
				_walls.TurnDownRight(cube);
				break;
			case "rb":
				_walls.TurnLeftRight(cube);
				_walls.TurnDownLeft(cube);
				_walls.TurnLeftRight(cube);
				_walls.TurnDownRight(cube);
				_walls.TurnLeftRight(cube);
				_walls.TurnDownRight(cube);
				_walls.TurnLeftRight(cube);
				_walls.TurnDownLeft(cube);
				_walls.TurnLeftLeft(cube);
				_walls.TurnDownLeft(cube);
				_walls.TurnLeftRight(cube);
				_walls.TurnLeftRight(cube);
				_walls.TurnDownRight(cube);
				break;
			case "bo":
				_walls.TurnFrontRight(cube);
				_walls.TurnDownLeft(cube);
				_walls.TurnFrontRight(cube);
				_walls.TurnDownRight(cube);
				_walls.TurnFrontRight(cube);
				_walls.TurnDownRight(cube);
				_walls.TurnFrontRight(cube);
				_walls.TurnDownLeft(cube);
				_walls.TurnFrontLeft(cube);
				_walls.TurnDownLeft(cube);
				_walls.TurnFrontRight(cube);
				_walls.TurnFrontRight(cube);
				_walls.TurnDownRight(cube);
				break;
		}
	}
}

