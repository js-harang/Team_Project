[System.Serializable]

// 모든 퀘스트들을 제이슨으로 가져오는 클래스
public class QuestJson
{
    public string quest_name;
    public int quest_id;
    public int giver_id;
    public string target_type;
    public int target_id;
    public int quest_goalline;
    public int gold_reward;
    public int exp_reward;
}

public class QuestJsons
{
    public QuestJson[] Items;
}

// 플레이어 캐릭터가 갖고 있는 퀘스트를 가져오는 클래스
public class QuestsCheck
{
    public int quest_id;
    public int current;
    public string isdone;
}

public class QuestsChecks
{
    public QuestsCheck[] Items;
}
