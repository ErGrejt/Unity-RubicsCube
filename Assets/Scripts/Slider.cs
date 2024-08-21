using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slider : MonoBehaviour
{
	public UnityEngine.UI.Slider slider;
	public TMP_Text speedLabel;
	private Scramble scramble;
	private MovingWalls walls;
	private RotatingSides rotate;
	private KociembaScript koc;
	void Start()
	{
		slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
		speedLabel.text = "Prêdkoœæ: 1";
	}
	void OnSliderValueChanged()
	{
		GameObject test = GameObject.Find("CubeBig");
		scramble = test.GetComponent<Scramble>();
		GameObject test2 = GameObject.Find("CubeBig");
		walls = test2.GetComponent<MovingWalls>();
		GameObject test3 = GameObject.Find("CubeBig");
		rotate = test3.GetComponent<RotatingSides>();
		GameObject test4 = GameObject.Find("CubeBig");
		koc = test4.GetComponent<KociembaScript>();
		float sliderValue = slider.value;
		HandleSliderValue(sliderValue);
	}
	void HandleSliderValue(float value)
	{
		switch (value)
		{
			case 0:
				speedLabel.text = "Prêdkoœæ: 1";
				scramble.rotatingSpeed = 0.6f;
				walls.rotatingSpeed = 0.6f;
				koc.rotatingSpeed = 0.6f;
				rotate.rotationSpeed = 420f;
				break;
			case 1:
				speedLabel.text = "Prêdkoœæ: 2";
				scramble.rotatingSpeed = 0.50f;
				walls.rotatingSpeed = 0.45f;
				koc.rotatingSpeed = 0.50f;
				rotate.rotationSpeed = 520f;
				break;
			case 2:
				speedLabel.text = "Prêdkoœæ: 3";
				scramble.rotatingSpeed = 0.33f;
				walls.rotatingSpeed = 0.30f;
				koc.rotatingSpeed = 0.33f;
				rotate.rotationSpeed = 600f;
				break;
			case 3:
				speedLabel.text = "Prêdkoœæ: 4";
				scramble.rotatingSpeed = 0.31f;
				walls.rotatingSpeed = 0.24f;
				koc.rotatingSpeed = 0.31f;
				rotate.rotationSpeed = 800f;
				break;

			case 4:
				speedLabel.text = "Prêdkoœæ: 5";
				scramble.rotatingSpeed = 0.27f;
				walls.rotatingSpeed = 0.20f;
				koc.rotatingSpeed = 0.27f;
				rotate.rotationSpeed = 1020f;
				break;
		}
	}
}
