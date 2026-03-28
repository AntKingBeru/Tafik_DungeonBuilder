[System.Serializable]
public struct DamageData
{
    public int amount;
    public DamageType type;
    
    public DamageData(int amount, DamageType type)
    {
        this.amount = amount;
        this.type = type;
    }
}