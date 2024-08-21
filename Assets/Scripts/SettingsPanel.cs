using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
	//ClickWall
	private ClickWall clk;
    public GameObject settingsScreen;
	public Button gearSettings;
	public Button closeSettingsButton;
	public Button funButton;
	public Button solveButton;
	public GameObject movingButtons;
	public GameObject solverButtons;
	//Instruction Panel
	public GameObject instructionPanel;
	public Button closingInstruction;
	public bool isFun = false;
	public TMP_Text record;
	private void Start()
	{
		GameObject test = GameObject.Find("CubeBig");
		clk = test.GetComponent<ClickWall>();
		settingsScreen.SetActive(false);
		closeSettingsButton.gameObject.SetActive(false);
		instructionPanel.gameObject.SetActive(false);
		UpdateScore();
		TurnFunMode();
	}
	public void UpdateScore()
	{
		int score = PlayerPrefs.GetInt("MinScore", int.MaxValue);
		if (score < 10000) record.text = $"Rekord min. iloœci ruchów do u³o¿enia kostki: {score}";
	}
	public void ShowSettings()
	{
		settingsScreen.SetActive(true);
		gearSettings.gameObject.SetActive(false);
		closeSettingsButton.gameObject.SetActive(true);
	}
	public void HideSettings()
	{
		settingsScreen.SetActive(false);
		gearSettings.gameObject.SetActive(true);
		closeSettingsButton.gameObject.SetActive(false);
	}
	public void TurnFunMode()
	{
		isFun = true;
		movingButtons.gameObject.SetActive(true);
		funButton.gameObject.SetActive(false);
		solveButton.gameObject.SetActive(true);
		solverButtons.gameObject.SetActive(false);
	}
	public void TurnSolveMode()
	{
		isFun = false;
		movingButtons.gameObject.SetActive(false);
		funButton.gameObject.SetActive(true);
		solveButton.gameObject.SetActive(false);
		solverButtons.gameObject.SetActive(true);
		clk.ResetColorsMark();
	}
	//Instructions panel
	public void OpenInstruction()
	{
		instructionPanel.SetActive(true);
	}
	public void CloseInstruction()
	{
		instructionPanel.SetActive(false);
	}
}
