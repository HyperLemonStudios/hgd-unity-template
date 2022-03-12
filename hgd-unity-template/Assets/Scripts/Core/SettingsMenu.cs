using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

public class SettingsMenu : MonoBehaviour{
    [SerializeField] GameObject[] panels;
    [SerializeField]GameObject discordRPCToggle;
    [SerializeField]GameObject cheatToggle;
    [SerializeField]GameObject qualityDropdopwn;
    [SerializeField]GameObject fullscreenToggle;
    [SerializeField]GameObject pprocessingToggle;
    [SerializeField]GameObject masterSlider;
    [SerializeField]GameObject soundSlider;
    [SerializeField]GameObject musicSlider;
    [SerializeField]AudioSource audioSource;
    public AudioMixer audioMixer;
    [AssetsOnly][SerializeField]GameObject pprocessingPrefab;
    public PostProcessVolume postProcessVolume;
    private void Start(){
        if(audioSource==null)audioSource=GetComponent<AudioSource>();

        if(SaveSerial.instance!=null){
            discordRPCToggle.GetComponent<Toggle>().isOn = SaveSerial.instance.settingsData.discordRPC;
            cheatToggle.GetComponent<Toggle>().isOn = GameSession.instance.cheatmode;

            masterSlider.GetComponent<Slider>().value = SaveSerial.instance.settingsData.masterVolume;
            soundSlider.GetComponent<Slider>().value = SaveSerial.instance.settingsData.soundVolume;
            musicSlider.GetComponent<Slider>().value = SaveSerial.instance.settingsData.musicVolume;

            qualityDropdopwn.GetComponent<Dropdown>().value = SaveSerial.instance.settingsData.quality;
            fullscreenToggle.GetComponent<Toggle>().isOn = SaveSerial.instance.settingsData.fullscreen;
            pprocessingToggle.GetComponent<Toggle>().isOn = SaveSerial.instance.settingsData.pprocessing;
        }
        if(SceneManager.GetActiveScene().name=="Options")OpenSettings();
    }
    private void Update(){
        postProcessVolume=FindObjectOfType<PostProcessVolume>();
        if(SaveSerial.instance!=null)if(SaveSerial.instance.settingsData.pprocessing==true&&postProcessVolume==null){postProcessVolume=Instantiate(pprocessingPrefab,Camera.main.transform).GetComponent<PostProcessVolume>();}
        if(SaveSerial.instance!=null)if(SaveSerial.instance.settingsData.pprocessing==true&&FindObjectOfType<PostProcessVolume>()!=null){postProcessVolume.enabled=true;}
        if(SaveSerial.instance!=null)if(SaveSerial.instance.settingsData.pprocessing==false&&FindObjectOfType<PostProcessVolume>()!=null){postProcessVolume=FindObjectOfType<PostProcessVolume>();postProcessVolume.enabled=false;}//Destroy(FindObjectOfType<PostProcessVolume>());}
        if(SaveSerial.instance.settingsData.masterVolume<=-40){SaveSerial.instance.settingsData.masterVolume=-80;}
        if(SaveSerial.instance.settingsData.soundVolume<=-40){SaveSerial.instance.settingsData.soundVolume=-80;}
        if(SaveSerial.instance.settingsData.musicVolume<=-40){SaveSerial.instance.settingsData.musicVolume=-80;}
    }
    public void SetPanelActive(int i){foreach(GameObject p in panels){p.SetActive(false);}panels[i].SetActive(true);}
    public void OpenSettings(){transform.GetChild(0).gameObject.SetActive(true);transform.GetChild(1).gameObject.SetActive(false);}
    public void OpenDeleteAll(){transform.GetChild(1).gameObject.SetActive(true);transform.GetChild(0).gameObject.SetActive(false);}
    public void Close(){transform.GetChild(0).gameObject.SetActive(false);transform.GetChild(1).gameObject.SetActive(false);}

    
    public void SetDiscordRPC(bool val){SaveSerial.instance.settingsData.discordRPC=val;}
    public void SetCheatmode(bool val){GameSession.instance.cheatmode=val;}

    public void SetMasterVolume(float val){SaveSerial.instance.settingsData.masterVolume=val;}
    public void SetSoundVolume(float val){SaveSerial.instance.settingsData.soundVolume=val;}
    public void SetMusicVolume(float val){SaveSerial.instance.settingsData.musicVolume=val;}

    public void SetQuality(int val){
        QualitySettings.SetQualityLevel(val);
        if(SaveSerial.instance!=null)SaveSerial.instance.settingsData.quality=val;
    }
    public void SetFullscreen(bool val){
        Screen.fullScreen=val;
        if(SaveSerial.instance!=null)SaveSerial.instance.settingsData.fullscreen=val;
        Screen.SetResolution(Display.main.systemWidth,Display.main.systemHeight,val,60);
    }
    public void SetPostProcessing(bool val){
        postProcessVolume=FindObjectOfType<PostProcessVolume>();
        if(SaveSerial.instance!=null)if(SaveSerial.instance!=null)SaveSerial.instance.settingsData.pprocessing = val;
        if(val==true && postProcessVolume==null){postProcessVolume=Instantiate(pprocessingPrefab,Camera.main.transform).GetComponent<PostProcessVolume>();}//GSceneManager.instance.RestartScene();}
        if(val==true && postProcessVolume!=null){postProcessVolume.enabled=true;}
        if(val==false && FindObjectOfType<PostProcessVolume>()!=null){FindObjectOfType<PostProcessVolume>().enabled=false;}//Destroy(FindObjectOfType<PostProcessVolume>());}
    }
}
