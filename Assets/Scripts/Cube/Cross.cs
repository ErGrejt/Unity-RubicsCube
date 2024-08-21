using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;


	public class Cross : MonoBehaviour
	{
		private CubeData cube = CubeData.Instance;
		private MovingWalls _walls;
		public void SolveWhiteCross()
		{
			GameObject test = GameObject.Find("CubeBig");
			_walls = test.GetComponent<MovingWalls>();
			CorrectinWhiteCross(); //Poprawiæ to pozniej bo siê funkcje powielaj¹
			MoveWhiteEdgesToTop();
		}
		private void CorrectinWhiteCross()
		{
			if (cube.UP[1] == 'w' && cube.UP[3] == 'w' && cube.UP[5] == 'w' && cube.UP[7] == 'w')
			{
				char color = GetAdjacentColor(cube, "UP", 1);
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
		private void MoveWhiteEdgesToTop()
		{
			bool whiteCrossOnTop = false;
			while (!whiteCrossOnTop)
			{
				for (int i = 0; i < 9; i++)
				{
					if (cube.UP[i] == 'w')
					{
						char sidecolor = GetAdjacentColor(cube, "UP", i);
						MoveWhiteEdgeFromTopToGoodPosition(i, sidecolor);
					}
					if (cube.BACK[i] == 'w')
					{
						char sidecolor = GetAdjacentColor(cube, "BACK", i);
						MoveWhiteEdgeToTopFromBack(i, sidecolor);
					}
					if (cube.RIGHT[i] == 'w')
					{
						char sidecolor = GetAdjacentColor(cube, "RIGHT", i);
						MoveWhiteEdgeToTopFromRight(i, sidecolor);
					}
					if (cube.LEFT[i] == 'w')
					{
						char sidecolor = GetAdjacentColor(cube, "LEFT", i);
						MoveWhiteEdgeToTopFromLeft(i, sidecolor);
					}
					if (cube.DOWN[i] == 'w')
					{
						char sidecolor = GetAdjacentColor(cube, "DOWN", i);
						MoveWhiteEdgeToTopFromDown(i, sidecolor);
					}
					if (cube.FRONT[i] == 'w')
					{
						char sidecolor = GetAdjacentColor(cube, "FRONT", i);
						MoveWhiteEdgeToTopFromFront(i, sidecolor);
					}
				}
				whiteCrossOnTop = (cube.UP[1] == 'w' && cube.UP[3] == 'w' && cube.UP[5] == 'w' && cube.UP[7] == 'w');
			}

		_walls.WriteMoves(1);
		}
		private void MoveWhiteEdgeFromTopToGoodPosition(int position, char sidecolor)
		{
			switch (position)
			{
				case 1:
					switch (sidecolor)
					{
						case 'b':
							_walls.TurnBackLeft(cube);
							_walls.TurnUpLeft(cube);
							_walls.TurnBackRight(cube);
							_walls.TurnUpRight(cube);

							break;
						case 'g':
							_walls.TurnBackRight(cube);
							_walls.TurnUpRight(cube);
							_walls.TurnBackLeft(cube);
							_walls.TurnUpLeft(cube);

							break;
						case 'r':
							_walls.TurnBackLeft(cube);
							_walls.TurnUpRight(cube);
							_walls.TurnUpRight(cube);
							_walls.TurnBackRight(cube);
							_walls.TurnUpLeft(cube);
							_walls.TurnUpLeft(cube);
							break;
					}
					break;
				case 3:
					switch (sidecolor)
					{
						case 'b':
							_walls.TurnLeftRight(cube);
							_walls.TurnUpRight(cube);
							_walls.TurnUpRight(cube);
							_walls.TurnLeftLeft(cube);
							_walls.TurnUpLeft(cube);
							_walls.TurnUpLeft(cube);
							break;
						case 'r':
							_walls.TurnLeftRight(cube);
							_walls.TurnUpRight(cube);
							_walls.TurnLeftLeft(cube);
							_walls.TurnUpLeft(cube);
							break;
						case 'o':
							_walls.TurnLeftLeft(cube);
							_walls.TurnUpLeft(cube);
							_walls.TurnLeftRight(cube);
							_walls.TurnUpRight(cube);
							break;
					}
					break;
				case 5:
					switch (sidecolor)
					{
						case 'g':
							_walls.TurnRightLeft(cube);
							_walls.TurnUpRight(cube);
							_walls.TurnUpRight(cube);
							_walls.TurnRightRight(cube);
							_walls.TurnUpLeft(cube);
							_walls.TurnUpLeft(cube);
							break;
						case 'r':
							_walls.TurnRightLeft(cube);
							_walls.TurnUpLeft(cube);
							_walls.TurnRightRight(cube);
							_walls.TurnUpRight(cube);
							break;
						case 'o':
							_walls.TurnRightRight(cube);
							_walls.TurnUpRight(cube);
							_walls.TurnRightLeft(cube);
							_walls.TurnUpLeft(cube);
							break;
					}
					break;
				case 7:
					switch (sidecolor)
					{
						case 'b':
							_walls.TurnFrontRight(cube);
							_walls.TurnUpRight(cube);
							_walls.TurnFrontLeft(cube);
							_walls.TurnUpLeft(cube);
							break;
						case 'g':
							_walls.TurnFrontLeft(cube);
							_walls.TurnUpLeft(cube);
							_walls.TurnFrontRight(cube);
							_walls.TurnUpRight(cube);
							break;
						case 'o':
							_walls.TurnFrontRight(cube);
							_walls.TurnUpRight(cube);
							_walls.TurnUpRight(cube);
							_walls.TurnFrontLeft(cube);
							_walls.TurnUpLeft(cube);
							_walls.TurnUpLeft(cube);
							break;
					}
					break;
			}
		}
		private void MoveWhiteEdgeToTopFromFront(int position, char sidecolor)
		{
			switch (position)
			{
				case 1:
					switch (sidecolor)
					{
						case 'b':
							_walls.TurnFrontRight(cube);
							_walls.TurnRightRight(cube);
							break;
						case 'g':
							_walls.TurnFrontLeft(cube);
							_walls.TurnLeftLeft(cube);
							break;
						case 'r':
							_walls.TurnFrontRight(cube);
							_walls.TurnUpLeft(cube);
							_walls.TurnRightRight(cube);
							_walls.TurnUpRight(cube);
							break;
						case 'o':
							_walls.TurnFrontRight(cube);
							_walls.TurnUpRight(cube);
							_walls.TurnRightRight(cube);
							_walls.TurnUpLeft(cube);
							break;
					}
					break;
				case 3:
					switch (sidecolor)
					{
						case 'b':
							_walls.TurnUpRight(cube);
							_walls.TurnUpRight(cube);
							_walls.TurnLeftLeft(cube);
							_walls.TurnUpLeft(cube);
							_walls.TurnUpLeft(cube);
							break;
						case 'g':
							_walls.TurnLeftLeft(cube);
							break;
						case 'r':
							_walls.TurnUpRight(cube);
							_walls.TurnLeftLeft(cube);
							_walls.TurnUpLeft(cube);
							break;
						case 'o':
							_walls.TurnUpLeft(cube);
							_walls.TurnLeftLeft(cube);
							_walls.TurnUpRight(cube);
							break;
					}
					break;
				case 5:
					switch (sidecolor)
					{
						case 'b':
							_walls.TurnRightRight(cube);
							break;
						case 'g':
							_walls.TurnUpLeft(cube);
							_walls.TurnUpLeft(cube);
							_walls.TurnRightRight(cube);
							_walls.TurnUpRight(cube);
							_walls.TurnUpRight(cube);
							break;
						case 'r':
							_walls.TurnUpLeft(cube);
							_walls.TurnRightRight(cube);
							_walls.TurnUpRight(cube);
							break;
						case 'o':
							_walls.TurnUpRight(cube);
							_walls.TurnRightRight(cube);
							_walls.TurnUpLeft(cube);
							break;
					}
					break;
				case 7:
					switch (sidecolor)
					{
						case 'b':
							_walls.TurnFrontLeft(cube);
							_walls.TurnRightRight(cube);
							_walls.TurnFrontRight(cube);
							break;
						case 'g':
							_walls.TurnFrontRight(cube);
							_walls.TurnLeftLeft(cube);
							_walls.TurnFrontLeft(cube);
							break;
						case 'r':
							_walls.TurnFrontRight(cube);
							_walls.TurnUpRight(cube);
							_walls.TurnLeftLeft(cube);
							_walls.TurnUpLeft(cube);
							break;
						case 'o':
							_walls.TurnFrontLeft(cube);
							_walls.TurnUpRight(cube);
							_walls.TurnRightRight(cube);
							_walls.TurnUpLeft(cube);
							_walls.TurnFrontRight(cube);
							break;
					}
					break;
			}
		}
		private void MoveWhiteEdgeToTopFromRight(int position, char sidecolor)
		{
			switch (position)
			{
				case 1:
					switch (sidecolor)
					{
						case 'b':
							_walls.TurnRightRight(cube);
							_walls.TurnUpLeft(cube);
							_walls.TurnBackRight(cube);
							_walls.TurnUpRight(cube);
							break;
						case 'g':
							_walls.TurnRightRight(cube);
							_walls.TurnUpRight(cube);
							_walls.TurnBackRight(cube);
							_walls.TurnUpLeft(cube);
							break;
						case 'o':
							_walls.TurnRightRight(cube);
							_walls.TurnBackRight(cube);
							break;
						case 'r':
							_walls.TurnRightLeft(cube);
							_walls.TurnFrontLeft(cube);
							break;
					}
					break;
				case 3:
					switch (sidecolor)
					{
						case 'b':
							_walls.TurnUpRight(cube);
							_walls.TurnFrontLeft(cube);
							_walls.TurnUpLeft(cube);
							break;
						case 'g':
							_walls.TurnUpLeft(cube);
							_walls.TurnFrontLeft(cube);
							_walls.TurnUpRight(cube);
							break;
						case 'o':
							_walls.TurnUpRight(cube);
							_walls.TurnUpRight(cube);
							_walls.TurnFrontLeft(cube);
							_walls.TurnUpLeft(cube);
							_walls.TurnUpLeft(cube);
							break;
						case 'r':
							_walls.TurnFrontLeft(cube);
							break;
					}
					break;
				case 5:
					switch (sidecolor)
					{
						case 'b':
							_walls.TurnUpLeft(cube);
							_walls.TurnBackRight(cube);
							_walls.TurnUpRight(cube);
							break;
						case 'g':
							_walls.TurnUpRight(cube);
							_walls.TurnBackRight(cube);
							_walls.TurnUpLeft(cube);
							break;
						case 'o':
							_walls.TurnBackRight(cube);
							break;
						case 'r':
							_walls.TurnUpLeft(cube);
							_walls.TurnUpLeft(cube);
							_walls.TurnBackRight(cube);
							_walls.TurnUpRight(cube);
							_walls.TurnUpRight(cube);
							break;
					}
					break;
				case 7:
					switch (sidecolor)
					{
						case 'b':
							_walls.TurnRightLeft(cube);
							_walls.TurnUpLeft(cube);
							_walls.TurnBackRight(cube);
							_walls.TurnUpRight(cube);
							break;
						case 'g':
							_walls.TurnRightRight(cube);
							_walls.TurnUpLeft(cube);
							_walls.TurnFrontLeft(cube);
							_walls.TurnUpRight(cube);
							_walls.TurnRightLeft(cube);
							break;
						case 'o':
							_walls.TurnRightLeft(cube);
							_walls.TurnBackRight(cube);
							_walls.TurnRightLeft(cube);
							break;
						case 'r':
							_walls.TurnRightRight(cube);
							_walls.TurnFrontLeft(cube);
							_walls.TurnRightLeft(cube);
							break;
					}
					break;
			}
		}
		private void MoveWhiteEdgeToTopFromLeft(int position, char sidecolor)
		{
			switch (position)
			{
				case 1:
					switch (sidecolor)
					{
						case 'b':
							_walls.TurnLeftRight(cube);
							_walls.TurnUpRight(cube);
							_walls.TurnFrontRight(cube);
							_walls.TurnUpLeft(cube);
							break;
						case 'g':
							_walls.TurnLeftRight(cube);
							_walls.TurnUpLeft(cube);
							_walls.TurnUpRight(cube);
							_walls.TurnUpRight(cube);
							break;
						case 'r':
							_walls.TurnLeftRight(cube);
							_walls.TurnFrontRight(cube);
							break;
						case 'o':
							_walls.TurnLeftLeft(cube);
							_walls.TurnBackLeft(cube);
							break;
					}
					break;
				case 3:
					switch (sidecolor)
					{
						case 'b':
							_walls.TurnUpLeft(cube);
							_walls.TurnBackRight(cube);
							_walls.TurnUpRight(cube);
							break;
						case 'g':
							_walls.TurnUpRight(cube);
							_walls.TurnBackLeft(cube);
							break;
						case 'r':
							_walls.TurnUpRight(cube);
							_walls.TurnUpRight(cube);
							_walls.TurnBackLeft(cube);
							_walls.TurnUpLeft(cube);
							_walls.TurnUpLeft(cube);
							break;
						case 'o':
							_walls.TurnBackLeft(cube);
							break;
					}
					break;
				case 5:
					switch (sidecolor)
					{
						case 'b':
							_walls.TurnUpRight(cube);
							_walls.TurnFrontRight(cube);
							_walls.TurnUpLeft(cube);
							break;
						case 'g':
							_walls.TurnUpLeft(cube);
							_walls.TurnFrontRight(cube);
							_walls.TurnUpRight(cube);
							break;
						case 'r':
							_walls.TurnFrontRight(cube);
							break;
						case 'o':
							_walls.TurnUpLeft(cube);
							_walls.TurnUpLeft(cube);
							_walls.TurnFrontRight(cube);
							_walls.TurnUpRight(cube);
							_walls.TurnUpRight(cube);
							break;
					}
					break;
				case 7:
					switch (sidecolor)
					{
						case 'b':
							_walls.TurnLeftLeft(cube);
							_walls.TurnUpRight(cube);
							_walls.TurnFrontRight(cube);
							_walls.TurnUpLeft(cube);
							_walls.TurnLeftRight(cube);
							break;
						case 'g':
							_walls.TurnLeftLeft(cube);
							_walls.TurnUpLeft(cube);
							_walls.TurnFrontRight(cube);
							_walls.TurnUpRight(cube);
							break;
						case 'r':
							_walls.TurnLeftLeft(cube);
							_walls.TurnFrontRight(cube);
							break;
						case 'o':
							_walls.TurnLeftRight(cube);
							_walls.TurnBackLeft(cube);
							break;
					}
					break;
			}
		}
		private void MoveWhiteEdgeToTopFromBack(int position, char sidecolor)
		{
			switch (position)
			{
				case 1:
					switch (sidecolor)
					{
						case 'b':
							_walls.TurnBackLeft(cube);
							_walls.TurnRightLeft(cube);
							break;
						case 'g':
							_walls.TurnBackRight(cube);
							_walls.TurnLeftRight(cube);
							break;
						case 'r':
							_walls.TurnBackRight(cube);
							_walls.TurnUpRight(cube);
							_walls.TurnLeftRight(cube);
							_walls.TurnUpLeft(cube);
							break;
						case 'o':
							_walls.TurnBackRight(cube);
							_walls.TurnUpLeft(cube);
							_walls.TurnLeftRight(cube);
							_walls.TurnUpRight(cube);
							break;
					}
					break;
				case 3:
					switch (sidecolor)
					{
						case 'b':
							_walls.TurnRightLeft(cube);
							break;
						case 'g':
							_walls.TurnUpRight(cube);
							_walls.TurnUpRight(cube);
							_walls.TurnRightLeft(cube);
							_walls.TurnUpLeft(cube);
							_walls.TurnUpLeft(cube);
							break;
						case 'r':
							_walls.TurnUpLeft(cube);
							_walls.TurnRightLeft(cube);
							_walls.TurnUpRight(cube);
							break;
						case 'o':
							_walls.TurnUpRight(cube);
							_walls.TurnRightLeft(cube);
							_walls.TurnUpLeft(cube);
							break;
					}
					break;
				case 5:
					switch (sidecolor)
					{
						case 'b':
							_walls.TurnUpLeft(cube);
							_walls.TurnUpLeft(cube);
							_walls.TurnLeftRight(cube);
							_walls.TurnUpRight(cube);
							_walls.TurnUpRight(cube);
							break;
						case 'g':
							_walls.TurnLeftRight(cube);
							break;
						case 'r':
							_walls.TurnUpRight(cube);
							_walls.TurnLeftRight(cube);
							_walls.TurnUpLeft(cube);
							break;
						case 'o':
							_walls.TurnUpLeft(cube);
							_walls.TurnLeftRight(cube);
							_walls.TurnUpRight(cube);
							break;
					}
					break;
				case 7:
					switch (sidecolor)
					{
						case 'b':
							_walls.TurnBackLeft(cube);
							_walls.TurnRightLeft(cube);
							_walls.TurnBackRight(cube);
							break;
						case 'g':
							_walls.TurnBackLeft(cube);
							_walls.TurnLeftRight(cube);
							_walls.TurnBackRight(cube);
							break;
						case 'r':
							_walls.TurnBackRight(cube);
							_walls.TurnUpLeft(cube);
							_walls.TurnRightLeft(cube);
							_walls.TurnUpRight(cube);
							_walls.TurnBackLeft(cube);
							break;
						case 'o':
							_walls.TurnBackRight(cube);
							_walls.TurnUpRight(cube);
							_walls.TurnRightLeft(cube);
							_walls.TurnUpLeft(cube);
							break;
					}
					break;
			}
		}
		private void MoveWhiteEdgeToTopFromDown(int position, char sidecolor)
		{
			switch (position)
			{
				case 1:
					switch (sidecolor)
					{
						case 'b':
							_walls.TurnDownRight(cube);
							_walls.TurnRightLeft(cube);
							_walls.TurnRightLeft(cube);
							break;
						case 'g':
							_walls.TurnDownLeft(cube);
							_walls.TurnLeftRight(cube);
							_walls.TurnLeftRight(cube);
							break;
						case 'r':
							_walls.TurnFrontRight(cube);
							_walls.TurnFrontRight(cube);
							break;
						case 'o':
							_walls.TurnDownRight(cube);
							_walls.TurnDownRight(cube);
							_walls.TurnBackRight(cube);
							_walls.TurnBackRight(cube);
							break;
					}
					break;
				case 3:
					switch (sidecolor)
					{
						case 'b':
							_walls.TurnDownRight(cube);
							_walls.TurnDownRight(cube);
							_walls.TurnRightLeft(cube);
							_walls.TurnRightLeft(cube);
							break;
						case 'g':
							_walls.TurnLeftRight(cube);
							_walls.TurnLeftRight(cube);
							break;
						case 'r':
							_walls.TurnDownRight(cube);
							_walls.TurnFrontRight(cube);
							_walls.TurnFrontRight(cube);
							break;
						case 'o':
							_walls.TurnDownLeft(cube);
							_walls.TurnBackRight(cube);
							_walls.TurnBackRight(cube);
							break;
					}
					break;
				case 5:
					switch (sidecolor)
					{
						case 'b':
							_walls.TurnRightLeft(cube);
							_walls.TurnRightLeft(cube);
							break;
						case 'g':
							_walls.TurnDownRight(cube);
							_walls.TurnDownRight(cube);
							_walls.TurnLeftRight(cube);
							_walls.TurnLeftRight(cube);
							break;
						case 'r':
							_walls.TurnDownLeft(cube);
							_walls.TurnFrontRight(cube);
							_walls.TurnFrontRight(cube);
							break;
						case 'o':
							_walls.TurnDownRight(cube);
							_walls.TurnBackRight(cube);
							_walls.TurnBackRight(cube);
							break;
					}
					break;
				case 7:
					switch (sidecolor)
					{
						case 'b':
							_walls.TurnDownLeft(cube);
							_walls.TurnRightLeft(cube);
							_walls.TurnRightLeft(cube);
							break;
						case 'g':
							_walls.TurnDownRight(cube);
							_walls.TurnLeftRight(cube);
							_walls.TurnLeftRight(cube);
							break;
						case 'r':
							_walls.TurnDownRight(cube);
							_walls.TurnDownRight(cube);
							_walls.TurnFrontRight(cube);
							_walls.TurnFrontRight(cube);
							break;
						case 'o':
							_walls.TurnBackRight(cube);
							_walls.TurnBackRight(cube);
							break;
					}
					break;
			}
		}
		public char GetAdjacentColor(CubeData cube, string face, int position)
		{
			switch (face)
			{
				case "FRONT":
					switch (position)
					{
						case 1:
							return cube.UP[7];
						case 3:
							return cube.LEFT[5];
						case 5:
							return cube.RIGHT[3];
						case 7:
							return cube.DOWN[1];
					}
					break;
				case "BACK":
					switch (position)
					{
						case 1:
							return cube.UP[1];
						case 3:
							return cube.RIGHT[5];
						case 5:
							return cube.LEFT[3];
						case 7:
							return cube.DOWN[7];
					}
					break;
				case "LEFT":
					switch (position)
					{
						case 1:
							return cube.UP[3];
						case 3:
							return cube.BACK[5];
						case 5:
							return cube.FRONT[3];
						case 7:
							return cube.DOWN[3];
					}
					break;
				case "RIGHT":
					switch (position)
					{
						case 1:
							return cube.UP[5];
						case 3:
							return cube.FRONT[5];
						case 5:
							return cube.BACK[3];
						case 7:
							return cube.DOWN[5];
					}
					break;
				case "UP":
					switch (position)
					{
						case 1:
							return cube.BACK[1];
						case 3:
							return cube.LEFT[1];
						case 5:
							return cube.RIGHT[1];
						case 7:
							return cube.FRONT[1];
					}
					break;
				case "DOWN":
					switch (position)
					{
						case 1:
							return cube.FRONT[7];
						case 3:
							return cube.LEFT[7];
						case 5:
							return cube.RIGHT[7];
						case 7:
							return cube.BACK[7];
					}
					break;
			}
			return '1';
		}
	}
