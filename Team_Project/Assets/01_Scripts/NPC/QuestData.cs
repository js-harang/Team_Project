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
    public int questId;
    public int targetId;
    // ��ǥ��ġ
    public int requiredAmount;
    // ���� ��ġ
    public int currentAmount;
    public bool isDone;

    // ����Ʈ�� ������ �ٸ� NPC���� ��ȭ�� ���
    public QuestData(string questName, int questId, int targetId)
    {
        questType = QuestType.Conversation;
        this.questName = questName;
        this.questId = questId;
        this.targetId = targetId;
        isDone = false;
    }

    // ����Ʈ�� �߷��� �Ű������� ���� Ÿ��Ÿ�Կ� ���� óġ����, �������� �з�
    public QuestData(string questName, int questId, TargetType targetType, 
                    int targetId, int requiredAmount, int currentAmount)
    {
        switch (targetType)
        {
            case TargetType.Monster:
                questType = QuestType.Kill;
                this.questName = questName;
                this.questId = questId;
                this.targetId = targetId;
                this.requiredAmount = requiredAmount;
                this.currentAmount = currentAmount;
                isDone = false;
                break;
            case TargetType.Item:
                questType = QuestType.Gathering;
                this.questName = questName;
                this.questId = questId;
                this.targetId = targetId;
                this.requiredAmount = requiredAmount;
                this.currentAmount = currentAmount;
                isDone = false;
                break;
            default:
                break;
        }
    }
}
