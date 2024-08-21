using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ClickWall : MonoBehaviour
{
    private CubeData cube = CubeData.Instance;
    private Scramble scrambleScript;
    public Material black;
    public Material purple;
    private NeighborChecker neigh;
    private List<GameObject> cubes;
    private GameObject lastClickedObject;
    private string wall;
    private bool reset = false;
    public List<GameObject> centers;
    public Button frontRight;
    public Button frontLeft;
    public Button RightRight;
    public Button RightLeft;
	public Button backRight;
	public Button backLeft;
	public Button LeftRight;
	public Button LeftLeft;
    public Button upRight;
    public Button upLeft;
    public Button downRight;
    public Button downLeft;
    public TMP_Text startText;
    //Script
    private SettingsPanel setPnl;
    public Button startSolving;
    private bool start = false;
    private int Score = 0;
	private bool nameEntered = false;
    public TMP_Text textScore;

	private void Start()
    {
		GameObject test = GameObject.Find("UI");
		setPnl = test.GetComponent<SettingsPanel>();
		GameObject test2 = GameObject.Find("CubeBig");
		scrambleScript = test2.GetComponent<Scramble>();
		HideAllButtons();
    }
    void Update()
    {
        if(setPnl.isFun && start)
        {
            if (startText.text != "" && scrambleScript.Scrambling == false)
            {
                startText.text = "Zaznacz œcianê któr¹ chcesz obróciæ";
            }

			if (Input.GetMouseButtonDown(0) && scrambleScript.Scrambling == false)
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit))
				{
					startText.text = "";
					if (hit.collider.gameObject == lastClickedObject && lastClickedObject != null)
					{

					}
					else
					{
						if (lastClickedObject != null)
						{
							reset = true;
						}
						HideAllButtons();
                        try
                        {
						    HandleClick(hit.collider.gameObject);
                        } catch(Exception ex)
                        {
                            
                        }
					}
				}
			}
		}
        if(IsCubeSolved() && start)
        {
			startSolving.gameObject.SetActive(true);
			int temp = PlayerPrefs.GetInt("MinScore", int.MaxValue);
			if (temp > Score)
			{
				PlayerPrefs.SetInt("MinScore", Score);
				PlayerPrefs.Save();
				Score = 0;
                textScore.text = "";
			}
			setPnl.UpdateScore();
			HideAllButtons();
			ResetColorsMark();
		}
    }
    
	public void ScoreAdd()
    {
        Score++;
        textScore.text = Convert.ToString(Score);
    }
    public void ButtonStart()
    {
		startSolving.gameObject.SetActive(false);
		scrambleScript.CubeScramble();
        start = true;
	}
    private bool IsCubeSolved()
    {
        for(int i = 0; i < 9; i++)
        {
            if (cube.UP[i] != 'w') return false;
            if (cube.DOWN[i] != 'y') return false;
            if (cube.LEFT[i] != 'g') return false;
            if (cube.RIGHT[i] != 'b') return false;
            if (cube.FRONT[i] != 'r') return false;
            if (cube.BACK[i] != 'o') return false;
        }
        return true;
    }
    public void ResetColorsMark()
    {
		ChangeAllMaterials(black);
        HideAllButtons();
		cubes.Clear();
        start = false;
        Score = 0;
        startText.text = " ";
		lastClickedObject = null;
		startSolving.gameObject.SetActive(true);
		textScore.text = "";

	}
    void HandleClick(GameObject clickedObject)
    {
        if(reset)
        {
            ChangeAllMaterials(black);
            cubes.Clear();
            reset = false;
        }
        GameObject test = GameObject.Find("CubeBig");
        neigh = test.GetComponent<NeighborChecker>();
        string name = ToLowerFirstLetter(clickedObject.name);
        WallCheck(name);
        if (name == "greenCenter") name = "leftCenter";
        cubes = neigh.CheckNeighbors(name, wall);
        ChangeAllMaterials(purple);
        ButtonSetActive(name);

        lastClickedObject = clickedObject;
    }
    private void ButtonSetActive(string name)
    {
        switch(name)
        {
            case "frontCenter":
                FrontWallMoveArrows();
                break;
            case "rightCenter":
                RightWallMoveArrows();
                break;
			case "leftCenter":
				LeftWallMoveArrows();
				break;
			case "backCenter":
				BackWallMoveArrows();
				break;
			case "upCenter":
				UpWallMoveArrows();
				break;
			case "downCenter":
				DownWallMoveArrows();
				break;
		}
    }
    private void WallCheck(string center)
    {
        switch(center)
        {
            case "frontCenter":
                wall = "FRONT";
                break;
            case "rightCenter":
                wall = "RIGHT";
                break;
            case "backCenter":
                wall = "BACK";
                break;
            case "greenCenter":
                wall = "LEFT";
                break;
            case "upCenter":
                wall = "UP";
                break;
            case "downCenter":
                wall = "DOWN";
                break;

        }
    }
    void ChangeAllMaterials(Material mat)
    {
        foreach (GameObject obj in cubes)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = mat;
            }
        }
    }
    public static string ToLowerFirstLetter(string input)
    {
        if (string.IsNullOrEmpty(input) || char.IsLower(input[0]))
            return input;

        return char.ToLower(input[0]) + input.Substring(1);
    }
    private void FrontWallMoveArrows()
    {
        frontRight.gameObject.SetActive(true);
        frontLeft.gameObject.SetActive(true);
    }
    private void RightWallMoveArrows()
    {
        RightRight.gameObject.SetActive(true);
        RightLeft.gameObject.SetActive(true);
    }
	private void LeftWallMoveArrows()
	{
		LeftRight.gameObject.SetActive(true);
		LeftLeft.gameObject.SetActive(true);
	}
	private void BackWallMoveArrows()
	{
		backLeft.gameObject.SetActive(true);
		backRight.gameObject.SetActive(true);
	}
	private void UpWallMoveArrows()
	{
		upRight.gameObject.SetActive(true);
		upLeft.gameObject.SetActive(true);
	}
	private void DownWallMoveArrows()
	{
		downLeft.gameObject.SetActive(true);
		downRight.gameObject.SetActive(true);
	}
	private void HideAllButtons()
    {
        frontRight.gameObject.SetActive(false);
        frontLeft.gameObject.SetActive(false);
        RightRight.gameObject.SetActive(false);
        RightLeft.gameObject.SetActive(false);
        backRight.gameObject.SetActive(false);
        backLeft.gameObject.SetActive(false);
        LeftLeft.gameObject.SetActive(false);
        LeftRight.gameObject.SetActive(false);
        upRight.gameObject.SetActive(false);
        upLeft.gameObject.SetActive(false);
        downRight.gameObject.SetActive(false);
        downLeft.gameObject.SetActive(false);
    }
}
