using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class YellowCorners : MonoBehaviour
{
	private MovingWalls _walls;
	private CubeData cube = CubeData.Instance;
	private WhiteCorners _whitecorners = new WhiteCorners();
	private Cross _cross = new Cross();
	private string connectorthis;
	//Connectors
	private int upConnector;
	private int downConnector;
	private int downCorrectCorner;
	public void SolveCornersLastStep(string connector)
	{
		GameObject test = GameObject.Find("CubeBig");
		_walls = test.GetComponent<MovingWalls>();
		connectorthis = connector;
		ConnectorsSet(connectorthis);
		SolveCorners();
	}
	private void SolveCorners()
	{
		bool corners = CheckingSolvedCorners(connectorthis);
		bool weirdSituation1 = CheckingYellowDoneWhiteNope();
		bool weirdSituation2 = CheckingWeirdSitation();
		bool solved = CheckingSolvedCube();
		char color;
		string colors;
		while (!corners && !weirdSituation1 && !weirdSituation2 && !solved)
		{
			switch (upConnector)
			{
				case 0:
					color = cube.UP[0];
					colors = _whitecorners.CheckCornersColors("UP", 0);
					colors = colors + color;
					if (colors.Contains('y')) colors = colors.Replace("y", "");
					if (colors.Contains('w'))
					{
						int uncorrectCorner = SearchUncorrectCorner();
						TurnDownToConnector(uncorrectCorner);
						LblAlgoritm(connectorthis);
						CorrectinYellowCross();
						break;
					}
					colors = _whitecorners.TransformColors(colors);
					LblLogic(colors);
					break;
				case 2:
					color = cube.UP[2];
					colors = _whitecorners.CheckCornersColors("UP", 2);
					colors = colors + color;
					if (colors.Contains('y')) colors = colors.Replace("y", "");
					if (colors.Contains('w'))
					{
						int uncorrectCorner = SearchUncorrectCorner();
						TurnDownToConnector(uncorrectCorner);
						LblAlgoritm(connectorthis);
						CorrectinYellowCross();
						break;
					}
					colors = _whitecorners.TransformColors(colors);
					LblLogic(colors);
					break;
				case 6:
					color = cube.UP[6];
					colors = _whitecorners.CheckCornersColors("UP", 6);
					colors = colors + color;
					if (colors.Contains('y')) colors = colors.Replace("y", "");
					if (colors.Contains('w'))
					{
						int uncorrectCorner = SearchUncorrectCorner();
						TurnDownToConnector(uncorrectCorner);
						LblAlgoritm(connectorthis);
						CorrectinYellowCross();
						break;
					}
					colors = _whitecorners.TransformColors(colors);
					LblLogic(colors);
					break;

				case 8:
					color = cube.UP[8];
					colors = _whitecorners.CheckCornersColors("UP", 8);
					colors = colors + color;
					if (colors.Contains('y')) colors = colors.Replace("y", "");
					if (colors.Contains('w'))
					{
						int uncorrectCorner = SearchUncorrectCorner();
						TurnDownToConnector(uncorrectCorner);
						LblAlgoritm(connectorthis);
						CorrectinYellowCross();
						break;
					}
					colors = _whitecorners.TransformColors(colors);
					LblLogic(colors);
					break;
			}
			corners = CheckingSolvedCorners(connectorthis);
			weirdSituation1 = CheckingYellowDoneWhiteNope();
			weirdSituation2 = CheckingWeirdSitation();
			solved = CheckingSolvedCube();
		}
		//Sytuacja gdzie wszystkie zolte sa ulozone ale kostka dalej jest broken
		if (weirdSituation1)
		{
			bool whiteWall = true;
			do
			{
				LblAlgoritm(connectorthis);
				whiteWall = true;
				for (int i = 0; i < 9; i++)
				{
					if (i != upConnector)
					{
						if (cube.UP[i] != 'w')
						{
							whiteWall = false;
							break;
						}
						whiteWall = true;
					}
				}
			} while (!whiteWall);
		}
		weirdSituation2 = CheckingWeirdSitation();
		//Sytuacja gdzie dwa naro¿niki s¹ z³e
		if (weirdSituation2)
		{
			switch (connectorthis)
			{
				case "frontRightEdge":
					do
					{
						CurvedLblAlgoritm(connectorthis);
					} while (!(cube.FRONT[8] == 'r' && cube.RIGHT[6] == 'b' && cube.DOWN[2] == 'y'));
					_walls.TurnFrontRight(cube);
					do
					{
						CurvedLblAlgoritm(connectorthis);
					} while (!(cube.FRONT[8] == 'r' && cube.RIGHT[6] == 'w' && cube.DOWN[2] == 'b'));
					_walls.TurnFrontLeft(cube);
					break;
				case "frontLeftEdge":
					do
					{
						CurvedLblAlgoritm(connectorthis);
					} while (!(cube.FRONT[6] == 'r' && cube.LEFT[8] == 'g' && cube.DOWN[0] == 'y'));
					_walls.TurnLeftRight(cube);
					do
					{
						CurvedLblAlgoritm(connectorthis);
					} while (!(cube.FRONT[6] == 'w' && cube.LEFT[8] == 'g' && cube.DOWN[0] == 'r'));
					_walls.TurnLeftLeft(cube);
					break;
				case "backRightEdge":
					do
					{
						CurvedLblAlgoritm(connectorthis);
					} while (!(cube.LEFT[6] == 'g' && cube.BACK[8] == 'o' && cube.DOWN[6] == 'y'));
					_walls.TurnBackRight(cube);
					do
					{
						CurvedLblAlgoritm(connectorthis);
					} while (!(cube.LEFT[6] == 'w' && cube.BACK[8] == 'o' && cube.DOWN[6] == 'g'));
					_walls.TurnBackLeft(cube);
					break;
				case "backLeftEdge":
					do
					{
						CurvedLblAlgoritm(connectorthis);
					} while (!(cube.BACK[6] == 'o' && cube.RIGHT[8] == 'b' && cube.DOWN[8] == 'y'));
					_walls.TurnRightRight(cube);
					do
					{
						CurvedLblAlgoritm(connectorthis);
					} while (!(cube.BACK[6] == 'w' && cube.RIGHT[6] == 'b' && cube.DOWN[8] == 'o'));
					_walls.TurnRightLeft(cube);
					break;
			}
		}
		solved = CheckingSolvedCube();
		if (solved) _walls.WriteMoves(5);
	}
	private int SearchUncorrectCorner()
	{
		bool leftup = (cube.DOWN[0] == 'y' && cube.FRONT[6] == 'r' && cube.LEFT[8] == 'g');
		bool rightup = (cube.DOWN[2] == 'y' && cube.FRONT[8] == 'r' && cube.RIGHT[6] == 'b');
		bool leftdown = (cube.DOWN[6] == 'y' && cube.LEFT[6] == 'g' && cube.BACK[8] == 'o');
		bool rightdown = (cube.DOWN[8] == 'y' && cube.RIGHT[8] == 'b' && cube.BACK[6] == 'o');

		if (!leftup) return 1;
		if (!rightup) return 2;
		if (!leftdown) return 3;
		if (!rightdown) return 4;
		return -1;
	}
	private void LblLogic(string colors)
	{
		MoveDownCorrectCorner(colors);
		TurnDownToConnector(downCorrectCorner);
		do
		{
			LblAlgoritm(connectorthis);
		} while (!(cube.DOWN[downConnector] == 'y' &&
		(_whitecorners.CheckCornersColors("DOWN", downConnector) == colors ||
		_whitecorners.CheckCornersColors("DOWN", downConnector) == new string(colors.Reverse().ToArray()))));
		CorrectinYellowCross();
	}
	private void LblAlgoritm(string connector)
	{
		switch (connector)
		{
			case "frontRightEdge":
				_walls.TurnRightRight(cube);
				_walls.TurnUpRight(cube);
				_walls.TurnRightLeft(cube);
				_walls.TurnUpLeft(cube);
				break;
			case "frontLeftEdge":
				_walls.TurnFrontRight(cube);
				_walls.TurnUpRight(cube);
				_walls.TurnFrontLeft(cube);
				_walls.TurnUpLeft(cube);
				break;
			case "backRightEdge":
				_walls.TurnLeftRight(cube);
				_walls.TurnUpRight(cube);
				_walls.TurnLeftLeft(cube);
				_walls.TurnUpLeft(cube);
				break;
			case "backLeftEdge":
				_walls.TurnBackRight(cube);
				_walls.TurnUpRight(cube);
				_walls.TurnBackLeft(cube);
				_walls.TurnUpLeft(cube);
				break;
		}
	}
	private void CurvedLblAlgoritm(string connector)
	{
		switch (connector)
		{
			case "frontRightEdge":
				_walls.TurnDownRight(cube);
				_walls.TurnBackRight(cube);
				_walls.TurnDownLeft(cube);
				_walls.TurnBackLeft(cube);
				break;
			case "frontLeftEdge":
				_walls.TurnDownRight(cube);
				_walls.TurnRightRight(cube);
				_walls.TurnDownLeft(cube);
				_walls.TurnRightLeft(cube);
				break;
			case "backRightEdge":
				_walls.TurnDownRight(cube);
				_walls.TurnFrontRight(cube);
				_walls.TurnDownLeft(cube);
				_walls.TurnFrontLeft(cube);
				break;
			case "backLeftEdge":
				_walls.TurnDownRight(cube);
				_walls.TurnLeftRight(cube);
				_walls.TurnDownLeft(cube);
				_walls.TurnLeftLeft(cube);
				break;
		}
	}
	private void CorrectinYellowCross()
	{
		if (cube.DOWN[1] == 'y' && cube.DOWN[3] == 'y' && cube.DOWN[5] == 'y' && cube.DOWN[7] == 'y')
		{
			char color = _cross.GetAdjacentColor(cube, "DOWN", 1);
			switch (color)
			{
				case 'o':
					_walls.TurnDownRight(cube);
					_walls.TurnDownRight(cube);
					break;
				case 'g':
					_walls.TurnDownLeft(cube);
					break;
				case 'b':
					_walls.TurnDownRight(cube);
					break;
			}
		}
	}
	private void MoveDownCorrectCorner(string colors)
	{
		switch (colors)
		{
			case "br":
				downCorrectCorner = 2;
				break;
			case "gr":
				downCorrectCorner = 1;
				break;
			case "go":
				downCorrectCorner = 3;
				break;
			case "bo":
				downCorrectCorner = 4;
				break;
		}

	}
	private bool CheckingSolvedCorners(string connector)
	{
		bool leftUpCorner = (cube.DOWN[0] == 'y' && cube.FRONT[6] == 'r' && cube.LEFT[8] == 'g');
		bool rightUpCorner = (cube.DOWN[2] == 'y' && cube.FRONT[8] == 'r' && cube.RIGHT[6] == 'b');
		bool leftDownCorner = (cube.DOWN[6] == 'y' && cube.LEFT[6] == 'g' && cube.BACK[8] == 'o');
		bool rightDownCorner = (cube.DOWN[8] == 'y' && cube.RIGHT[8] == 'b' && cube.BACK[6] == 'o');

		int count = 0;

		if (leftUpCorner) count++;
		if (rightUpCorner) count++;
		if (leftDownCorner) count++;
		if (rightDownCorner) count++;

		return count == 4;
	}
	private void ConnectorsSet(string connector)
	{
		switch (connector)
		{
			case "frontRightEdge":
				upConnector = 8;
				downConnector = 2;
				break;
			case "frontLeftEdge":
				upConnector = 6;
				downConnector = 0;
				break;
			case "backRightEdge":
				upConnector = 0;
				downConnector = 6;
				break;
			case "backLeftEdge":
				upConnector = 2;
				downConnector = 8;
				break;
		}
	}
	private void TurnDownToConnector(int position)
	{
		switch (position)
		{
			case 1:
				switch (downConnector)
				{
					case 2:
						_walls.TurnDownRight(cube);
						break;
					case 6:
						_walls.TurnDownLeft(cube);
						break;
					case 8:
						_walls.TurnDownRight(cube);
						_walls.TurnDownRight(cube);
						break;
				}
				break;
			case 2:
				switch (downConnector)
				{
					case 0:
						_walls.TurnDownLeft(cube);
						break;
					case 6:
						_walls.TurnDownRight(cube);
						_walls.TurnDownRight(cube);
						break;
					case 8:
						_walls.TurnDownRight(cube);
						break;
				}
				break;
			case 3:
				switch (downConnector)
				{
					case 0:
						_walls.TurnDownRight(cube);
						break;
					case 2:
						_walls.TurnDownRight(cube);
						_walls.TurnDownRight(cube);
						break;
					case 8:
						_walls.TurnDownLeft(cube);
						break;
				}
				break;
			case 4:
				switch (downConnector)
				{
					case 0:
						_walls.TurnDownLeft(cube);
						_walls.TurnDownLeft(cube);
						break;
					case 2:
						_walls.TurnDownLeft(cube);
						break;
					case 6:
						_walls.TurnDownRight(cube);
						break;
				}
				break;
		}
	}
	private bool Solved3Corners(string connector)
	{
		bool leftUpCorner = (cube.DOWN[0] == 'y' && cube.FRONT[6] == 'r' && cube.LEFT[8] == 'g');
		bool rightUpCorner = (cube.DOWN[2] == 'y' && cube.FRONT[8] == 'r' && cube.RIGHT[6] == 'b');
		bool leftDownCorner = (cube.DOWN[6] == 'y' && cube.LEFT[6] == 'g' && cube.BACK[8] == 'o');
		bool rightDownCorner = (cube.DOWN[8] == 'y' && cube.RIGHT[8] == 'b' && cube.BACK[6] == 'o');

		int count = 0;

		if (connector != "frontRightEdge" && rightUpCorner) count++;
		if (connector != "frontLeftEdge" && leftUpCorner) count++;
		if (connector != "backRightEdge" && leftDownCorner) count++;
		if (connector != "backLeftEdge" && rightDownCorner) count++;

		return count == 3;
	}
	private bool CheckingWeirdSitation()
	{
		bool solved = CheckingSolvedCube();
		bool corners = Solved3Corners(connectorthis);
		bool whiteWall = true;
		for (int i = 0; i < 9; i++)
		{
			if (i != upConnector)
			{
				if (cube.UP[i] != 'w')
				{
					whiteWall = false;
					break;
				}
			}
		}
		bool yellowWall = true;
		for (int i = 0; i < 9; i++)
		{
			if (i != downConnector)
			{
				if (cube.DOWN[i] != 'y')
				{
					yellowWall = false;
					break;
				}
			}
		}
		if (whiteWall && yellowWall && !solved && corners)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	private bool CheckingYellowDoneWhiteNope()
	{
		bool yellowWallDone = true;
		for (int i = 0; i < 9; i++)
		{
			if (cube.DOWN[i] != 'y')
			{
				yellowWallDone = false;
				break;
			}
		}
		bool whiteWallDone = true;
		for (int i = 0; i < 9; i++)
		{
			if (cube.UP[i] != 'w')
			{
				whiteWallDone = false;
				break;
			}
		}
		if (yellowWallDone == true && whiteWallDone != true)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	public bool CheckingSolvedCube()
	{
		bool UP = true;
		bool DOWN = true;
		bool RIGHT = true;
		bool LEFT = true;
		bool BACK = true;
		bool FRONT = true;

		for (int i = 0; i < 9; i++)
		{
			if (cube.UP[i] != 'w')
			{
				UP = false;
				break;
			}
			if (cube.DOWN[i] != 'y')
			{
				DOWN = false;
				break;
			}
			if (cube.RIGHT[i] != 'b')
			{
				RIGHT = false;
				break;
			}
			if (cube.LEFT[i] != 'g')
			{
				LEFT = false;
				break;
			}
			if (cube.FRONT[i] != 'r')
			{
				FRONT = false;
				break;
			}
			if (cube.BACK[i] != 'o')
			{
				BACK = false;
				break;
			}
		}
		if (UP && DOWN && LEFT && RIGHT && FRONT && BACK)
		{
			return true;
		}
		else
		{
			return false;
		}

	}
}

