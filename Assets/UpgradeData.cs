using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgradeData", menuName = "Upgrade Data", order = 51)]
public class UpgradeData : ScriptableObject
{
    public string productName;
    public float baseCookingTime;
    public float cookingTimeUpgrade;
    public int baseProductPrice;
    public int productValueUpgrade;
    public int baseUpgradeCost;
    public float upgradeCostMultiplier;
    public GameObject foodPrefab;
}
