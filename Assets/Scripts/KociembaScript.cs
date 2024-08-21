using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kociemba;
using UnityEngine.UI;

public class KociembaScript : MonoBehaviour
{
	private CubeData cube = CubeData.Instance;
	private Solver solver;
	private MoveSidesButtons move;
	private string Colors;
	private string info = "";
	public List<string> result;
	public float rotatingSpeed = 0.6f;
	private bool AlgReady = false;
	public IEnumerator KociembaStart()
	{
		GameObject test = GameObject.Find("CubeBig");
		move = test.GetComponent<MoveSidesButtons>();
		GameObject test2 = GameObject.Find("CubeBig");
		solver = test2.GetComponent<Solver>();
		KociembaSolver();
		//string solution = SearchRunTime.solution(Colors, out info, buildTables: true);
		string solution = Search.solution(Colors, out info);
		solution = ExpandDoubleMoves(solution);
		result = StringToList(solution);
		while (AlgReady == false)
		{
			yield return null;
		}
		yield return new WaitForSeconds(0.5f);
		if(solver.Autobool == true)
		{
			StartCoroutine(Moving());
		}
	}

	private void KociembaSolver()
	{
		Colors = "";
		ColorsString();
	}
	private void ColorsString()
	{
		for(int i = 0; i < 9; i++)
		{
			Colors += cube.UP[i];
		}
		for (int i = 0; i < 9; i++)
		{
			Colors += cube.RIGHT[i];
		}
		for (int i = 0; i < 9; i++)
		{
			Colors += cube.FRONT[i];
		}
		for (int i = 0; i < 9; i++)
		{
			Colors += cube.DOWN[i];
		}
		for (int i = 0; i < 9; i++)
		{
			Colors += cube.LEFT[i];
		}
		for (int i = 0; i < 9; i++)
		{
			Colors += cube.BACK[i];
		}
		ReplaceChars();
	}
	private void ReplaceChars()
	{
		Colors = Colors.Replace('w', 'U');
		Colors = Colors.Replace('b', 'R');
		Colors = Colors.Replace('r', 'F');
		Colors = Colors.Replace('y', 'D');
		Colors = Colors.Replace('g', 'L');
		Colors = Colors.Replace('o', 'B');
	}
	private List<string> StringToList(string solution)
	{
		List<string> solutionList = new List<string>(solution.Split(new string[] { " " },
			System.StringSplitOptions.RemoveEmptyEntries));
		AlgReady = true;
		return solutionList;
	}
	private string ExpandDoubleMoves(string moves)
	{
		Dictionary<string, string> doubleMoves = new Dictionary<string, string>()
		{
			{ "F2", "F F" },
			{ "B2", "B B" },
			{ "L2", "L L" },
			{ "R2", "R R" },
			{ "U2", "U U" },
			{ "D2", "D D" }
		};

		string[] moveArray = moves.Split(' ');
		List<string> expandedMoves = new List<string>();

		foreach (string move in moveArray)
		{
			if (doubleMoves.ContainsKey(move))
			{
				expandedMoves.Add(doubleMoves[move]);
			}
			else
			{
				expandedMoves.Add(move);
			}
		}

		return string.Join(" ", expandedMoves);
	}
	private IEnumerator Moving()
	{
		for(int i = 0; i < result.Count; i++)
		{
			KociembaMovesSwitch(result[i]);
			yield return new WaitForSeconds(rotatingSpeed);
		}
	}
	public void KociembaMovesSwitch(string moveChar)
	{
		switch (moveChar)
		{
			case "F":
				move.FrontRight();
				break;
			case "F'":
				move.FrontLeft();
				break;
			case "B":
				move.BackRight();
				break;
			case "B'":
				move.BackLeft();
				break;
			case "L":
				move.LeftRight();
				break;
			case "L'":
				move.LeftLeft();
				break;
			case "R":
				move.RightRight();
				break;
			case "R'":
				move.RightLeft();
				break;
			case "U":
				move.UpRight();
				break;
			case "U'":
				move.UpLeft();
				break;
			case "D":
				move.DownRight();
				break;
			case "D'":
				move.DownLeft();
				break;
		}
	}
}
