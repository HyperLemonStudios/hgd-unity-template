﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Screenflash : MonoBehaviour{
    [SerializeField] Color damageFlashColor;
    [SerializeField] float damageFlashSpeed;
    [SerializeField] Color healFlashColor;
    [SerializeField] float healedFlashSpeed;
    Image image;
    void Start(){
        image=GetComponent<Image>();
    }

    void Update(){
        if(SaveSerial.instance.settingsData.screenflash&&Player.instance!=null){
        if(Player.instance.damaged==true){image.color=damageFlashColor;Player.instance.damaged=false;}
        else{image.color=Color.Lerp(image.color, Color.clear, damageFlashSpeed*Time.deltaTime);}

        if(Player.instance.healed==true){image.color=healFlashColor;Player.instance.healed=false;}
        else{image.color=Color.Lerp(image.color, Color.clear, healedFlashSpeed*Time.deltaTime);}
        }
    }
}
