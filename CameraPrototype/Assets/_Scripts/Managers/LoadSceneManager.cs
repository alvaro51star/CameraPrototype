using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneManager : MonoBehaviour
{
    [SerializeField] private List<SO_LevelInfo> levelInfos;
    
    
}

public enum Levels
{
    Menu,
    Lobby,
    Level1,
    Level2,
    Level3
}
