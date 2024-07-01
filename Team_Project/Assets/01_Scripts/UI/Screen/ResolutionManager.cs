using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    public List<Resolution> resolutions;
    public TMP_Dropdown resolutionDropdown;

    void Start()
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

        // �ػ� �ɼ��� Dropdown�� �߰��ϱ�
        resolutionDropdown.ClearOptions();
        var options = new List<string>();
        foreach (var resolution in resolutions)
            options.Add(resolution.width + "x" + resolution.height);
        resolutionDropdown.AddOptions(options);

        // ���� �ػ� �����ϱ�
        var currentResolution = Screen.currentResolution;
        var currentResolutionIndex = resolutions.FindIndex(r => r.Equals(currentResolution));
        resolutionDropdown.value = currentResolutionIndex;
    }

    /// <summary>
    /// �ػ� ����
    /// </summary>
    public void SetResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
