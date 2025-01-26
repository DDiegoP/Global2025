using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField]
    private Slider _masterSlider, _musicSlider, _sfxSlider;
    [SerializeField]
    private FMOD.Studio.Bus _master, _music, _sfx;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float masterVolume, musicVolume, sfxVolume;
        _master = FMODUnity.RuntimeManager.GetBus("bus:/");
        _master.getVolume(out masterVolume);
        _music = FMODUnity.RuntimeManager.GetBus("bus:/Soundtrack");
        _music.getVolume(out musicVolume);
        _sfx = FMODUnity.RuntimeManager.GetBus("bus:/SFX");
        _sfx.getVolume(out sfxVolume);
        _masterSlider.SetValueWithoutNotify(masterVolume);
        _musicSlider.SetValueWithoutNotify(musicVolume);
        _sfxSlider.SetValueWithoutNotify(sfxVolume);
        _masterSlider.onValueChanged.AddListener(delegate { UpdateBus(_masterSlider, _master); });   
        _musicSlider.onValueChanged.AddListener(delegate { UpdateBus(_musicSlider, _music); });   
        _sfxSlider.onValueChanged.AddListener(delegate { UpdateBus(_sfxSlider, _sfx); });   
    }

    // Update is called once per frame
    private void UpdateBus(Slider slider, FMOD.Studio.Bus bus)
    {
        bus.setVolume(slider.value);
    }

    public void ToggleVisibility()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
