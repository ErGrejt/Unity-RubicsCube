using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MovingWalls : MonoBehaviour
	{
		private CubeData cube = CubeData.Instance;
		private RotatingSides rotate = new RotatingSides();
		public static List<string> moves = new List<string>();
		private int countMoves = 0;
		public TMP_Text labelText;
		public float rotatingSpeed = 0.6f;
		private bool nextStep = true;
		private Solver solver;
	//Movings
	public void TurnFrontRight(CubeData cube)
		{
			moves.Add("TurnFrontRight");
			RotateFrontClockwise(cube);
		}
		public void TurnFrontLeft(CubeData cube)
		{
			moves.Add("TurnFrontLeft");
			RotateFrontCounterClockwise(cube);
		}
		public void TurnUpRight(CubeData cube)
		{
			moves.Add("TurnUpRight");
			RotateUpClockwise(cube);
		}
		public void TurnUpLeft(CubeData cube)
		{
			moves.Add("TurnUpLeft");
			RotateUpCounterClockwise(cube);
		}
		public void TurnRightRight(CubeData cube)
		{
			moves.Add("TurnRightRight");
			RotateRightClockwise(cube);
		}
		public void TurnRightLeft(CubeData cube)
		{
			moves.Add("TurnRightLeft");
			RotateRightCounterClockwise(cube);
		}
		public void TurnLeftRight(CubeData cube)
		{
			moves.Add("TurnLeftRight");
			RotateLeftClockwise(cube);
		}
		public void TurnLeftLeft(CubeData cube)
		{
			moves.Add("TurnLeftLeft");
			RotateLeftCounterClockwise(cube);
		}
		public void TurnBackRight(CubeData cube)
		{
			moves.Add("TurnBackRight");
			RotateBackClockwise(cube);
		}
		public void TurnBackLeft(CubeData cube)
		{
			moves.Add("TurnBackLeft");
			RotateBackCounterClockwise(cube);
		}
		public void TurnDownRight(CubeData cube)
		{
			moves.Add("TurnDownRight");
			RotateDownClockwise(cube);
		}
		public void TurnDownLeft(CubeData cube)
		{
			moves.Add("TurnDownLeft");
			RotateDownCounterClockwise(cube);
		}
		private void CountMoves(string message)
		{
			Console.WriteLine(message + moves.Count);
			Console.ReadLine();
		}
		public void WriteMoves(int number)
		{
			RemoveMoves();
			GameObject test = GameObject.Find("CubeBig");
			solver = test.GetComponent<Solver>();
		if (number == 1)
			{
				CountMoves("Iloœæ ruchów do u³o¿enia bia³ego krzy¿a: ");
			}
			else if (number == 2)
			{
				CountMoves("Iloœæ ruchów do u³o¿enia œrodkowych 3 krawêdzi: ");
			}
			else if (number == 3)
			{
				CountMoves("Iloœæ ruchów do u³o¿enia 3 bia³ych naro¿ników: ");
			}
			else if (number == 4)
			{
				CountMoves("Iloœæ ruchów do u³o¿enia ¿ó³tego krzy¿a: ");
			}
			else if (number == 5)
			{
				CountMoves("Iloœæ ruchów do u³o¿enia naro¿ników: ");
			}
			else if (number == 0)
			{
				CountMoves("Inne: ");
			}
		if(solver.Autobool) StartCoroutine(WriteMovesCoroutine());
	}
	private IEnumerator WriteMovesCoroutine()
	{
		for (int i = 0; i < moves.Count; i++)
		{
			ConsoleWLMoves(moves[i]);
			yield return new WaitForSeconds(rotatingSpeed);
		}
		labelText.text = "";
	}
	public void ConsoleWLMoves(string move)
		{
			switch (move)
			{
				case "TurnFrontRight":
				labelText.text = "FRONT W PRAWO";
				GetComponent<RotatingSides>().MoveFrontRight();
				break;
				case "TurnFrontLeft":
				labelText.text = "FRONT W LEWO";
				GetComponent<RotatingSides>().MoveFrontLeft();
				break;
				case "TurnBackRight":
				labelText.text = "BACK W PRAWO";
				GetComponent<RotatingSides>().MoveBackRight();
				break;
				case "TurnBackLeft":
				labelText.text = "BACK W LEWO";
				GetComponent<RotatingSides>().MoveBackLeft();
				break;
				case "TurnUpRight":
				labelText.text = "UP W PRAWO";
				GetComponent<RotatingSides>().MoveUpRight();
				break;
				case "TurnUpLeft":
				labelText.text = "UP W LEWO";
				GetComponent<RotatingSides>().MoveUpLeft();
				break;
				case "TurnDownRight":
				labelText.text = "DOWN W PRAWO";
				GetComponent<RotatingSides>().MoveDownRight();
				break;
				case "TurnDownLeft":
				labelText.text = "DOWN W LEWO";
				GetComponent<RotatingSides>().MoveDownLeft();
				break;
				case "TurnLeftRight":
				labelText.text = "LEFT W PRAWO";
				GetComponent<RotatingSides>().MoveLeftRight();
				break;
				case "TurnLeftLeft":
				labelText.text = "LEFT W LEWO";
				GetComponent<RotatingSides>().MoveLeftLeft();
				break;
				case "TurnRightRight":
				labelText.text = "RIGHT W PRAWO";
				GetComponent<RotatingSides>().MoveRightRight();
				break;
				case "TurnRightLeft":
				labelText.text = "RIGHT W LEWO";
				GetComponent<RotatingSides>().MoveRightLeft();
				break;
				default:
					Console.WriteLine("NIEZNANY RUCH");
					break;
			}
		}
		private void RemoveMoves()
		{
			int i = 0;
			while (i < moves.Count - 1)
			{
				if (isCanceling(moves[i], moves[i + 1]))
				{
					moves.RemoveAt(i);
					moves.RemoveAt(i);
					i = 0;
				}
				else
				{
					i++;
				}
			}
			ReplaceTripleMoves();

		}
		private bool isCanceling(string move1, string move2)
		{

			if ((move1 == "TurnUpLeft" && move2 == "TurnUpRight") ||
				(move1 == "TurnUpRight" && move2 == "TurnUpLeft") ||
				(move1 == "TurnFrontLeft" && move2 == "TurnFrontRight") ||
				(move1 == "TurnFrontRight" && move2 == "TurnFrontLeft") ||
				(move1 == "TurnDownLeft" && move2 == "TurnDownRight") ||
				(move1 == "TurnDownRight" && move2 == "TurnDownLeft") ||
				(move1 == "TurnLeftLeft" && move2 == "TurnLeftRight") ||
				(move1 == "TurnLeftRight" && move2 == "TurnLeftLeft") ||
				(move1 == "TurnRightLeft" && move2 == "TurnRightRight") ||
				(move1 == "TurnRightRight" && move2 == "TurnRightLeft") ||
				(move1 == "TurnBackLeft" && move2 == "TurnBackRight") ||
				(move1 == "TurnBackRight" && move2 == "TurnBackLeft"))
			{
				return true;
			}

			return false;
		}
		private void ReplaceTripleMoves()
		{
			Dictionary<string, string> oppositeMoves = new Dictionary<string, string>
			{
				{ "TurnUpLeft", "TurnUpRight" },
				{ "TurnUpRight", "TurnUpLeft" },
				{ "TurnFrontLeft", "TurnFrontRight" },
				{ "TurnFrontRight", "TurnFrontLeft" },
				{ "TurnDownLeft", "TurnDownRight" },
				{ "TurnDownRight", "TurnDownLeft" },
				{ "TurnLeftLeft", "TurnLeftRight" },
				{ "TurnLeftRight", "TurnLeftLeft" },
				{ "TurnRightLeft", "TurnRightRight" },
				{ "TurnRightRight", "TurnRightLeft" },
				{ "TurnBackLeft", "TurnBackRight" },
				{ "TurnBackRight", "TurnBackLeft" }
			};

			int i = 0;
			while (i < moves.Count - 2)
			{
				if (moves[i] == moves[i + 1] && moves[i] == moves[i + 2])
				{
					moves[i] = oppositeMoves[moves[i]];
					moves.RemoveAt(i + 1);
					moves.RemoveAt(i + 1);
					i = 0;
				}
				else
				{
					i++;
				}
			}
		}
		private void RotateSideLeft(char[] side)
		{
			var temp = new char[9];
			temp[0] = side[2];
			temp[1] = side[5];
			temp[2] = side[8];
			temp[3] = side[1];
			temp[4] = side[4];
			temp[5] = side[7];
			temp[6] = side[0];
			temp[7] = side[3];
			temp[8] = side[6];
			Array.Copy(temp, side, 9);
		}
		private void RotateSideRight(char[] side)
		{
			var temp = new char[9];
			temp[0] = side[6];
			temp[1] = side[3];
			temp[2] = side[0];
			temp[3] = side[7];
			temp[4] = side[4];
			temp[5] = side[1];
			temp[6] = side[8];
			temp[7] = side[5];
			temp[8] = side[2];
			Array.Copy(temp, side, 9);
		}
		//Turn Front Right
		public void RotateFrontClockwise(CubeData cube)
		{
			RotateSideRight(cube.FRONT);
			char[] temp = { cube.UP[6], cube.UP[7], cube.UP[8] };

			cube.UP[6] = cube.LEFT[8];
			cube.UP[7] = cube.LEFT[5];
			cube.UP[8] = cube.LEFT[2];

			cube.LEFT[2] = cube.DOWN[0];
			cube.LEFT[5] = cube.DOWN[1];
			cube.LEFT[8] = cube.DOWN[2];

			cube.DOWN[0] = cube.RIGHT[6];
			cube.DOWN[1] = cube.RIGHT[3];
			cube.DOWN[2] = cube.RIGHT[0];

			cube.RIGHT[0] = temp[0];
			cube.RIGHT[3] = temp[1];
			cube.RIGHT[6] = temp[2];
		}
		//Turn Front Left
		public void RotateFrontCounterClockwise(CubeData cube)
		{
			RotateSideLeft(cube.FRONT);
			char[] temp = { cube.LEFT[2], cube.LEFT[5], cube.LEFT[8] };

			cube.LEFT[2] = cube.UP[8];
			cube.LEFT[5] = cube.UP[7];
			cube.LEFT[8] = cube.UP[6];

			cube.UP[6] = cube.RIGHT[0];
			cube.UP[7] = cube.RIGHT[3];
			cube.UP[8] = cube.RIGHT[6];

			cube.RIGHT[0] = cube.DOWN[2];
			cube.RIGHT[3] = cube.DOWN[1];
			cube.RIGHT[6] = cube.DOWN[0];

			cube.DOWN[2] = temp[2];
			cube.DOWN[1] = temp[1];
			cube.DOWN[0] = temp[0];
		}
		//Turn Up Right
		public void RotateUpClockwise(CubeData cube)
		{
			RotateSideRight(cube.UP);
			char[] temp = { cube.BACK[0], cube.BACK[1], cube.BACK[2] };

			cube.BACK[0] = cube.LEFT[0];
			cube.BACK[1] = cube.LEFT[1];
			cube.BACK[2] = cube.LEFT[2];

			cube.LEFT[0] = cube.FRONT[0];
			cube.LEFT[1] = cube.FRONT[1];
			cube.LEFT[2] = cube.FRONT[2];

			cube.FRONT[0] = cube.RIGHT[0];
			cube.FRONT[1] = cube.RIGHT[1];
			cube.FRONT[2] = cube.RIGHT[2];

			cube.RIGHT[0] = temp[0];
			cube.RIGHT[1] = temp[1];
			cube.RIGHT[2] = temp[2];
		}
		//Turn Up Left
		public void RotateUpCounterClockwise(CubeData cube)
		{
			RotateSideLeft(cube.UP);
			char[] temp = { cube.LEFT[0], cube.LEFT[1], cube.LEFT[2] };

			cube.LEFT[0] = cube.BACK[0];
			cube.LEFT[1] = cube.BACK[1];
			cube.LEFT[2] = cube.BACK[2];

			cube.BACK[0] = cube.RIGHT[0];
			cube.BACK[1] = cube.RIGHT[1];
			cube.BACK[2] = cube.RIGHT[2];

			cube.RIGHT[0] = cube.FRONT[0];
			cube.RIGHT[1] = cube.FRONT[1];
			cube.RIGHT[2] = cube.FRONT[2];

			cube.FRONT[0] = temp[0];
			cube.FRONT[1] = temp[1];
			cube.FRONT[2] = temp[2];
		}
		//Turn Right Right
		public void RotateRightClockwise(CubeData cube)
		{
			RotateSideRight(cube.RIGHT);
			char[] temp = { cube.UP[2], cube.UP[5], cube.UP[8] };

			cube.UP[2] = cube.FRONT[2];
			cube.UP[5] = cube.FRONT[5];
			cube.UP[8] = cube.FRONT[8];

			cube.FRONT[2] = cube.DOWN[2];
			cube.FRONT[5] = cube.DOWN[5];
			cube.FRONT[8] = cube.DOWN[8];

			cube.DOWN[2] = cube.BACK[6];
			cube.DOWN[5] = cube.BACK[3];
			cube.DOWN[8] = cube.BACK[0];

			cube.BACK[0] = temp[2];
			cube.BACK[3] = temp[1];
			cube.BACK[6] = temp[0];
		}
		//Turn Right Left
		public void RotateRightCounterClockwise(CubeData cube)
		{
			RotateSideLeft(cube.RIGHT);
			char[] temp = { cube.UP[2], cube.UP[5], cube.UP[8] };

			cube.UP[2] = cube.BACK[6];
			cube.UP[5] = cube.BACK[3];
			cube.UP[8] = cube.BACK[0];

			cube.BACK[0] = cube.DOWN[8];
			cube.BACK[3] = cube.DOWN[5];
			cube.BACK[6] = cube.DOWN[2];

			cube.DOWN[2] = cube.FRONT[2];
			cube.DOWN[5] = cube.FRONT[5];
			cube.DOWN[8] = cube.FRONT[8];

			cube.FRONT[2] = temp[0];
			cube.FRONT[5] = temp[1];
			cube.FRONT[8] = temp[2];
		}
		//Turn Left Right
		public void RotateLeftClockwise(CubeData cube)
		{
			RotateSideRight(cube.LEFT);
			char[] temp = { cube.UP[0], cube.UP[3], cube.UP[6] };

			cube.UP[0] = cube.BACK[8];
			cube.UP[3] = cube.BACK[5];
			cube.UP[6] = cube.BACK[2];

			cube.BACK[2] = cube.DOWN[6];
			cube.BACK[5] = cube.DOWN[3];
			cube.BACK[8] = cube.DOWN[0];

			cube.DOWN[0] = cube.FRONT[0];
			cube.DOWN[3] = cube.FRONT[3];
			cube.DOWN[6] = cube.FRONT[6];

			cube.FRONT[0] = temp[0];
			cube.FRONT[3] = temp[1];
			cube.FRONT[6] = temp[2];
		}
		//Turn Left Left
		public void RotateLeftCounterClockwise(CubeData cube)
		{
			RotateSideLeft(cube.LEFT);
			char[] temp = { cube.UP[0], cube.UP[3], cube.UP[6] };

			cube.UP[0] = cube.FRONT[0];
			cube.UP[3] = cube.FRONT[3];
			cube.UP[6] = cube.FRONT[6];

			cube.FRONT[0] = cube.DOWN[0];
			cube.FRONT[3] = cube.DOWN[3];
			cube.FRONT[6] = cube.DOWN[6];

			cube.DOWN[0] = cube.BACK[8];
			cube.DOWN[3] = cube.BACK[5];
			cube.DOWN[6] = cube.BACK[2];

			cube.BACK[2] = temp[2];
			cube.BACK[5] = temp[1];
			cube.BACK[8] = temp[0];
		}
		//Turn Back Right
		public void RotateBackClockwise(CubeData cube)
		{
			RotateSideRight(cube.BACK);
			char[] temp = { cube.UP[0], cube.UP[1], cube.UP[2] };

			cube.UP[0] = cube.RIGHT[2];
			cube.UP[1] = cube.RIGHT[5];
			cube.UP[2] = cube.RIGHT[8];

			cube.RIGHT[2] = cube.DOWN[8];
			cube.RIGHT[5] = cube.DOWN[7];
			cube.RIGHT[8] = cube.DOWN[6];

			cube.DOWN[8] = cube.LEFT[6];
			cube.DOWN[7] = cube.LEFT[3];
			cube.DOWN[6] = cube.LEFT[0];

			cube.LEFT[0] = temp[2];
			cube.LEFT[3] = temp[1];
			cube.LEFT[6] = temp[0];
		}
		//Turn Back Left
		public void RotateBackCounterClockwise(CubeData cube)
		{
			RotateSideLeft(cube.BACK);
			char[] temp = { cube.UP[0], cube.UP[1], cube.UP[2] };

			cube.UP[0] = cube.LEFT[6];
			cube.UP[1] = cube.LEFT[3];
			cube.UP[2] = cube.LEFT[0];

			cube.LEFT[0] = cube.DOWN[6];
			cube.LEFT[3] = cube.DOWN[7];
			cube.LEFT[6] = cube.DOWN[8];

			cube.DOWN[6] = cube.RIGHT[8];
			cube.DOWN[7] = cube.RIGHT[5];
			cube.DOWN[8] = cube.RIGHT[2];

			cube.RIGHT[2] = temp[0];
			cube.RIGHT[5] = temp[1];
			cube.RIGHT[8] = temp[2];
		}
		//Turn Down Right
		public void RotateDownClockwise(CubeData cube)
		{
			RotateSideRight(cube.DOWN);
			char[] temp = { cube.FRONT[6], cube.FRONT[7], cube.FRONT[8] };

			cube.FRONT[6] = cube.LEFT[6];
			cube.FRONT[7] = cube.LEFT[7];
			cube.FRONT[8] = cube.LEFT[8];

			cube.LEFT[6] = cube.BACK[6];
			cube.LEFT[7] = cube.BACK[7];
			cube.LEFT[8] = cube.BACK[8];

			cube.BACK[6] = cube.RIGHT[6];
			cube.BACK[7] = cube.RIGHT[7];
			cube.BACK[8] = cube.RIGHT[8];

			cube.RIGHT[6] = temp[0];
			cube.RIGHT[7] = temp[1];
			cube.RIGHT[8] = temp[2];
		}
		//Turn Down Left
		public void RotateDownCounterClockwise(CubeData cube)
		{
			RotateSideLeft(cube.DOWN);
			char[] temp = { cube.FRONT[6], cube.FRONT[7], cube.FRONT[8] };

			cube.FRONT[6] = cube.RIGHT[6];
			cube.FRONT[7] = cube.RIGHT[7];
			cube.FRONT[8] = cube.RIGHT[8];

			cube.RIGHT[6] = cube.BACK[6];
			cube.RIGHT[7] = cube.BACK[7];
			cube.RIGHT[8] = cube.BACK[8];

			cube.BACK[6] = cube.LEFT[6];
			cube.BACK[7] = cube.LEFT[7];
			cube.BACK[8] = cube.LEFT[8];

			cube.LEFT[6] = temp[0];
			cube.LEFT[7] = temp[1];
			cube.LEFT[8] = temp[2];
		}
	}
