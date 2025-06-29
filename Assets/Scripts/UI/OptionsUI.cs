using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsUI : MonoBehaviour
{
    public Slider volumeSlider;
    public Toggle muteToggle;

    private void Start()
    {
        // Carrega o volume salvo ou usa o padrão
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
        volumeSlider.value = savedVolume;
        muteToggle.isOn = savedVolume == 0f;

        // Aplica o volume ao AudioManager
        SetVolume(savedVolume);

        // Listeners
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        muteToggle.onValueChanged.AddListener(OnMuteToggled);
    }

    public void OnVolumeChanged(float value)
    {
        SetVolume(value);
        muteToggle.isOn = value == 0f;
        PlayerPrefs.SetFloat("Volume", value);
    }

    public void OnMuteToggled(bool isMuted)
    {
        float value = isMuted ? 0f : (volumeSlider.value == 0f ? 1f : volumeSlider.value);
        volumeSlider.value = value;
        SetVolume(value);
        PlayerPrefs.SetFloat("Volume", value);
    }

    private void SetVolume(float value)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetVolume(value);
        }
        AudioListener.volume = value; // Garante que o volume global é ajustado
    }

    public void VoltarParaMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
