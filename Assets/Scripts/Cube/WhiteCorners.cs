using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WhiteCorners : MonoBehaviour
{
	private CubeData cube = CubeData.Instance;
	private MovingWalls _walls;
	private Cross _cross = new Cross();
	//Connectors edges
	private int upConnector;
	private int downConnector;
	private string connectorThis;
	private int cornerUpCorr;
	private int cornerDownCorr;
	public void SolveWhiteCorners(string connector)
	{
		GameObject test = GameObject.Find("CubeBig");
		_walls = test.GetComponent<MovingWalls>();
		connectorThis = connector;
		ConnectorsSet(connector);
		string colors;
		bool corners = CheckingSolvedCorners(connectorThis);
		while (!corners)
		{
			for (int i = 8; i >= 0; i--)
			{
				if (cube.DOWN[i] == 'w')
				{
					colors = CheckCornersColors("DOWN", i);
					if (!colors.Contains("y"))
					{
						if (!Skip(connector, colors))
						{
							SolveWhiteCornersDown(colors, i);
						}
					}
				}
				if (cube.BACK[i] == 'w')
				{
					colors = CheckCornersColors("BACK", i);
					if (!colors.Contains("y"))
					{
						if (!Skip(connector, colors))
						{
							SolveWhiteCornersBack(colors, i);
						}
					}
				}
				if (cube.RIGHT[i] == 'w')
				{
					colors = CheckCornersColors("RIGHT", i);
					if (!colors.Contains("y"))
					{
						if (!Skip(connector, colors))
						{
							SolveWhiteCornersRight(colors, i);
						}
					}
				}
				if (cube.LEFT[i] == 'w')
				{
					colors = CheckCornersColors("LEFT", i);
					if (!colors.Contains("y"))
					{
						if (!Skip(connector, colors))
						{
							SolveWhiteCornersLeft(colors, i);
						}
					}
				}
				if (cube.FRONT[i] == 'w')
				{
					colors = CheckCornersColors("FRONT", i);
					if (!colors.Contains("y"))
					{
						if (!Skip(connector, colors))
						{
							SolveWhiteCornersFront(colors, i);
						}
					}
				}
				if (cube.UP[i] == 'w')
				{
					colors = CheckCornersColors("UP", i);
					if (!colors.Contains("y"))
					{
						if (!Skip(connector, colors))
						{
							SolveWhiteCornersUp(colors, i);
						}
					}
				}
			}
			corners = CheckingSolvedCorners(connectorThis);
		}
		_walls.WriteMoves(3);
	}
	private bool CheckingSolvedCorners(string connector)
	{
		bool frontRightCorner = (cube.UP[8] == 'w' && cube.RIGHT[0] == 'b' && cube.FRONT[2] == 'r');
		bool frontLeftCorner = (cube.UP[6] == 'w' && cube.FRONT[0] == 'r' && cube.LEFT[2] == 'g');
		bool backRightCorner = (cube.UP[0] == 'w' && cube.LEFT[0] == 'g' && cube.BACK[2] == 'o');
		bool backLeftCorner = (cube.UP[2] == 'w' && cube.BACK[0] == 'o' && cube.RIGHT[2] == 'b');

		int count = 0;

		if (connector != "frontRightEdge" && frontRightCorner) count++;
		if (connector != "frontLeftEdge" && frontLeftCorner) count++;
		if (connector != "backRightEdge" && backRightCorner) count++;
		if (connector != "backLeftEdge" && backLeftCorner) count++;

		return count >= 3;
	}
	private bool Skip(string connector, string colors)
	{
		switch (connector)
		{
			case "frontRightEdge":
				if (colors == "br" || colors == "rb") return true;
				break;
			case "frontLeftEdge":
				if (colors == "gr" || colors == "rg") return true;
				break;
			case "backRightEdge":
				if (colors == "go" || colors == "og") return true;
				break;
			case "backLeftEdge":
				if (colors == "bo" || colors == "ob") return true;
				break;

		}
		return false;
	}
	private void SolveWhiteCornersFront(string colors, int positon)
	{
		colors = TransformColors(colors);
		int number = 0;
		string receivedColors = "xd";
		switch (positon)
		{
			case 0:
				number = 3;
				cornerDownCorr = downConnector;
				break;
			case 2:
				number = 4;
				cornerDownCorr = downConnector;
				break;
			case 6:
				cornerDownCorr = 1;
				break;
			case 8:
				cornerDownCorr = 2;
				break;
		}
		switch (colors)
		{
			case "br":
				cornerUpCorr = 4;
				receivedColors = "br";
				break;
			case "gr":
				cornerUpCorr = 3;
				receivedColors = "gr";
				break;
			case "go":
				cornerUpCorr = 1;
				receivedColors = "go";
				break;
			case "bo":
				cornerUpCorr = 2;
				receivedColors = "bo";
				break;
		}
		LogicLblMoving(number, receivedColors);
	}
	private void SolveWhiteCornersBack(string colors, int positon)
	{
		colors = TransformColors(colors);
		int number = 0;
		string receivedColors = "xd";
		switch (positon)
		{
			case 0:
				number = 2;
				cornerDownCorr = downConnector;
				break;
			case 2:
				number = 1;
				cornerDownCorr = downConnector;
				break;
			case 6:
				cornerDownCorr = 4;
				break;
			case 8:
				cornerDownCorr = 3;
				break;
		}
		switch (colors)
		{
			case "br":
				cornerUpCorr = 4;
				receivedColors = "br";
				break;
			case "gr":
				cornerUpCorr = 3;
				receivedColors = "gr";
				break;
			case "go":
				cornerUpCorr = 1;
				receivedColors = "go";
				break;
			case "bo":
				cornerUpCorr = 2;
				receivedColors = "bo";
				break;
		}
		LogicLblMoving(number, receivedColors);
	}
	private void SolveWhiteCornersRight(string colors, int positon)
	{
		colors = TransformColors(colors);
		int number = 0;
		string receivedColors = "xd";
		switch (positon)
		{
			case 0:
				number = 4;
				cornerDownCorr = downConnector;
				break;
			case 2:
				number = 2;
				cornerDownCorr = downConnector;
				break;
			case 6:
				cornerDownCorr = 2;
				break;
			case 8:
				cornerDownCorr = 4;
				break;
		}
		switch (colors)
		{
			case "br":
				cornerUpCorr = 4;
				receivedColors = "br";
				break;
			case "gr":
				cornerUpCorr = 3;
				receivedColors = "gr";
				break;
			case "go":
				cornerUpCorr = 1;
				receivedColors = "go";
				break;
			case "bo":
				cornerUpCorr = 2;
				receivedColors = "bo";
				break;
		}
		LogicLblMoving(number, receivedColors);
	}
	private void SolveWhiteCornersLeft(string colors, int positon)
	{
		colors = TransformColors(colors);
		int number = 0;
		string receivedColors = "xd";
		switch (positon)
		{
			case 0:
				number = 1;
				cornerDownCorr = downConnector;
				break;
			case 2:
				number = 3;
				cornerDownCorr = downConnector;
				break;
			case 6:
				cornerDownCorr = 3;
				break;
			case 8:
				cornerDownCorr = 1;
				break;
		}
		switch (colors)
		{
			case "br":
				cornerUpCorr = 4;
				receivedColors = "br";
				break;
			case "gr":
				cornerUpCorr = 3;
				receivedColors = "gr";
				break;
			case "go":
				cornerUpCorr = 1;
				receivedColors = "go";
				break;
			case "bo":
				cornerUpCorr = 2;
				receivedColors = "bo";
				break;
		}
		LogicLblMoving(number, receivedColors);
	}
	private void SolveWhiteCornersDown(string colors, int positon)
	{
		colors = TransformColors(colors);
		int number = 0;
		string receivedColors = "xd";
		switch (positon)
		{
			case 0:
				cornerDownCorr = 1;
				break;
			case 2:
				cornerDownCorr = 2;
				break;
			case 6:
				cornerDownCorr = 3;
				break;
			case 8:
				cornerDownCorr = 4;
				break;
		}
		switch (colors)
		{
			case "br":
				cornerUpCorr = 4;
				receivedColors = "br";
				break;
			case "gr":
				cornerUpCorr = 3;
				receivedColors = "gr";
				break;
			case "go":
				cornerUpCorr = 1;
				receivedColors = "go";
				break;
			case "bo":
				cornerUpCorr = 2;
				receivedColors = "bo";
				break;
		}
		LogicLblMoving(number, receivedColors);
	}
	private void SolveWhiteCornersUp(string colors, int positon)
	{
		colors = TransformColors(colors);
		int number = 0;
		string receivedColors = "xd";
		switch (positon)
		{
			case 0:
				number = -1;
				if (colors != "go") number = 1;
				break;
			case 2:
				number = -1;
				if (colors != "bo") number = 2;
				break;
			case 6:
				number = -1;
				if (colors != "gr") number = 3;
				break;
			case 8:
				number = -1;
				if (colors != "br") number = 4;
				break;
		}
		switch (colors)
		{
			case "br":
				cornerUpCorr = 4;
				receivedColors = "br";
				break;
			case "gr":
				cornerUpCorr = 3;
				receivedColors = "gr";
				break;
			case "go":
				cornerUpCorr = 1;
				receivedColors = "go";
				break;
			case "bo":
				cornerUpCorr = 2;
				receivedColors = "bo";
				break;
		}
		LogicLblMoving(number, receivedColors);
	}
	private void LogicLblMoving(int number, string receivedColors)
	{
		if (number == -1) return;
		if (number != 0)
		{
			TurnUpToConnector(number);
			MoveFromUpToDownWrongCorner(connectorThis);
			CorrectinWhiteCross();
		}
		if (number == 0)
		{
			if (cornerUpCorr == 1 && upConnector != 0 || cornerUpCorr == 2 && upConnector != 2 || cornerUpCorr == 3 && upConnector != 6 || cornerUpCorr == 4 && upConnector != 8)
			{
				TurnUpToConnector(cornerUpCorr);
				TurnDownToConnector(cornerDownCorr);
				do
				{
					LblAlgoritm(connectorThis);
				} while (!(cube.UP[upConnector] == 'w' && (CheckCornersColors("UP", upConnector) == receivedColors || CheckCornersColors("UP", upConnector) == new string(receivedColors.Reverse().ToArray()))));
				CorrectinWhiteCross();
			}
		}
	}
	public void CorrectinWhiteCross()
	{
		if (cube.UP[1] == 'w' && cube.UP[3] == 'w' && cube.UP[5] == 'w' && cube.UP[7] == 'w')
		{
			char color = _cross.GetAdjacentColor(cube, "UP", 1);
			switch (color)
			{
				case 'o':
					break;
				case 'g':
					_walls.TurnUpLeft(cube);
					break;
				case 'b':
					_walls.TurnUpRight(cube);
					break;
				case 'r':
					_walls.TurnUpLeft(cube);
					_walls.TurnUpLeft(cube);
					break;
			}
		}
	}
	private void TurnUpToConnector(int position)
	{
		switch (position)
		{
			case 1:
				switch (upConnector)
				{
					case 2:
						_walls.TurnUpRight(cube);
						break;
					case 6:
						_walls.TurnUpLeft(cube);
						break;
					case 8:
						_walls.TurnUpLeft(cube);
						_walls.TurnUpLeft(cube);
						break;
				}
				break;
			case 2:
				switch (upConnector)
				{
					case 0:
						_walls.TurnUpLeft(cube);
						break;
					case 6:
						_walls.TurnUpLeft(cube);
						_walls.TurnUpLeft(cube);
						break;
					case 8:
						_walls.TurnUpRight(cube);
						break;
				}
				break;
			case 3:
				switch (upConnector)
				{
					case 0:
						_walls.TurnUpRight(cube);
						break;
					case 2:
						_walls.TurnUpRight(cube);
						_walls.TurnUpRight(cube);
						break;
					case 8:
						_walls.TurnUpLeft(cube);
						break;
				}
				break;
			case 4:
				switch (upConnector)
				{
					case 0:
						_walls.TurnUpLeft(cube);
						_walls.TurnUpLeft(cube);
						break;
					case 2:
						_walls.TurnUpLeft(cube);
						break;
					case 6:
						_walls.TurnUpRight(cube);
						break;
				}
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
	public string TransformColors(string colors)
	{
		if (colors == "rb") return "br";
		if (colors == "rg") return "gr";
		if (colors == "og") return "go";
		if (colors == "ob") return "bo";
		return colors;
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
	public string CheckCornersColors(string face, int position)
	{
		switch (face)
		{
			case "UP":
				switch (position)
				{
					case 0: return $"{cube.LEFT[0]}{cube.BACK[2]}";
					case 2: return $"{cube.RIGHT[2]}{cube.BACK[0]}";
					case 6: return $"{cube.LEFT[2]}{cube.FRONT[0]}";
					case 8: return $"{cube.RIGHT[0]}{cube.FRONT[2]}";
					default: return "y";
				}

			case "DOWN":
				switch (position)
				{
					case 0: return $"{cube.LEFT[8]}{cube.FRONT[6]}";
					case 2: return $"{cube.RIGHT[6]}{cube.FRONT[8]}";
					case 6: return $"{cube.LEFT[6]}{cube.BACK[8]}";
					case 8: return $"{cube.RIGHT[8]}{cube.BACK[6]}";
					default: return "y";
				}
			case "FRONT":
				switch (position)
				{
					case 0: return $"{cube.LEFT[2]}{cube.UP[6]}";
					case 2: return $"{cube.RIGHT[0]}{cube.UP[8]}";
					case 6: return $"{cube.LEFT[8]}{cube.DOWN[0]}";
					case 8: return $"{cube.RIGHT[6]}{cube.DOWN[2]}";
					default: return "y";
				}
			case "BACK":
				switch (position)
				{
					case 0: return $"{cube.RIGHT[2]}{cube.UP[2]}";
					case 2: return $"{cube.LEFT[0]}{cube.UP[0]}";
					case 6: return $"{cube.RIGHT[8]}{cube.DOWN[8]}";
					case 8: return $"{cube.LEFT[6]}{cube.DOWN[6]}";
					default: return "y";
				}
			case "RIGHT":
				switch (position)
				{
					case 0: return $"{cube.FRONT[2]}{cube.UP[8]}";
					case 2: return $"{cube.BACK[0]}{cube.UP[2]}";
					case 6: return $"{cube.FRONT[8]}{cube.DOWN[2]}";
					case 8: return $"{cube.BACK[6]}{cube.DOWN[8]}";
					default: return "y";
				}
			case "LEFT":
				switch (position)
				{
					case 0: return $"{cube.BACK[2]}{cube.UP[0]}";
					case 2: return $"{cube.FRONT[0]}{cube.UP[6]}";
					case 6: return $"{cube.BACK[8]}{cube.DOWN[6]}";
					case 8: return $"{cube.FRONT[6]}{cube.DOWN[0]}";
					default: return "y";
				}
		}
		return null;
	}
	public void LblAlgoritm(string connector)
	{
		switch (connector)
		{
			case "frontRightEdge":
				_walls.TurnFrontRight(cube);
				_walls.TurnDownRight(cube);
				_walls.TurnFrontLeft(cube);
				_walls.TurnDownLeft(cube);
				break;
			case "frontLeftEdge":
				_walls.TurnLeftRight(cube);
				_walls.TurnDownRight(cube);
				_walls.TurnLeftLeft(cube);
				_walls.TurnDownLeft(cube);
				break;
			case "backRightEdge":
				_walls.TurnBackRight(cube);
				_walls.TurnDownRight(cube);
				_walls.TurnBackLeft(cube);
				_walls.TurnDownLeft(cube);
				break;
			case "backLeftEdge":
				_walls.TurnRightRight(cube);
				_walls.TurnDownRight(cube);
				_walls.TurnRightLeft(cube);
				_walls.TurnDownLeft(cube);
				break;
		}
	}
	private void MoveFromUpToDownWrongCorner(string connector)
	{
		switch (connector)
		{
			case "frontRightEdge":
				_walls.TurnFrontRight(cube);
				_walls.TurnDownRight(cube);
				_walls.TurnFrontLeft(cube);
				break;
			case "frontLeftEdge":
				_walls.TurnLeftRight(cube);
				_walls.TurnDownRight(cube);
				_walls.TurnLeftLeft(cube);
				break;
			case "backRightEdge":
				_walls.TurnBackRight(cube);
				_walls.TurnDownRight(cube);
				_walls.TurnBackLeft(cube);
				break;
			case "backLeftEdge":
				_walls.TurnRightRight(cube);
				_walls.TurnDownRight(cube);
				_walls.TurnRightLeft(cube);
				break;
		}
	}
}

