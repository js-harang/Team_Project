using System.IO;
using UnityEngine;

/// <summary>
/// NPC ��ȭ��
/// </summary>
public class Dialogue : MonoBehaviour
{
    readonly string filePath = "C:\\Users\\YONSAI\\Desktop\\Team_Project\\Team_Project\\Assets\\21_Data\\Dialogue.txt";

    public string ReadLineAtIndex(int index)
    {
        // ���� ���� ���� Ȯ��
        if (!File.Exists(filePath))
        {
            Debug.LogError("������ ã�� �� �����ϴ�. ���ϰ�� : " + filePath);
            return null;
        }

        string[] lines = File.ReadAllLines(filePath);

        // �ε��� ��ȿ�� Ȯ��
        if (index < 0 || index >= lines.Length)
        {
            Debug.LogError("��ȿ���� ���� �ε����Դϴ�. Index : " + index);
            return null;
        }

        return lines[index];
    }

    private void Start()
    {
        string line = ReadLineAtIndex(1);
        if (line != null)
            Debug.Log("�о�� ����: " + line);
    }
}
