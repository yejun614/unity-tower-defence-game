using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower Data", menuName = "Scriptable Object/Tower Data", order = Int32.MaxValue)]
public class TowerData : ScriptableObject
{
    public int level;
    public float attackPoint;
    public float attackSpeed;
    public GameObject attackMotion;
    public String enemy;
}
