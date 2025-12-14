using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private AgentCharacter _character;
    [SerializeField] private HealSpawner _healSpawner;

    [SerializeField] private Image _spawnButtonImage;

    [SerializeField] private Color _activeButtonColor = Color.white;
    [SerializeField] private Color _inactiveButtonColor = Color.gray;

    private float _maxHealth;

    private void Start()
    {
        if (_character != null)
            _maxHealth = _character.Health;

        if (_healSpawner != null)
            UpdateSpawnButtonColor(_healSpawner.IsEnableHealSpawn);
    }

    private void Update()
    {
        CalculateHealthBar();
    }

    private void CalculateHealthBar()
    {
        if (_character == null)
            return;

        if (_maxHealth > 0)
            _healthBar.fillAmount = _character.Health / _maxHealth;
        else
            _healthBar.fillAmount = 0;
    }

    public void ToggleHealSpawn()
    {
        if (_healSpawner == null)
            return;

        if (_healSpawner.IsEnableHealSpawn)
        {
            _healSpawner.SetHealSpawnDisable();
            UpdateSpawnButtonColor(false);
        }
        else
        {
            _healSpawner.SetHealSpawnEnable();
            UpdateSpawnButtonColor(true);
        }
    }

    private void UpdateSpawnButtonColor(bool isActive)
    {
        if (_spawnButtonImage == null)
            return;

        if (isActive)
            _spawnButtonImage.color = _activeButtonColor;
        else
            _spawnButtonImage.color = _inactiveButtonColor;
    }
}

