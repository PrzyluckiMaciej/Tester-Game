using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioMenuUI : BaseUI
{
    [SerializeField] private Button quitButton;
    [SerializeField] private Button controlsButton;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private BaseUI previousMenu;
    [SerializeField] private BaseUI controlsMenu;

    private MusicManager musicManager;
    private SoundManager soundManager;

    private void Awake() {
        musicManager = MusicManager.Instance;
        soundManager = SoundManager.Instance;
        // Pobranie zapisanych ustawieñ i ustawienie sliderów
        musicSlider.value = musicManager.GetVolume()*100;
        soundSlider.value = soundManager.GetVolume()*100;
        quitButton.onClick.AddListener(() => {
            Hide();
            previousMenu.Show();
        });
        controlsButton.onClick.AddListener(() => {
            Hide();
            controlsMenu.Show();
        });
        musicSlider.onValueChanged.AddListener(delegate { MusicVolumeChange(); });
        soundSlider.onValueChanged.AddListener(delegate { SoundsVolumeChanged(); });
    }

    private void MusicVolumeChange() {
        musicManager.ChangeVolume(musicSlider.value/100);
        Debug.Log("Music volume from menu: "+musicSlider.value);
    }

    private void SoundsVolumeChanged() {
        soundManager.ChangeVolume(soundSlider.value/100);
        Debug.Log("Sound volume from menu: " + soundSlider.value);
    }
}
