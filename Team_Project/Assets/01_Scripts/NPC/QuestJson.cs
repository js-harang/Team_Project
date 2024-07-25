[System.Serializable]

public class QuestJson
{
    public string quest_name;
    public int quest_id;
    public int giver_id;
    public char target_type;
    public int target_id;
    public int quest_goalline;
    public int gold_reward;
    public int exp_reward;
}

public class QuestJsons
{
    public QuestJson[] Items;
}
