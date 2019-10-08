using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/LevelData", order = 1)]
public class Level : ScriptableObject
{
    public List<GameObject> LevelsList = new List<GameObject>();
}
