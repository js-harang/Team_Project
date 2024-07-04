using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionManager : MonoBehaviour
{
    [SerializeField] FullScreen fs;

    public List<Resolution> resolutions;
    public TMP_Dropdown resolutionDropdown;

    List<string> options;

    private void Start()
    {
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
            PlayerPrefs.SetInt("ResolutionIndex", 0);

        // 현재 해상도 선택하기
        var resolutionIndex = PlayerPrefs.GetInt("ResolutionIndex");
        resolutionDropdown.value = resolutionIndex;
    }

    /// <summary>
    /// 해상도 설정
    /// </summary>
    public void SetResolution(int index)
    {
        bool fullScreen;

        if (fs.toggles[0].GetComponent<Toggle>().isOn)
            fullScreen = true;
        else
            fullScreen = false;

        Resolution resolution = resolutions[index];

        Screen.SetResolution(resolution.width, resolution.height, fullScreen);
    }
}
