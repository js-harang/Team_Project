// 퀘스트의 종류
public enum QuestType
{
    Kill,
    Gathering,
    Conversation,
}

public class QuestData
{
    // 퀘스트를 분류하고 진행을 확인하기 위해 필요한 변수들
    public QuestType questType;
    public string questName;
    public int questID;
    public int giverID;
    public string targetType;
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

    // 퀘스트의 중류가 매개변수로 받은 타겟타입에 따라 처치인지, 수집인지 분류
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
