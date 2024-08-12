using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLevelButtonBehaviour : MonoBehaviour
{
    private Button _button;
    [SerializeField] private Levels levelToLoad;
    

    private void Start()
    {
        _button = GetComponent<Button>();
        
        _button.onClick.AddListener(() => ChangeScene((int)levelToLoad));
    }

    private void ChangeScene(int level)
    {
        if (!ScenesManager.Instance)
        {
            Debug.LogError("No hay ScenesManager");
            return;
        }
        
        ScenesManager.Instance.ChangeSceneToLoad(level);
        ScenesManager.Instance.StartLoadScreen();
    }
}
