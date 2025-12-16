using UnityEngine;
using UnityEngine.Audio;

public class AudioHandler
{
    private const float OffVolumeValue = -80;
    private const float OnVolumeValue = 0;
    private const float MinDifferenceValue = 0.01f;

    private const string KeyMusic = "VolumeMusic";
    private const string KeySFX = "VolumeSFX";
    private const string KeyNoiseBG = "VolumeNoiseBG";

    private AudioMixer _mixer;

    public AudioHandler(AudioMixer mixer)
    {
        _mixer = mixer;
    }

    public bool IsMusicOn() => IsVolumeOn(KeyMusic);
    public bool IsSFXOn() => IsVolumeOn(KeySFX);
    public bool IsNoiseBGOn() => IsVolumeOn(KeyNoiseBG);

    public void OffMusic() => OffVolume(KeyMusic);
    public void OnMusic() => OnVolume(KeyMusic);

    public void OffSFX() => OffVolume(KeySFX);
    public void OnSFX() => OnVolume(KeySFX);

    public void OffNoiseBG() => OffVolume(KeyNoiseBG);
    public void OnNoiseBG() => OnVolume(KeyNoiseBG);

    private bool IsVolumeOn(string key)
        => _mixer.GetFloat(key, out float  volume) && Mathf.Abs(volume - OnVolumeValue) <= MinDifferenceValue;

    private void OnVolume(string key) => _mixer.SetFloat(key, OnVolumeValue);
    private void OffVolume(string key) => _mixer.SetFloat(key, OffVolumeValue);

}
