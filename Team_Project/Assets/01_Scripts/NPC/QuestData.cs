// ����Ʈ�� ����
public enum QuestType
{
    Kill,
    Gathering,
    Conversation,
}

public class QuestData
{
    // ����Ʈ�� �з��ϰ� ������ Ȯ���ϱ� ���� �ʿ��� ������
    public QuestType questType;
    public string questName;
    public int questID;
    public int giverID;
    public string targetType;
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

    // ����Ʈ�� �߷��� �Ű������� ���� Ÿ��Ÿ�Կ� ���� óġ����, �������� �з�
    public QuestData(QuestJson qj)
    {
        switch (targetType)
        {
            case "M":
                questType = QuestType.Kill;
                this.questName = qj.questName;
                this.questID = qj.questID;
                this.giverID = qj.giverID;
                this.targetID = qj.targetID;
                this.requiredAmount = qj.requiredAmount;
                currentAmount = 0;
                this.goldReward = qj.goldReward;
                this.expReward = qj.expReward;
                isDone = false;
                break;
            case "I":
                questType = QuestType.Gathering;
                this.questName = qj.questName;
                this.questID = qj.questID;
                this.giverID = qj.giverID;
                this.targetID = qj.targetID;
                this.requiredAmount = qj.requiredAmount;
                currentAmount = 0;
                this.goldReward = qj.goldReward;
                this.expReward = qj.expReward;
                isDone = false;
                break;
            case "0":
                questType = QuestType.Conversation;
                this.questName = qj.questName;
                this.questID = qj.questID;
                this.giverID = qj.giverID;
                this.targetID = qj.targetID;
                this.goldReward = qj.goldReward;
                this.expReward = qj.expReward;
                isDone = false;
                break;
            default:
                break;
        }
    }
}
