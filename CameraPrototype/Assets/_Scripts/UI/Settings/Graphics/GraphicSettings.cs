using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GraphicSettings : MonoBehaviour
{
    [FormerlySerializedAs("resolutionDropdown")] [SerializeField] private TMP_Dropdown m_TMPDrop_resolutionDropdown;
    [FormerlySerializedAs("fpsDropdown")] [SerializeField] private TMP_Dropdown m_TMPDrop_fpsDropdown;
    
    [FormerlySerializedAs("fpsValues")] [SerializeField] private int[] m_intA_fpsValues = new []{30, 60, 120, 144, 165, 240};
    


    private int m_currentRefreshRate;
    private int m_currentResolutionIndex;
    private int m_currentFPSIndex;

    private List<Resolution> m_resL_filteredResolutions;

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
        SetResolutionOptions(m_TMPDrop_resolutionDropdown);
        SetFPSOptions(m_TMPDrop_fpsDropdown);
    }

    public void SetResolutionOptions(TMP_Dropdown dropdown)
    {
        List<Resolution> resolutions = Screen.resolutions.ToList();
        m_resL_filteredResolutions = new List<Resolution>();

        m_TMPDrop_resolutionDropdown.ClearOptions();
        m_currentRefreshRate = Screen.currentResolution.refreshRate;

        List<TMP_Dropdown.OptionData> optionDatas;

        foreach (var resolution in resolutions)
        {
            if (resolution.refreshRate == m_currentRefreshRate)
            {
                m_resL_filteredResolutions.Add(resolution);
            }
        }

        List<string> options = new List<string>();
        for (int i = 0; i < m_resL_filteredResolutions.Count; i++)
        {
            string resolutionOption = $"{m_resL_filteredResolutions[i].width}x{m_resL_filteredResolutions[i].height}";
            options.Add(resolutionOption);
            if (m_resL_filteredResolutions[i].width == Screen.width && m_resL_filteredResolutions[i].height == Screen.height)
            {
                m_currentResolutionIndex = i;
            }
        }

        m_TMPDrop_resolutionDropdown.AddOptions(options);
        m_TMPDrop_resolutionDropdown.value = m_currentResolutionIndex;
        m_TMPDrop_resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int index)
    {
        Resolution resolution = m_resL_filteredResolutions[index];

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
    
    private void SetFPSOptions(TMP_Dropdown dropdown)
    {
        int index = -1;
        for (int i = 0; i < m_intA_fpsValues.Length; i++)
        {
            if (Screen.currentResolution.refreshRate == m_intA_fpsValues[i])
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

    public void ToggleVsync(bool value)
    {
        if (value)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
    }
}