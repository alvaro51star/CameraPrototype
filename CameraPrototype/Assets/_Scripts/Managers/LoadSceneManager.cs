using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LoadSceneManager : MonoBehaviour
{
    [SerializeField] private float timeToChangeTip = 3f;
    [SerializeField] private List<SO_LevelInfo> levelInfos;
    
    [Space]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider loadingSlider;
    
    [Space]
    [SerializeField] private TextMeshProUGUI levelName;
    [SerializeField] private TextMeshProUGUI levelDescription;
    [SerializeField] private Image levelImage;

    [Space]
    [SerializeField] private GameObject loadingLevelText;
    [SerializeField] private GameObject loadingIcon;
    [SerializeField] private GameObject levelReadyText;
    [SerializeField] private DOTweenAnimation fillImage;

    [Space]
    [SerializeField] private TextMeshProUGUI tipText;
    [SerializeField] private DOTweenAnimation tipTextFade;
    
    [Space]
    [SerializeField] private List<string> tipList;
    private int _tipCount;

    private void Awake()
    {
        if (!ScenesManager.Instance)
        {
            Debug.LogError("No hay ScenesManager");
            return;
        }
        
        LoadLevel(ScenesManager.Instance.sceneToLoad);
    }

    public void LoadLevel(int levelToLoad)
    {
        SetUpLevelInfo(levelToLoad);
        loadingScreen.SetActive(true);

        StartCoroutine(LoadLevelASync(levelToLoad));
    }

    private void SetUpLevelInfo(int levelToLoad)
    {
        if (levelInfos.Count <= 0 || levelInfos.Count <= levelToLoad)
            return;

        SO_LevelInfo levelInfo = levelInfos[levelToLoad];

        levelName.text = levelInfo.levelName;
        levelDescription.text = levelInfo.levelDescription;
        levelImage.sprite = levelInfo.backgroundImage;
        tipList = levelInfo.tipsForTheLevel;
        
        loadingLevelText.SetActive(true);
        loadingIcon.SetActive(true);
        levelReadyText.SetActive(false);
        loadingSlider.gameObject.SetActive(true);

        StartCoroutine(GenerateTip());
    }

    IEnumerator LoadLevelASync(int levelToLoad)
    {
        yield return null;

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);
        loadOperation.allowSceneActivation = false;


        while (!loadOperation.isDone && loadOperation.progress < 0.9f)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = progressValue;
            
            yield return null;
        }

        //TODO hay que poner un sonido cuando se completa la carga
        
        loadingSlider.value = loadingSlider.maxValue;
        fillImage.DOPlay();
        
        loadingLevelText.SetActive(false);
        loadingIcon.SetActive(false);
        levelReadyText.SetActive(true);
        
        //loadingSlider.gameObject.SetActive(false);

        while (!Input.GetKey(KeyCode.Space))
        {
            yield return null;
        }

        loadOperation.allowSceneActivation = true;

        yield return null;
    }

    IEnumerator GenerateTip()
    {
        _tipCount = Random.Range(0, tipList.Count);
        tipText.text = tipList[_tipCount];

        yield return new WaitForEndOfFrame();
        
        while (loadingScreen.activeInHierarchy)
        {
            yield return new WaitForSecondsRealtime(timeToChangeTip);

            tipTextFade.DOPlayForward();
            yield return new WaitForSecondsRealtime(tipTextFade.duration);

            _tipCount++;
            if (_tipCount >= tipList.Count)
            {
                _tipCount = 0;
            }

            tipText.text = tipList[_tipCount];

            tipTextFade.DOPlayBackwards();

        }
        
        yield return null;
    }
}

public enum Levels
{
    Menu,
    Lobby,
    Level1
}