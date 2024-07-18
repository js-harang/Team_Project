// ����Ʈ�� ����
public enum QuestType
{
    Kill,
    Gathering,
    Conversation,
}

// ����Ʈ�� ��ǥ�� �ϴ� Ÿ���� ����
public enum TargetType
{
    Monster,
    Item,
}

public class QuestData
{
    // ����Ʈ�� �з��ϰ� ������ Ȯ���ϱ� ���� �ʿ��� ������
    public QuestType questType;
    public TargetType targetType;
    public string questName;
    public int questID;
    public int targetID;
    // ��ǥ��ġ
    public int requiredAmount;
    // ���� ��ġ
    public int currentAmount;
    public bool isDone;

    // ����Ʈ�� ������ �ٸ� NPC���� ��ȭ�� ���
    public QuestData(string questName, int questID, int targetID)
    {
        questType = QuestType.Conversation;
        this.questName = questName;
        this.questID = questID;
        this.targetID = targetID;
        isDone = false;
    }

    // ����Ʈ�� �߷��� �Ű������� ���� Ÿ��Ÿ�Կ� ���� óġ����, �������� �з�
    public QuestData(string questName, int questID, TargetType targetType, 
                    int targetID, int requiredAmount)
    {
        switch (targetType)
        {
            case TargetType.Monster:
                questType = QuestType.Kill;
                this.questName = questName;
                this.questID = questID;
                this.targetID = targetID;
                this.requiredAmount = requiredAmount;
                this.currentAmount = 0;
                isDone = false;
                break;
            case TargetType.Item:
                questType = QuestType.Gathering;
                this.questName = questName;
                this.questID = questID;
                this.targetID = targetID;
                this.requiredAmount = requiredAmount;
                this.currentAmount = 0;
                isDone = false;
                break;
            default:
                break;
        }
    }
}
