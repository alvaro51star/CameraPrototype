using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GraphicSettings : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown fpsDropdown;
    
    [SerializeField] private int[] fpsValues = new []{30, 60, 120, 144, 165, 240};
    


    private int currentRefreshRate;
    private int currentResolutionIndex;
    private int currentFPSIndex;

    private List<Resolution> filteredResolutions;

    // Start is called before the first frame update
    void Start()
    {
        SetOptions();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeQuality(int index) //Graficos dropdown
    {
        QualitySettings.SetQualityLevel(index);
    }

    private void SetOptions()
    {
        SetResolutionOptions(resolutionDropdown);
        SetFPSOptions(fpsDropdown);
    }

    public void SetResolutionOptions(TMP_Dropdown dropdown)
    {
        List<Resolution> resolutions = Screen.resolutions.ToList();
        filteredResolutions = new List<Resolution>();

        resolutionDropdown.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRate;

        List<TMP_Dropdown.OptionData> optionDatas;

        foreach (var resolution in resolutions)
        {
            if (resolution.refreshRate == currentRefreshRate)
            {
                filteredResolutions.Add(resolution);
            }
        }

        List<string> options = new List<string>();
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOption = $"{filteredResolutions[i].width}x{filteredResolutions[i].height}";
            options.Add(resolutionOption);
            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int index)
    {
        Resolution resolution = filteredResolutions[index];

        if (PlayerPrefs.HasKey("FullScreen Mode"))
        {
            int screenModeIndex = PlayerPrefs.GetInt("FullScreen Mode");
            switch (screenModeIndex)
            {
                case 0:
                    Screen.SetResolution(resolution.width, resolution.height, FullScreenMode.ExclusiveFullScreen);
                    break;
                case 1:
                    Screen.SetResolution(resolution.width, resolution.height, FullScreenMode.FullScreenWindow);
                    break;
                case 2:
                    Screen.SetResolution(resolution.width, resolution.height, FullScreenMode.Windowed);
                    break;
                default:
                    Debug.LogError($"Index out of bounds {screenModeIndex}");
                    break;
            }
        }
        else
        {
            Screen.SetResolution(resolution.width, resolution.height, FullScreenMode.ExclusiveFullScreen);
        }
    }

    //TODO hacer esta funcion para que ponga los fps a los que va tu monitor
    private void SetFPSOptions(TMP_Dropdown dropdown)
    {
        int index = -1;
        for (int i = 0; i < fpsValues.Length; i++)
        {
            if (Screen.currentResolution.refreshRate == fpsValues[i])
            {
                index = i;
            }
        }

        if (index >= 0 && index < dropdown.options.Count )
        {
            dropdown.value = index;
            dropdown.RefreshShownValue();
        }
    }

    public void SetScreenFrames(int index)
    {
        switch (index)
        {
            case 0:
                Application.targetFrameRate = 30;
                break;
            case 1:
                Application.targetFrameRate = 60;
                break;
            case 2:
                Application.targetFrameRate = 120;
                break;
            case 3:
                Application.targetFrameRate = 144;
                break;
            case 4:
                Application.targetFrameRate = 165;
                break;
            case 5:
                Application.targetFrameRate = 240;
                break;
            
        }
    }

    public void SetScreenMode(int index)
    {
        switch (index)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
            default:
                Debug.LogError($"Index out of bounds {index}");
                break;
        }

        PlayerPrefs.SetInt("FullScreen Mode", index);
    }
}