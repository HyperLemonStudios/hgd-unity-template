using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    public static Player instance;
    [Header("Variables")]
    public float hp;
    [Header("Other")]
    public bool damaged;
    public bool healed;
    void Start(){
        instance=this;
    }
    void Update(){
        
    }
}
