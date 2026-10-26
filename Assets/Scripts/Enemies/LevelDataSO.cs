using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "New Level Data")]
[System.Serializable]
public class LevelDataSO : ScriptableObject
{
    public string _levelName = "Level ";
    public int _scene = 1;
    public List<EnemyWave> _enemySpawns;
}
