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


    private int currentRefreshRate;
    private int currentResolutionIndex;

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
        
        Screen.SetResolution(resolution.width, resolution.height, FullScreenMode.ExclusiveFullScreen);
    }

    public void SetScreenFrames()
    {
    }

    public void SetScreenMode(int index)
    {
        
    }
}