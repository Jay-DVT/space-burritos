using System.Collections.Generic;

[System.Serializable]
public class CookingStationData
{
    public string productName;
    public float cookingTime;
    public int productPrice;
    public int speedUpgradeLevel;
    public int valueUpgradeLevel;
}

[System.Serializable]
public class GameData
{
    public List<CookingStationData> cookingStations;
    public float remainingTime;
    public int money;
}