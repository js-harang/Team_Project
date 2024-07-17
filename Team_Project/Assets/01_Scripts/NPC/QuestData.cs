// 퀘스트의 종류
public enum QuestType
{
    Kill,
    Gathering,
    Conversation,
}

// 퀘스트가 목표로 하는 타겟의 종류
public enum TargetType
{
    Monster,
    Item,
}

public class QuestData
{
    // 퀘스트를 분류하고 진행을 확인하기 위해 필요한 변수들
    public QuestType questType;
    public TargetType targetType;
    public string questName;
    public int questId;
    public int targetId;
    // 묙표수치
    public int requiredAmount;
    // 현재 수치
    public int currentAmount;
    public bool isDone;

    // 퀘스트의 종류가 다른 NPC와의 대화인 경우
    public QuestData(string questName, int questId, int targetId)
    {
        questType = QuestType.Conversation;
        this.questName = questName;
        this.questId = questId;
        this.targetId = targetId;
        isDone = false;
    }

    // 퀘스트의 중류가 매개변수로 받은 타겟타입에 따라 처치인지, 수집인지 분류
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
