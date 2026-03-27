public struct DamageData
{
    public int Amount;
    public DamageType Type;
    
    public DamageData(int amount, DamageType type)
    {
        Amount = amount;
        Type = type;
    }
}