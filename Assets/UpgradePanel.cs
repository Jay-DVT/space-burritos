using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    public static UpgradePanel Instance { get; private set; }

    public Button[] speedUpgradeButtons;
    public Button[] valueUpgradeButtons;
    public TextMeshProUGUI[] speedUpgradeCosts;
    public TextMeshProUGUI[] valueUpgradeCosts;

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

    private void Start()
    {
        InitializeButtons();
    }

    public void InitializeButtons()
    {
        for (int i = 0; i < speedUpgradeButtons.Length; i++)
        {
            int index = i; // Capture the index for the lambda
            speedUpgradeButtons[i].onClick.AddListener(() => UpgradeManager.Instance.UpgradeCookingSpeed(index));
            valueUpgradeButtons[i].onClick.AddListener(() => UpgradeManager.Instance.UpgradeProductValue(index));
        }

        UpdateUpgradeCosts();
    }

    public void UpdateUpgradeCosts()
    {
        for (int i = 0; i < speedUpgradeButtons.Length; i++)
        {
            var station = UpgradeManager.Instance.cookingStations[i];
            int speedUpgradeCost = Mathf.CeilToInt(station.upgradeData.baseUpgradeCost * Mathf.Pow(station.upgradeData.upgradeCostMultiplier, station.speedUpgradeLevel));
            int valueUpgradeCost = Mathf.CeilToInt(station.upgradeData.baseUpgradeCost * Mathf.Pow(station.upgradeData.upgradeCostMultiplier, station.valueUpgradeLevel));

            speedUpgradeCosts[i].text = "" + speedUpgradeCost;
            valueUpgradeCosts[i].text = "" + valueUpgradeCost;
        }
    }
}
