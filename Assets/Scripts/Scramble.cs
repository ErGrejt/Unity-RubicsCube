using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Scramble : MonoBehaviour
{
    private MoveSidesButtons _move;
    private Solver solver;
    private System.Random rnd = new System.Random();
    public bool Scrambling = false;
    private bool Rotating = false;
    public float rotatingSpeed = 0.6f;
    public TMP_Text moves;
    public void CubeScramble()
    {
        StartCoroutine(ScrambleCube());
    }
	private IEnumerator ScrambleCube()
    {
        Scrambling = true;
		GameObject test = GameObject.Find("CubeBig");
		_move = test.GetComponent<MoveSidesButtons>();
		GameObject test2 = GameObject.Find("CubeBig");
		solver = test2.GetComponent<Solver>();
		for (int i = 0; i < 20; i++)
        {
            Rotating = true;
            if (!Scrambling || solver.Stepping) break;
            int randomMove = rnd.Next(1,13);
            switch (randomMove)
            {
                case 1:
                    _move.UpRight();
                    break;
                case 2:
                    _move.UpLeft();
                    break;
                case 3:
					_move.DownRight();
					break;
                case 4:
                    _move.DownLeft();
                    break;
                case 5:
					_move.FrontLeft();
					break;
                case 6:
                    _move.FrontRight();
                    break;
                case 7:
                    _move.BackLeft();
                    break;
                case 8:
                    _move.BackRight();
                    break;
                case 9:
                    _move.RightRight();
                    break;
                case 10:
                    _move.LeftRight();
                    break;
                case 11:
                    _move.RightLeft();
                    break;
                case 12:
                    _move.LeftLeft();
					break;
            }
            //Scrambling = false;
			yield return new WaitForSeconds(rotatingSpeed);
		}
        Scrambling = false;
		if (!solver.Stepping) moves.text = "";
	}
    
}
