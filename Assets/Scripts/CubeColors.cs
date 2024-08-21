using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class CubeColors : MonoBehaviour
{
	private CubeData cube = CubeData.Instance;
	private ClickWall clk;
	private KociembaScript koc;
	private Solver solver;
	private Scramble scramble;
	public TMP_InputField input;
	public TMP_Text buttonText;
	public TMP_Text buttonMoves;
	public Button button;
	[Space(20)]
	public GameObject[] frontBlocks;
	public GameObject[] backBlocks;
	public GameObject[] upBlocks;
	public GameObject[] downBlocks;
	public GameObject[] leftBlocks;
	public GameObject[] rightBlocks;
	[Space(20)]
	public Material redMaterial;
	public Material blueMaterial;
	public Material greenMaterial;
	public Material yellowMaterial;
	public Material whiteMaterial;
	public Material orangeMaterial;
	//Reset
	private struct TransformData
	{
		public Vector3 initialPosition;
		public Quaternion initialRotation;
	}
	private static Dictionary<string, TransformData> initialTransforms = new Dictionary<string, TransformData>();
	public List<GameObject> names = new List<GameObject>();
	//Buttons Colors
	[Space(20)]
	public Button redButton;
	public Button blueButton;
	public Button greenButton;
	public Button orangeButton;
	public Button whiteButton;
	public Button yellowButton;
	//Image
	public Image displayImage;
	//Label
	public TMP_Text colorsText;
	//Photos
	public List<Sprite> imagesUP = new List<Sprite>();
	public List<Sprite> imagesDOWN = new List<Sprite>();
	public List<Sprite> imagesFRONT = new List<Sprite>();
	public List<Sprite> imagesBACK = new List<Sprite>();
	public List<Sprite> imagesRIGHT = new List<Sprite>();
	public List<Sprite> imagesLEFT = new List<Sprite>();
	//Char
	private char selectedColor;
	//Panel
	public GameObject colorsPanel;
	//Moves error
	public TMP_Text errorText;
	private int white, red, yellow, green, blue, orange;

	private void Start()
	{
		GameObject test = GameObject.Find("CubeBig");
		clk = test.GetComponent<ClickWall>();
		input.gameObject.SetActive(false);
		colorsPanel.gameObject.SetActive(false);
		foreach (Transform child in transform)
		{
			TransformData data = new TransformData();
			data.initialPosition = child.position;
			data.initialRotation = child.rotation;
			initialTransforms[child.name] = data; 
		}
	}
	public void ResetTransformByName(string objectName)
	{
		GameObject test = GameObject.Find("CubeBig");
		scramble = test.GetComponent<Scramble>();
		GameObject test2 = GameObject.Find("CubeBig");
		solver = test2.GetComponent<Solver>();
		GameObject test3 = GameObject.Find("CubeBig");
		koc = test3.GetComponent<KociembaScript>();
		MovingWalls.moves.Clear();
		koc.result.Clear();
		scramble.Scrambling = false;
		buttonMoves.text = "";
		

		if (initialTransforms.ContainsKey(objectName))
		{
			TransformData data = initialTransforms[objectName];
			Transform child = transform.Find(objectName); 
			if (child != null)
			{
				child.position = data.initialPosition;
				child.rotation = data.initialRotation;
			}
		}
		solver.ResetBools();
		ResetColors();
	}
	private void ResetColors()
	{
		for (int i = 0; i < 9; i++)
		{
			cube.UP[i] = 'w';
			cube.DOWN[i] = 'y';
			cube.LEFT[i] = 'g';
			cube.RIGHT[i] = 'b';
			cube.FRONT[i] = 'r';
			cube.BACK[i] = 'o';
		}
	}
	public void ResetCube()
	{
		clk.ResetColorsMark();
		for (int i = 0; i < names.Count; i++)
		{
			ResetTransformByName(names[i].name);
		}
	}
	public void OnButtonClick()
	{
		ResetCube();
		colorsPanel.gameObject.SetActive(true);
		redButton.onClick.AddListener(() => SetSelectedColor('r'));
		blueButton.onClick.AddListener(() => SetSelectedColor('b'));
		greenButton.onClick.AddListener(() => SetSelectedColor('g'));
		orangeButton.onClick.AddListener(() => SetSelectedColor('o'));
		whiteButton.onClick.AddListener(() => SetSelectedColor('w'));
		yellowButton.onClick.AddListener(() => SetSelectedColor('y'));
		StartCoroutine(InputColors());
	}
	private void SetSelectedColor(char color)
	{
		selectedColor = color;
	}
	IEnumerator InputColors()
	{
		buttonMoves.text = "";
		for(int i = 0; i < 9; i++)
		{
			if (i == 4) continue;
			colorsText.text = $"Up kolor klocka numer: {i+1}";
			displayImage.sprite = imagesUP[i];
			yield return new WaitUntil(() => selectedColor != '\0');

			cube.UP[i] = selectedColor;
			selectedColor = '\0';
			yield return new WaitForSeconds(0.1f);
		}
		for (int i = 0; i < 9; i++)
		{
			if (i == 4) continue;
			colorsText.text = $"Down kolor klocka numer: {i + 1}";
			displayImage.sprite = imagesDOWN[i];
			yield return new WaitUntil(() => selectedColor != '\0');
			cube.DOWN[i] = selectedColor;
			selectedColor = '\0';
			yield return new WaitForSeconds(0.1f);
		}
		for (int i = 0; i < 9; i++)
		{
			if (i == 4) continue;
			colorsText.text = $"Front kolor klocka numer: {i + 1}";
			displayImage.sprite = imagesFRONT[i];
			yield return new WaitUntil(() => selectedColor != '\0');
			cube.FRONT[i] = selectedColor;
			selectedColor = '\0';
			yield return new WaitForSeconds(0.1f);
		}
		for (int i = 0; i < 9; i++)
		{
			if (i == 4) continue;
			colorsText.text = $"Back kolor klocka numer: {i + 1}";
			displayImage.sprite = imagesBACK[i];
			yield return new WaitUntil(() => selectedColor != '\0');
			cube.BACK[i] = selectedColor;
			selectedColor = '\0';
			yield return new WaitForSeconds(0.1f);
		}
		for (int i = 0; i < 9; i++)
		{
			if (i == 4) continue;
			colorsText.text = $"Right kolor klocka numer: {i + 1}";
			displayImage.sprite = imagesRIGHT[i];
			yield return new WaitUntil(() => selectedColor != '\0');
			cube.RIGHT[i] = selectedColor;
			selectedColor = '\0';
			yield return new WaitForSeconds(0.1f);
		}
		for (int i = 0; i < 9; i++)
		{
			if (i == 4) continue;
			colorsText.text = $"Left kolor klocka numer: {i + 1}";
			displayImage.sprite = imagesLEFT[i];
			yield return new WaitUntil(() => selectedColor != '\0');
			cube.LEFT[i] = selectedColor;
			selectedColor = '\0';
			yield return new WaitForSeconds(0.1f);
		}
		CheckCubeColors();
	}
	public void CheckCubeColors()
	{
		for (int i = 0; i < 9; i++)
		{
			char color = cube.UP[i];
			CheckColorChar(color);
			color = cube.DOWN[i];
			CheckColorChar(color);
			color = cube.FRONT[i];
			CheckColorChar(color);
			color = cube.BACK[i];
			CheckColorChar(color);
			color = cube.RIGHT[i];
			CheckColorChar(color);
			color = cube.LEFT[i];
			CheckColorChar(color);
		}
		if (yellow != 9 || red != 9 | white != 9 || orange != 9 || blue != 9 || green != 9)
		{
			yellow = 0;
			red = 0;
			white = 0;
			green = 0;
			blue = 0;
			orange = 0;
			errorText.text = "B³êdnie wpisane kolory";
			colorsPanel.gameObject.SetActive(false);
			ResetCube();
		} else
		{
			colorsPanel.gameObject.SetActive(false);
			UpdateColors();
		}
	}
	public void ExitManualInput()
	{
		ResetCube();
		colorsPanel.gameObject.SetActive(false);
		button.gameObject.SetActive(true);
	}
	private void CheckColorChar(char color)
	{
		if (color == 'y')
		{
			yellow++;
		}
		else if (color == 'r')
		{
			red++;
		}
		else if (color == 'w')
		{
			white++;
		}
		else if (color == 'o')
		{
			orange++;
		}
		else if (color == 'b')
		{
			blue++;
		}
		else if (color == 'g')
		{
			green++;
		}
	}
	public void UpdateColors()
	{
		SetColors(frontBlocks, cube.FRONT);
		SetColors(backBlocks, cube.BACK);
		SetColors(upBlocks, cube.UP);
		SetColors(downBlocks, cube.DOWN);
		SetColors(leftBlocks, cube.LEFT);
		SetColors(rightBlocks, cube.RIGHT);
		button.gameObject.SetActive(true);
	}
	void SetColors(GameObject[] blocks, char[] colorArray)
	{
		for (int i = 0; i < blocks.Length; i++)
		{
			Renderer renderer = blocks[i].GetComponent<MeshRenderer>();
			renderer.material = CharToMaterial(colorArray[i]);
		}
	}
	Material CharToMaterial(char colorChar)
	{
		switch (colorChar)
		{
			case 'r': return redMaterial;
			case 'b': return blueMaterial;
			case 'g': return greenMaterial;
			case 'y': return yellowMaterial;
			case 'w': return whiteMaterial;
			case 'o': return orangeMaterial;
			default: return null;
		}
	}
}


