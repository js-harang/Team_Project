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
    public int questID;
    public int giverID;
    public int targetID;
    // 묙표수치
    public int requiredAmount;
    // 현재 수치
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
    // 달성 보상
    public int goldReward;
    public int expReward;
    // 기준 충족 확인
    public bool isDone;

    // 퀘스트의 종류가 다른 NPC와의 대화인 경우
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

    // 퀘스트의 중류가 매개변수로 받은 타겟타입에 따라 처치인지, 수집인지 분류
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
