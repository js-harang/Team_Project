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
    public char targetType;
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
            if (currentAmount == requiredAmount)
                isDone = true;
            currentAmount = value;
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
            case 'M':
                questType = QuestType.Kill;
                this.questName = qj.quest_name;
                this.questID = qj.quest_id;
                this.giverID = qj.giver_id;
                this.targetID = qj.target_id;
                this.requiredAmount = qj.quest_goalline;
                currentAmount = 0;
                this.goldReward = qj.gold_reward;
                this.expReward = qj.exp_reward;
                isDone = false;
                break;
            case 'I':
                questType = QuestType.Gathering;
                this.questName = qj.quest_name;
                this.questID = qj.quest_id;
                this.giverID = qj.giver_id;
                this.targetID = qj.target_id;
                this.requiredAmount = qj.quest_goalline;
                currentAmount = 0;
                this.goldReward = qj.gold_reward;
                this.expReward = qj.exp_reward;
                isDone = false;
                break;
            case '0':
                questType = QuestType.Conversation;
                this.questName = qj.quest_name;
                this.questID = qj.quest_id;
                this.giverID = qj.giver_id;
                this.targetID = qj.target_id;
                this.goldReward = qj.gold_reward;
                this.expReward = qj.exp_reward;
                isDone = false;
                break;
            default:
                break;
        }
    }
}
