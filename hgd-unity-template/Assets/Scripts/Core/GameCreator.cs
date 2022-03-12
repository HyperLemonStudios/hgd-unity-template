using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;

public class GameCreator : MonoBehaviour{   public static GameCreator instance;
    [SerializeField] GameObject saveSerialPrefab;
    [SerializeField] GameObject gsceneManagerPrefab;
    [SerializeField] GameObject gameSessionPrefab;
    
    [Header("Assets managers")]
    [SerializeField] GameObject gameAssetsPrefab;
    [SerializeField] GameObject audioManagerPrefab;

    
    [Header("Networking, Advancements etc")]
    [AssetsOnly][SerializeField] GameObject discordPresencePrefab;
    private void Awake(){
        instance=this;
        if(SceneManager.GetActiveScene().name=="Loading")LoadPre();
        else Load();
    }
    void LoadPre(){
        if(FindObjectOfType<SaveSerial>()==null){Instantiate(saveSerialPrefab);}
        if(FindObjectOfType<GSceneManager>()==null){var go=Instantiate(gsceneManagerPrefab);go.GetComponent<GSceneManager>().enabled=true;}
            /*Idk it disables itself so I guess Ill turn it on manually*/
    }

    void Load(){
        LoadPre();

        if(FindObjectOfType<GameSession>()==null){Instantiate(gameSessionPrefab);}
        
        if(FindObjectOfType<GameAssets>()==null){Instantiate(gameAssetsPrefab);}
        if(FindObjectOfType<AudioManager>()==null){Instantiate(audioManagerPrefab);}


        if(FindObjectOfType<PostProcessVolume>()!=null&& FindObjectOfType<SaveSerial>().settingsData.pprocessing!=true){FindObjectOfType<PostProcessVolume>().enabled=false;}

    }
}
