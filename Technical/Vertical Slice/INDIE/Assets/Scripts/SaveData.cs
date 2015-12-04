using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SaveData
{

	public static SaveData current;

	public int money;
	public int exp;
	public bool silencer;
	public int smoke;
	public int mine;
    public float bulletDamage;

	public int currentLevel;
}