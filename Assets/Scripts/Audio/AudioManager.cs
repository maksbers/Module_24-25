using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource _audioSourceMusic;
    [SerializeField] private AudioSource _audioSourceSFX;
    [SerializeField] private AudioSource _audioSourceRun;

    [SerializeField] private AudioMixer _mixer;
    private AudioHandler _audioHandler;

    [SerializeField] private AudioClip _buttonClickSFX;
    [SerializeField] private float _buttonClickVolume = 1f;

    [SerializeField] private AudioClip _pointClickSFX;
    [SerializeField] private float _pointClickVolume = 1f;

    [SerializeField] private AudioClip _jumpSFX;
    [SerializeField] private float _jumpVolume = 1f;

    [SerializeField] private AudioClip _explosionSFX;
    [SerializeField] private float _explosionVolume = 1f;

    [SerializeField] private AudioClip _healCollectSFX;
    [SerializeField] private float _healCollectVolume = 1f;

    [SerializeField] private float _injuredRunPitchAmount = 0.4f;
    [SerializeField] private float _defeatTime = 1.5f;


    public bool IsMusicOn => _audioHandler.IsMusicOn();
    public bool IsSFXOn => _audioHandler.IsSFXOn();
    public bool IsNoiseBGOn => _audioHandler.IsNoiseBGOn();


    private void Awake()
    {
        Instance = this;

        _audioHandler = new AudioHandler(_mixer);
        _audioHandler.OnMusic();
        _audioHandler.OnNoiseBG();
    }


    public void ToggleMusicOn() => _audioHandler.OnMusic();
    public void ToggleMusicOff() => _audioHandler.OffMusic();

    public void ToggleRunSFX(bool isRunning, bool isInjured = false)
    {
        if (isRunning)
        {
            if (isInjured)
                _audioSourceRun.pitch = _injuredRunPitchAmount;
            else
                _audioSourceRun.pitch = 1.0f;

            if (_audioSourceRun.isPlaying != true)
                _audioSourceRun.Play();
        }
        else
        {
            if (_audioSourceRun.isPlaying)
                _audioSourceRun.Stop();
        }
    }

    public void PlayButtonClick() => PlaySfx(_buttonClickSFX, _buttonClickVolume);
    public void PlayPointClick() => PlaySfx(_pointClickSFX, _pointClickVolume);
    public void PlayJump() => PlaySfx(_jumpSFX, _jumpVolume);
    public void PlayExplosion() => PlaySfx(_explosionSFX, _explosionVolume);
    public void PlayHealCollect() => PlaySfx(_healCollectSFX, _healCollectVolume);

    public void SwitchToDefeatEffect() => _audioHandler.SwitchToDefeatEffect(_defeatTime);

    private void PlaySfx(AudioClip _clip, float volume = 1f) => _audioSourceSFX.PlayOneShot(_clip, volume);
}
