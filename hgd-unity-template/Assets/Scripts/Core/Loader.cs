using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Loader : MonoBehaviour{
    [SerializeField] public AudioMixer audioMixer;
    [SerializeField] bool loaded;
    [SerializeField] bool forceLoad;
    private void Load(){
        if(!loaded){
            SaveSerial.instance.Load();
            SaveSerial.instance.LoadSettings();
            loaded=true;
        }
        Screen.fullScreen = SaveSerial.instance.settingsData.fullscreen;if(SaveSerial.instance.settingsData.fullscreen)Screen.SetResolution(Display.main.systemWidth,Display.main.systemHeight,true,60);
        QualitySettings.SetQualityLevel(SaveSerial.instance.settingsData.quality);
        audioMixer.SetFloat("MasterVolume", SaveSerial.instance.settingsData.masterVolume);
        audioMixer.SetFloat("SoundVolume", SaveSerial.instance.settingsData.soundVolume);
        audioMixer.SetFloat("MusicVolume", SaveSerial.instance.settingsData.musicVolume);
    }
    public void ForceLoad(){
        if(loaded)GSceneManager.instance.LoadStartMenuLoader();
    }
    void Update(){
        Load();
        if(forceLoad){ForceLoad();}
    }
}
