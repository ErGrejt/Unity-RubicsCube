using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Solver : MonoBehaviour
{
    private Cross _cross = new Cross();
    private SecondLayerEdges _edges = new SecondLayerEdges();
    private WhiteCorners _whiteCorners = new WhiteCorners();
    private YellowCross _yellowCross = new YellowCross();
    private YellowCorners _yellowCorners = new YellowCorners();
	private KociembaScript koc;
    private MovingWalls _walls;
    private Scramble scramble;
    public TMP_Text text;
    public bool Autobool = false;
    public bool Stepping = false;
    public int index = -1;
	public int indexKociemba = -1;
    public Button stepButton;
    public Button auto;
    public Button manual;
    public TMP_Text stepCount;
	public TMP_Text stepCountKociemba;
	//Kociemba
	public Button kociembaButton;
	public Button lblButton;
    public bool AlgoritmLbl = true;
	public Button nextStepKociemba;
	private void Start()
	{
        AutoButton();
        ChangeAlgToLbl();
		stepButton.gameObject.SetActive(false);
		nextStepKociemba.gameObject.SetActive(false);
	}
	public void SolveCube()
    {
		GameObject test = GameObject.Find("CubeBig");
		scramble = test.GetComponent<Scramble>();
		GameObject test2 = GameObject.Find("CubeBig");
		_walls = test2.GetComponent<MovingWalls>();
		GameObject test3 = GameObject.Find("CubeBig");
		koc = test3.GetComponent<KociembaScript>();
		//Lbl
		if (AlgoritmLbl)
        {
			if (_yellowCorners.CheckingSolvedCube() != true && scramble.Scrambling == false && !Stepping)
			{
				if (!Autobool)
				{
					stepButton.gameObject.SetActive(true);
					Stepping = true;
				}
				MovingWalls.moves.Clear();
				_cross.SolveWhiteCross();
				_edges.SolveEdgesSecondLayer();
				string edge = _edges.ConnectorEdge();
				_whiteCorners.SolveWhiteCorners(edge);
				_edges.SolverLastEdgeSecondLayer(edge);
				_yellowCross.SolveYellowCross();
				_yellowCorners.SolveCornersLastStep(edge);
				if (Stepping)
				{
					stepCount.text = $"Pozosta³e ruchy: {MovingWalls.moves.Count}";
				}
			}
			else
			{
				if (scramble.Scrambling == false && !Stepping)
				{
					MovingWalls.moves.Clear();
					Debug.Log("Kostka u³o¿ona");
					text.text = "Kostka u³o¿ona";
				}
			}
		} else
        {
			//Kociemba
			if (_yellowCorners.CheckingSolvedCube() != true && scramble.Scrambling == false && !Stepping)
			{
				StartCoroutine(koc.KociembaStart());
				if (!Autobool)
				{
					nextStepKociemba.gameObject.SetActive(true);
					Stepping = true;
					stepCountKociemba.text = $"Pozosta³e ruchy: {koc.result.Count}";
				}
			}
			else
			{
				if (scramble.Scrambling == false && !Stepping)
				{
					text.text = "Kostka u³o¿ona";
				}
			}
		}
	}
	public void KociembaNextStep()
	{
		indexKociemba++;
		if(indexKociemba <= koc.result.Count -1)
		{
			int resultkoc = koc.result.Count - indexKociemba;
			stepCountKociemba.text = $"Pozosta³e ruchy: {resultkoc - 1}";
			KociembaMove();
		} else
		{
			Stepping = false;
			nextStepKociemba.gameObject.SetActive(false);
			text.text = "Kostka u³o¿ona";
		}
	}
	private void KociembaMove()
	{
		koc.KociembaMovesSwitch(koc.result[indexKociemba]);
	}

    public void ChangeAlgToLbl()
    {
        lblButton.gameObject.SetActive(true);
        kociembaButton.gameObject.SetActive(false);
        AlgoritmLbl = true;
    }
    public void ChangeAlgToKociemba()
    {
		lblButton.gameObject.SetActive(false);
		kociembaButton.gameObject.SetActive(true);
        AlgoritmLbl = false;
	}
    public void StepMoves()
    {
        _walls.ConsoleWLMoves(MovingWalls.moves[index]);
    }
    public void NextStep()
    {
        index++;
        if (index <= MovingWalls.moves.Count-1)
        {
            int result = MovingWalls.moves.Count - index;
			stepCount.text = $"Pozosta³e ruchy: {result-1}";
			StepMoves();
        }
        else
        {
            Stepping = false;
            stepButton.gameObject.SetActive(false);
			text.text = "Kostka u³o¿ona";
		}
    }
    public void ResetBools()
    {
        Stepping = false;
        stepButton.gameObject.SetActive(false);
        //text.text = "";
    }
    public void AutoButton()
    {
		Autobool = false;
		manual.gameObject.SetActive(true);
		auto.gameObject.SetActive(false);
	}
    public void ManualButton()
    {
		Autobool = true;
        manual.gameObject.SetActive(false);
        auto.gameObject.SetActive(true);
	}
}
