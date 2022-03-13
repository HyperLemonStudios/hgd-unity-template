using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Loader : MonoBehaviour{
    [Header("Properties")]
    [SerializeField] public AudioMixer audioMixer;
    [SerializeField] bool forceLoad;

    [Header("Variables")]
    [SerializeField] bool loaded;
    void Load(){
        if(!loaded){
            SaveSerial.instance.Load();
            SaveSerial.instance.LoadSettings();
            loaded=true;
            if(forceLoad){LoadScene();}
        }
		
        Screen.fullScreen = SaveSerial.instance.settingsData.fullscreen;if(SaveSerial.instance.settingsData.fullscreen)Screen.SetResolution(Display.main.systemWidth,Display.main.systemHeight,true,60);
        QualitySettings.SetQualityLevel(SaveSerial.instance.settingsData.quality);
        audioMixer.SetFloat("MasterVolume", SaveSerial.instance.settingsData.masterVolume);
        audioMixer.SetFloat("SoundVolume", SaveSerial.instance.settingsData.soundVolume);
        audioMixer.SetFloat("MusicVolume", SaveSerial.instance.settingsData.musicVolume);
    }
    public void LoadScene(){
        GSceneManager.instance.LoadStartMenuLoader();
    }
    void Update(){
        Load();
    }
}
