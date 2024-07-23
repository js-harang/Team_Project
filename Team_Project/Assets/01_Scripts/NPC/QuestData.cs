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
    public int giverID;
    public int targetID;
    // ��ǥ��ġ
    public int requiredAmount;
    // ���� ��ġ
    int currentAmount;
    public int CurrentAmount
    {
        get
        { return currentAmount; }
        set 
        {
            currentAmount = value;
            if (currentAmount == requiredAmount)
                isDone = true;
        }
    }
    // �޼� ����
    public int goldReward;
    public int expReward;
    // ���� ���� Ȯ��
    public bool isDone;

    // ����Ʈ�� ������ �ٸ� NPC���� ��ȭ�� ���
    public QuestData(string questName, int questID, int giverID,
        int targetID, int goldReward, int expReward)
    {
        questType = QuestType.Conversation;
        this.questName = questName;
        this.questID = questID;
        this.giverID = giverID;
        this.targetID = targetID;
        this.goldReward = goldReward;
        this.expReward = expReward;
        isDone = false;
    }

    // ����Ʈ�� �߷��� �Ű������� ���� Ÿ��Ÿ�Կ� ���� óġ����, �������� �з�
    public QuestData(string questName, int questID, int giverID, TargetType targetType, 
        int targetID, int requiredAmount, int goldReward, int expReward)
    {
        switch (targetType)
        {
            case TargetType.Monster:
                questType = QuestType.Kill;
                this.questName = questName;
                this.questID = questID;
                this.giverID = giverID;
                this.targetID = targetID;
                this.requiredAmount = requiredAmount;
                currentAmount = 0;
                this.goldReward = goldReward;
                this.expReward = expReward;
                isDone = false;
                break;
            case TargetType.Item:
                questType = QuestType.Gathering;
                this.questName = questName;
                this.questID = questID;
                this.giverID = giverID;
                this.targetID = targetID;
                this.requiredAmount = requiredAmount;
                currentAmount = 0;
                this.goldReward = goldReward;
                this.expReward = expReward;
                isDone = false;
                break;
            default:
                break;
        }
    }
}
