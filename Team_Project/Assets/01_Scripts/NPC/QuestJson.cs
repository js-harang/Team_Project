public class QuestJson
{
    public string questName;
    public int questID;
    public int giverID;
    public char targetType;
    public int targetID;
    public int requiredAmount;
    public int goldReward;
    public int expReward;
}

public class QuestJsons
{
    public QuestJson[] Items;
}
