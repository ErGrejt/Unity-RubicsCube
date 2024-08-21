using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class PhoneCamera : MonoBehaviour
{
    public RawImage rawImage;
	private CubeColors cubeColors;
	public CubeData cubeData = CubeData.Instance;
    public Button button;
	public GameObject cube;
	public TMP_Text colorText;
    private WebCamTexture webCamTexture;
	public Transform[] colorSamplePoints;
	public Image[] colorsAlpha;
	public Sprite whiteSprite;
	public Sprite blueSprite;
	public Sprite greenSprite;
	public Sprite redSprite;
	public Sprite orangeSprite;
	public Sprite yellowSprite;
	private List<string> colorsButton = new List<string>(new string[9]);
	private int step = -1;
	//Center
	public Image center;
	public GameObject wholeCameraImage;
	//Panel camera
	public GameObject cameraPanel;
	public TMP_Text infoText;

	private void Start()
	{
		cameraPanel.gameObject.SetActive(false);
	}
	public void ToggleCamera()
    {
		GameObject test = GameObject.Find("CubeBig");
		cubeColors = test.GetComponent<CubeColors>();
		if (Permission.HasUserAuthorizedPermission(Permission.Camera))
		{
			if (webCamTexture == null || !webCamTexture.isPlaying)
			{
				StartingCamera();
			}
			else
			{
				StopCamera();
			}
		} else
		{
			var permissionCallbacks = new PermissionCallbacks();
			permissionCallbacks.PermissionGranted += PermissionGranted;
			permissionCallbacks.PermissionDenied += PermissionDenied;
			Permission.RequestUserPermission(Permission.Camera, permissionCallbacks);
		}
    }
	private void StartingCamera()
	{
		cameraPanel.gameObject.SetActive(true);
		StartCamera();
	}
	private void PermissionGranted(string permission)
	{
		StartingCamera();
	}

	private void PermissionDenied(string permission)
	{
		cubeColors.OnButtonClick();
	}
	private void StartCamera()
    {
		webCamTexture = new WebCamTexture();
		rawImage.texture = webCamTexture;
		rawImage.material.mainTexture = webCamTexture;
		webCamTexture.Play();
		StartCoroutine(UpdateColor());
	}
	public void ExitCameraPanel()
	{
		StopCamera();
		cameraPanel.gameObject.SetActive(false);
		cubeColors.ResetCube();
	}
	public void ChangeMethodInput()
	{
		ExitCameraPanel();
		cubeColors.OnButtonClick();
	}
	private void StopCamera()
	{
		if (webCamTexture != null)
		{
			webCamTexture.Stop();
			rawImage.texture = null;
		}
	}
	Vector2[] offsets = new Vector2[]
	{
		new Vector2(-0.5f, -0.5f), 
		new Vector2(-0.5f, 0f),    
		new Vector2(-0.5f, 0.5f),  
		new Vector2(0f, -0.5f),    
		new Vector2(0f, 0f),       
		new Vector2(0f, 0.5f),     
		new Vector2(0.5f, -0.5f),  
		new Vector2(0.5f, 0f),     
		new Vector2(0.5f, 0.5f)    
	};
	Color[] colors = new Color[9];
	//Getting color
	private IEnumerator UpdateColor()
	{
		while (webCamTexture.isPlaying)
		{
			yield return new WaitForEndOfFrame();
			if (webCamTexture.width > 100 && webCamTexture.height > 100)
			{
				for (int i = 0; i < offsets.Length; i++)
				{
					colors[i] = GetColorAtPosition(colorSamplePoints[i].position);
				}
				List<string> result = new List<string>();
				for(int i = 0; i < 9; i++)
				{
					result.Add(SimplifyColor(colors[i]));
				}
				//colorsButton = result;
				UpdateCubes(result);
			}
		}
	}
	public void NextWall()
	{
		if(IsImageNull())
		{
			return;
		}
		step++;
		
		
		for (int i = 0; i < colorsButton.Count; i++)
		{
			switch(step)
			{
				case 0:
					SavingColorsToCube("UP");
					break;
				case 1:
					SavingColorsToCube("DOWN");
					break;
				case 2:
					SavingColorsToCube("FRONT");
					break;
				case 3:
					SavingColorsToCube("RIGHT");
					break;
				case 4:
					SavingColorsToCube("BACK");
					break;
				case 5:
					SavingColorsToCube("LEFT");
					break;
			}
		}
	
		if (step == 0)
		{
			center.sprite = yellowSprite;
			infoText.text = "Trzymaj kostkê tak by czerwona œciana by³a skierowana do góry";
		}
		if (step == 1)
		{
			center.sprite = redSprite;
			infoText.text = "Trzymaj kostkê tak by bia³a œciana by³a skierowana do góry";
		}
		if (step == 2)
		{
			center.sprite = blueSprite;
		}
		if (step == 3)
		{
			center.sprite = orangeSprite;
		}
		if(step == 4)
		{
			center.sprite = greenSprite;
		}
		if(step >= 5)
		{
			center.sprite = whiteSprite;
			infoText.text = "Trzymaj kostkê tak by czerwona œciana by³a skierowana do Ciebie";
			StopCamera();
			cubeColors.CheckCubeColors();
			step = -1;
			cubeColors.UpdateColors();
			cameraPanel.gameObject.SetActive(false);
		}
	}
	private bool IsImageNull()
	{
		for(int i = 0; i < 9; i++)
		{
			if (colorsAlpha[i].sprite == null)
			{
				return true;
			}
		}
		return false;
	}
	private void SavingColorsToCube(string wall)
	{
		switch(wall)
		{
			case "UP":
				for(int i = 0; i < 9; i++)
				{
					if (i == 4)
					{
						cubeData.UP[i] = 'w';
						continue;
					}
					cubeData.UP[i] = Convert.ToChar(colorsButton[i]);
				}
				break;
			case "DOWN":
				for (int i = 0; i < 9; i++)
				{
					if (i == 4)
					{
						cubeData.DOWN[i] = 'y';
						continue;
					}
					cubeData.DOWN[i] = Convert.ToChar(colorsButton[i]);
				}
				break;
			case "LEFT":
				for (int i = 0; i < 9; i++)
				{
					if (i == 4)
					{
						cubeData.LEFT[i] = 'g';
						continue;
					}
					cubeData.LEFT[i] = Convert.ToChar(colorsButton[i]);
				}
				break;
			case "RIGHT":
				for (int i = 0; i < 9; i++)
				{
					if (i == 4)
					{
						cubeData.RIGHT[i] = 'b';
						continue;
					}
					cubeData.RIGHT[i] = Convert.ToChar(colorsButton[i]);
				}
				break;
			case "FRONT":
				for (int i = 0; i < 9; i++)
				{
					if (i == 4)
					{
						cubeData.FRONT[i] = 'r';
						continue;
					}
					cubeData.FRONT[i] = Convert.ToChar(colorsButton[i]);
				}
				break;
			case "BACK":
				for (int i = 0; i < 9; i++)
				{
					if (i == 4)
					{
						cubeData.BACK[i] = 'o';
						continue;
					}
					cubeData.BACK[i] = Convert.ToChar(colorsButton[i]);
				}
				break;
		}
	}
	private void UpdateCubes(List<string> strings)
	{
		for(int i = 0; i < strings.Count; i++)
		{
			switch (strings[i])
			{
				case "w":
					if(i != 4) colorsAlpha[i].sprite = whiteSprite;
					colorsButton[i] = "w";
					break;
				case "b":
					if (i != 4) colorsAlpha[i].sprite = blueSprite;
					colorsButton[i] = "b";
					break;
				case "g":
					if (i != 4) colorsAlpha[i].sprite = greenSprite;
					colorsButton[i] = "g";
					break;
				case "o":
					if (i != 4) colorsAlpha[i].sprite = orangeSprite;
					colorsButton[i] = "o";
					break;
				case "y":
					if (i != 4) colorsAlpha[i].sprite = yellowSprite;
					colorsButton[i] = "y";
					break;
				case "r":
					if (i != 4) colorsAlpha[i].sprite = redSprite;
					colorsButton[i] = "r";
					break;
			}
			if ((strings[i] == "w" || strings[i] == "b" || strings[i] == "g" || strings[i] == "o" || strings[i] == "y" || strings[i] == "r") && i != 4)
			{
				colorsAlpha[i].color = new Color(1f, 1f, 1f, 90f / 255f);
			}
		}
	}
	private void SeeResultTest(List<string> strings)
	{
		string result = "";
		for(int i = 0; i < strings.Count; i++)
		{
			if (i == 4) continue;
			result += strings[i];
		}
		colorText.text = result;
	}

	private Color GetColorAtPosition(Vector3 position)
	{
		if (webCamTexture.width > 0 && webCamTexture.height > 0)
		{
			Vector2 localPoint;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(
				rawImage.rectTransform,
				position,
				null, 
				out localPoint);

			int x = Mathf.Clamp((int)((localPoint.x / rawImage.rectTransform.rect.width + 0.5f) * webCamTexture.width), 0, webCamTexture.width - 1);
			int y = Mathf.Clamp((int)((localPoint.y / rawImage.rectTransform.rect.height + 0.5f) * webCamTexture.height), 0, webCamTexture.height - 1);

			Color[] pixels = webCamTexture.GetPixels(x, y, 1, 1);
			return pixels[0];
		}
		return Color.black;
	}
	private string SimplifyColor(Color color)
	{
		float r = color.r;
		float g = color.g;
		float b = color.b;

		if (r > 0.350f && g < 0.300f && b < 0.300f)
		{
			return "r";
			//return Color.red; 
		}
		else if (r > 0.450f && g < 0.520f && b < 0.500f)
		{
			return "o";
			//return new Color(255f, 0.162f, 0f)
		}
		else if (r > 0.300f && g > 0.550f && b < 0.500f)
		{
			return "y";
			//return Color.yellow; 
		}
		else if (r < 0.400f && g > 0.150f && b > 0.200f && b > g)
		{
			return "b";
			//return Color.blue; 
		}
		else if (r < 0.600f && g > 0.400f && b < 0.600f && g > b)
		{
			return "g";
			//return Color.green; 
		}
		else if (r > 0.500f && g > 0.500f && b > 0.500f)
		{
			return "w";
			//return Color.white; 
		}
		else
		{
			return $"(R: {r}, G: {g}, B: {b})";
			//return Color.gray;
		}
	}
}
