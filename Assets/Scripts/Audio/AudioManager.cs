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
    [SerializeField] private AudioClip _pointClickSFX;
    [SerializeField] private AudioClip _jumpSFX;
    [SerializeField] private AudioClip _explosionSFX;
    [SerializeField] private AudioClip _healCollectSFX;


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

    public void ToggleRunSFX(bool isRunning, float pitch = 1.0f)
    {
        if (isRunning)
        {
            _audioSourceRun.pitch = pitch;

            if (!_audioSourceRun.isPlaying)
                _audioSourceRun.Play();
        }
        else
        {
            if (_audioSourceRun.isPlaying)
                _audioSourceRun.Stop();
        }
    }

    public void PlayButtonClick() => PlaySfx(_buttonClickSFX);
    public void PlayPointClick() => PlaySfx(_pointClickSFX);
    public void PlayJump() => PlaySfx(_jumpSFX);
    public void PlayExplosion() => PlaySfx(_explosionSFX);
    public void PlayHealCollect() => PlaySfx(_healCollectSFX);

    private void PlaySfx(AudioClip _clip) => _audioSourceSFX.PlayOneShot(_clip);
}
