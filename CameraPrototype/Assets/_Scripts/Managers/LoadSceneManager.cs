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
    [SerializeField] private List<SO_LevelInfo> levelInfos;

    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;

    [SerializeField] private Slider loadingSlider;

    [SerializeField] private TextMeshProUGUI levelName;
    [SerializeField] private TextMeshProUGUI levelDescription;
    [SerializeField] private Image levelImage;

    [SerializeField] private GameObject loadingLevelText;
    [SerializeField] private GameObject loadingIcon;
    [SerializeField] private GameObject levelReadyText;
    [SerializeField] private DOTweenAnimation fillImage;

    [SerializeField] private TextMeshProUGUI tipText;
    [SerializeField] private DOTweenAnimation tipTextFade;
    
    [SerializeField] private List<string> tipList;
    private int _tipCount;
    
    


    public void LoadLevel(int levelToLoad)
    {
        SetUpLevelInfo(levelToLoad);
        //mainMenu.SetActive(false);
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
            yield return new WaitForSeconds(3f);

            tipTextFade.DOPlayForward();
            yield return new WaitForSeconds(tipTextFade.duration);

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
    Level1,
    Lobby
}