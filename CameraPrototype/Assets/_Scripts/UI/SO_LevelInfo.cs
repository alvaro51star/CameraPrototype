using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelInfo", menuName = "SO/LevelInfoScriptableObject", order = 1)]
public class SO_LevelInfo : ScriptableObject
{
    public Sprite backgroundImage;

    public string levelName;
    public string levelDescription;
}
