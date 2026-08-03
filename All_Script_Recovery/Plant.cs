using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Plant", menuName = "Game/Plant")]
public class Plant : ScriptableObject
{
    public string plantName;
    public int cost;
    public float attackPower;
    public float attackSpeed;
    public float health;
    public Sprite plantSprite;
    public AttackTypes attackTypes;
}
