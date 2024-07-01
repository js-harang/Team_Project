using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    public List<Resolution> resolutions;
    public TMP_Dropdown resolutionDropdown;

    List<string> options;

    private void Start()
    {
        Debug.Log(name);

        resolutions = new List<Resolution>
        {
            new() { width = 1920, height = 1080 },
            new() { width = 1600, height = 1024 },
            new() { width = 1600, height = 900 },
            new() { width = 1440, height = 960 },
            new() { width = 1280, height = 720 },
            new() { width = 1024, height = 768 },
            new() { width = 800, height = 600 }
        };

        // 해상도 옵션을 Dropdown에 추가하기
        resolutionDropdown.ClearOptions();
        options = new List<string>();
        foreach (var resolution in resolutions)
            options.Add(resolution.width + "x" + resolution.height);
        resolutionDropdown.AddOptions(options);

        if (!PlayerPrefs.HasKey("ResolutionIndex"))
            PlayerPrefs.SetInt("ResolutionIndex", 1);

        // 현재 해상도 선택하기
        var resolutionIndex = PlayerPrefs.GetInt("ResolutionIndex");
        resolutionDropdown.value = resolutionIndex;
    }

    /// <summary>
    /// 해상도 설정
    /// </summary>
    public void SetResolution(int index)
    {
        Resolution resolution = resolutions[index];

        bool fullScreen;
        
        if(PlayerPrefs.GetInt("IsFullScreen") == 1)
            fullScreen = true;
        else
            fullScreen = false;

        Screen.SetResolution(resolution.width, resolution.height, fullScreen);

        SaveResolutionSettings();
    }

    private void SaveResolutionSettings()
    {
        PlayerPrefs.SetInt("ResolutionIndex", resolutionDropdown.value);
    }
}
