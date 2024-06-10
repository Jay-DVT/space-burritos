using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }

    public CookingStation[] cookingStations;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpgradeCookingSpeed(int stationIndex)
    {
        if (stationIndex >= 0 && stationIndex < cookingStations.Length)
        {
            cookingStations[stationIndex].UpgradeCookingSpeed();
            UpgradePanel.Instance.UpdateUpgradeCosts();
        }
    }

    public void UpgradeProductValue(int stationIndex)
    {
        if (stationIndex >= 0 && stationIndex < cookingStations.Length)
        {
            cookingStations[stationIndex].UpgradeProductValue();
            UpgradePanel.Instance.UpdateUpgradeCosts();
        }
    }
}
