using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelInfo", menuName = "SO/LevelInfoScriptableObject", order = 1)]
public class SO_LevelInfo : ScriptableObject
{
    public Sprite backgroundImage;

    public string levelName;
    [Space]
    [TextArea(5, 10)]
    public string levelDescription;
    [Space]
    public List<string> tipsForTheLevel;
}
