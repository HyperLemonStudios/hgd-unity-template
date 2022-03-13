using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;

public class GameCreator : MonoBehaviour{   public static GameCreator instance;
    [AssetsOnly][SerializeField] GameObject saveSerialPrefab;
    [AssetsOnly][SerializeField] GameObject gsceneManagerPrefab;
    [AssetsOnly][SerializeField] GameObject gameSessionPrefab;
    
    [Header("Assets managers")]
    [AssetsOnly][SerializeField] GameObject gameAssetsPrefab;
    [AssetsOnly][SerializeField] GameObject audioManagerPrefab;
    [SerializeField] bool jukeboxCreate;
    [AssetsOnly][SerializeField] GameObject jukeboxPrefab;

    
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
        if(FindObjectOfType<Jukebox>()==null&&jukeboxCreate){Instantiate(jukeboxPrefab);}

        if(FindObjectOfType<DiscordPresence.PresenceManager>()==null){Instantiate(discordPresencePrefab);}

        if(FindObjectOfType<PostProcessVolume>()!=null&&FindObjectOfType<SaveSerial>().settingsData.pprocessing!=true){FindObjectOfType<PostProcessVolume>().enabled=false;}

    }
}
