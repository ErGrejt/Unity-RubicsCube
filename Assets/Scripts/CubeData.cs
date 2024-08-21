using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeData
{
	private static CubeData _instance;
	private CubeData()
	{
		UP = new char[9];
		DOWN = new char[9];
		LEFT = new char[9];
		RIGHT = new char[9];
		FRONT = new char[9];
		BACK = new char[9];
		InitializeCube();
	}
	public static CubeData Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new CubeData();
			}
			return _instance;
		}
	}
	public char[] UP { get; set; } = new char[9];
	public char[] DOWN { get; set; } = new char[9];
	public char[] LEFT { get; set; } = new char[9];
	public char[] RIGHT { get; set; } = new char[9];
	public char[] FRONT { get; set; } = new char[9];
	public char[] BACK { get; set; } = new char[9];
	private void InitializeCube()
	{
		for (int i = 0; i < 9; i++)
		{
			UP[i] = 'w';      
			DOWN[i] = 'y';    
			LEFT[i] = 'g';    
			RIGHT[i] = 'b';   
			FRONT[i] = 'r';   
			BACK[i] = 'o';    
		}
	}
}
