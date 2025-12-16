using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private AgentCharacter _character;
    [SerializeField] private HealSpawner _healSpawner;

    [SerializeField] private Image _toggleSpawnHealButton;
    [SerializeField] private Image _toggleMusicButton;

    [SerializeField] private Color _activeButtonColor = Color.white;
    [SerializeField] private Color _inactiveButtonColor = Color.gray;

    private AudioManager _audioManager;

    private float _maxHealth;

    private void Start()
    {
        _audioManager = AudioManager.Instance;

        if (_character != null)
            _maxHealth = _character.Health;

        if (_healSpawner != null)
            UpdateButtonColor(_toggleSpawnHealButton, _healSpawner.IsEnableHealSpawn);
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

        _audioManager.PlayButtonClick();

        if (_healSpawner.IsEnableHealSpawn)
        {
            _healSpawner.SetHealSpawnDisable();
            UpdateButtonColor(_toggleSpawnHealButton, false);
        }
        else
        {
            _healSpawner.SetHealSpawnEnable();
            UpdateButtonColor(_toggleSpawnHealButton, true);
        }
    }

    public void ToggleMusicPlay()
    {
        if (_audioManager == null)
            return;

        _audioManager.PlayButtonClick();

        if (_audioManager.IsMusicOn)
        {
            _audioManager.ToggleMusicOff();
            UpdateButtonColor(_toggleMusicButton, false);
        }
        else
        {
            _audioManager.ToggleMusicOn();
            UpdateButtonColor(_toggleMusicButton, true);
        }
    }

    private void UpdateButtonColor(Image button, bool isActive)
    {
        if (isActive)
            button.color = _activeButtonColor;
        else
            button.color = _inactiveButtonColor;
    }
}

