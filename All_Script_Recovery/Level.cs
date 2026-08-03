using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : ScriptableObject
{
    public string levelName;
    public int rows;
    public int columns;
    public int initialSunPoints;
    public Plant[] availablePlants;
    public Wave[] waves;
}

[System.Serializable]
public class Wave
{
    public float startTime;
    public Zombie[] zombies;
}
