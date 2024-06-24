using System.IO;
using UnityEngine;

/// <summary>
/// NPC 대화문
/// </summary>
public class Dialogue : MonoBehaviour
{
    readonly string filePath = "C:\\Users\\YONSAI\\Desktop\\Team_Project\\Team_Project\\Assets\\21_Data\\Dialogue.txt";

    public string ReadLineAtIndex(int index)
    {
        // 파일 존재 여부 확인
        if (!File.Exists(filePath))
        {
            Debug.LogError("파일을 찾을 수 없습니다. 파일경로 : " + filePath);
            return null;
        }

        string[] lines = File.ReadAllLines(filePath);

        // 인덱스 유효성 확인
        if (index < 0 || index >= lines.Length)
        {
            Debug.LogError("유효하지 않은 인덱스입니다. Index : " + index);
            return null;
        }

        return lines[index];
    }

    private void Start()
    {
        string line = ReadLineAtIndex(1);
        if (line != null)
            Debug.Log("읽어온 내용: " + line);
    }
}
