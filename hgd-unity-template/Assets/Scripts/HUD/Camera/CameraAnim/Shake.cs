using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour{
    public static Shake instance;
    public Animator camAnim;
    [HideInInspector]public float mult;
    [SerializeField]float x=0;
    [SerializeField]float y=0;
    private void Awake(){instance=this;}
    public void CamShake(float multiplier, float speed){
    if(SaveSerial.instance.settingsData.screenshake){
        if(multiplier>mult||camAnim.GetBool("shake")!=true){
        camAnim.ResetTrigger("shake");
        camAnim.SetTrigger("shake");
        camAnim.speed=speed;
        mult=multiplier;
        if(SaveSerial.instance.settingsData.vibrations)Vibrator.Vibrate((int)(22*(mult/speed)));
        }
    }else if(SaveSerial.instance.settingsData.vibrations){ 
        mult=multiplier; 
        Vibrator.Vibrate((int)(22*(mult/speed))); 
    }
    }
    private void Update() {
        camAnim.transform.position=new Vector3(x*mult,y*mult,-10);
    }
}
