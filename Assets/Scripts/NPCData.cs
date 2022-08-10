using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC Data", menuName = "Scriptable Object/NPC Data", order = Int32.MaxValue)]
public class NPCData : ScriptableObject
{
    public float maxHeathPoint = 100.0f;
    public float attackPoint = 10.0f;
    public float speed = 10.0f;
    public float movementAccuracy = 0.001f;
    public String enemy;
}
