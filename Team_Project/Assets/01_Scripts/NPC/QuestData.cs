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
    public char targetType;
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
            if (currentAmount == requiredAmount)
                isDone = true;
            currentAmount = value;
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
