using UnityEngine;
using UnityEngine.UIElements;

public class MainUIScripts : MonoBehaviour
{
    private Label healthLabel;
    private Label pointLabel;
    private Label waveLabel;
    private Label enemiesLeftLabel;

    private HealthComponent playerHealth;
    private CombatManager combatManager;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        var leftVisual = root.Q<VisualElement>("Kiri");
        healthLabel = leftVisual.Q<Label>("Health");
        pointLabel = leftVisual.Q<Label>("Point");

        var rightVisual = root.Q<VisualElement>("Kanan");
        waveLabel = rightVisual.Q<Label>("Wave");
        enemiesLeftLabel = rightVisual.Q<Label>("Enemy");

        QueryComponents();
    }

    void Update()
    {
        if (playerHealth == null || combatManager == null)
        {
            QueryComponents();
        }

        healthLabel.text = $"Health: {(playerHealth != null ? playerHealth.Health : 0)}";
        pointLabel.text = $"Point: {(combatManager != null ? combatManager.point : 0)}";
        waveLabel.text = $"Wave: {(combatManager != null ? combatManager.waveNumber : 0)}";
        enemiesLeftLabel.text = $"Enemies Left: {(combatManager != null ? combatManager.totalEnemies : 0)}";
    }

    private void QueryComponents()
    {
        var player = GameObject.Find("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<HealthComponent>();
        }

        var combatManagerObject = GameObject.Find("CombatManager");
        if (combatManagerObject != null)
        {
            combatManager = combatManagerObject.GetComponent<CombatManager>();
        }
    }
}