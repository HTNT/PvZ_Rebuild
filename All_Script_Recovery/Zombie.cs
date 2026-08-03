using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Zombie", menuName = "Game/Zombie")]
public class Zombie : ScriptableObject
{
    public string zombieName;
    public float attackPower;
    public float attackSpeed;
    public float health;
    public float movementSpeed;
    public Sprite zombieSprite;
}