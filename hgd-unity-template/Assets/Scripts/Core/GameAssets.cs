using System;
using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;

public class GameAssets : MonoBehaviour{	public static GameAssets instance;
	[Header("Special game-related")]
    [AssetsOnly]public GameObject powerupSpawnerPrefab;
    [AssetsOnly]public GameObject waveSpawnerPrefab;
    [AssetsOnly]public GameObject disrupterSpawnerPrefab;
	[Header("Main")]
	[AssetsOnly,Searchable]public GObject[] objects;
	[AssetsOnly,Searchable]public GObject[] vfx;
	[AssetsOnly,Searchable]public GSprite[] sprites;
    
    void Awake(){if(instance!=null){Destroy(gameObject);}else{DontDestroyOnLoad(gameObject);instance=this;}}

    public GameObject Make(string obj, Vector2 pos){
		GObject o=Array.Find(objects, item => item.name == obj);
		if(o==null){
			Debug.LogWarning("Object: " + name + " not found!");
			return null;
		}
		GameObject gobj=o.gobj;
        GameObject objref=Instantiate(gobj,pos,Quaternion.identity);
        return objref;
	}
    public GameObject MakeSpread(string obj, Vector2 pos, int amnt=3, float rangeX=0.5f, float rangeY=0.5f){
		GObject o=Array.Find(objects, item => item.name == obj);
		if(o==null){
			Debug.LogWarning("Object: " + name + " not found!");
			return null;
		}
		GameObject gobj=o.gobj;
		GameObject objref=Instantiate(gobj,pos,Quaternion.identity);
		for(var i=1;i<amnt-1;i++){
		var rndmX=UnityEngine.Random.Range(-rangeX,rangeX);
		var rndmY=UnityEngine.Random.Range(-rangeY,rangeY);
		var poss=pos+new Vector2(rndmX,rndmY);
        Instantiate(gobj,poss,Quaternion.identity);
		}
        return objref;
	}
    public GameObject VFX(string obj, Vector2 pos, float duration=0){
		GObject o=Array.Find(vfx, item => item.name == obj);
		if(o==null){
			Debug.LogWarning("Object: " + obj + " not found!");
			return null;
		}
		GameObject gobj=o.gobj;
        GameObject objref;
		if(SaveSerial.instance.settingsData.particles){
			objref=Instantiate(gobj,pos,Quaternion.identity);
			if(duration!=0)Destroy(objref,duration);
			return objref;
		}else return null;
	}
    public GameObject Get(string obj){
		GObject o=Array.Find(objects, item => item.name == obj);
		if(o==null){
			Debug.LogWarning("Object: " + obj + " not found!");
			return null;
		}
		GameObject gobj=o.gobj;
        return gobj;
	}public GameObject GetVFX(string obj){
		GObject o=Array.Find(vfx, item => item.name == obj);
		if(o==null){
			Debug.LogWarning("VFX: " + obj + " not found!");
			return null;
		}
		GameObject gobj=o.gobj;
		return gobj;
        //if(SaveSerial.instance.settingsData.particles)return gobj; else return null;
	}

    public Sprite Spr(string spr){
		GSprite s=Array.Find(sprites, item => item.name == spr);
		if(s==null){
			Debug.LogWarning("Sprite: " + spr + " not found!");
			return null;
		}
		Sprite gs=s.spr;
        return gs;
	}


	public void TransformIntoUIParticle(GameObject go,float mult=0,float dur=-4,bool multShape=false){
		var ps=go.GetComponent<ParticleSystem>();var psMain=ps.main;
		if(mult==0){
			if(ps.startSize<=1){mult=100;}
			if(ps.startSize<=10&&ps.startSize>1){mult=10;}
		}
		if(dur>0){Destroy(go,dur);}
		else if(dur==0){Destroy(go,psMain.startLifetime.constantMax+psMain.duration);}
		else if(dur==-1){Destroy(go,psMain.startLifetime.constantMax+psMain.duration*2);}
		var startSize=psMain.startSize;
		var sizeMin=startSize.constantMin;var sizeMax=startSize.constantMax;if(sizeMin==0){sizeMin=sizeMax;}
		var startSpeed=psMain.startSpeed;
		var speedMin=startSpeed.constantMin;var speedMax=startSpeed.constantMax;if(speedMin==0){speedMin=speedMax;}
		var startColor=new ParticleSystem.MinMaxGradient(psMain.startColor.colorMin,psMain.startColor.colorMax);
		var _color=startColor.colorMin;if(startColor.colorMin.r<0){_color=startColor.colorMax;}
		if(_color==Color.clear){_color=Color.white;}
		//psMain.startColor=new ParticleSystem.MinMaxGradient(_color,psMain.startColor.colorMax);
		var colorBySpeed=ps.colorBySpeed;
		var colorMin=colorBySpeed.range.x;var colorMax=colorBySpeed.range.y;
		psMain.startSize=new ParticleSystem.MinMaxCurve(sizeMin*mult, sizeMax*mult);
		psMain.startSpeed=new ParticleSystem.MinMaxCurve(speedMin*mult, speedMax*mult);
		//colorBySpeed.range=new Vector2(colorMin*mult, colorMax*mult);
		//colorBySpeed.range=new Vector2(colorMin*30, colorMax*30);
		var psShape=ps.shape;if(multShape){psShape.scale*=mult;}

		var psUI=go.AddComponent<UnityEngine.UI.Extensions.UIParticleSystem>();
		var _tex=ps.GetComponent<Renderer>().material.GetTexture("_MainTex");
		Material mat=new Material(Shader.Find("UI Extensions/Particles/Additive"));
		/*Debug.Log(go.name+" | ColorMin: "+startColor.colorMin);
		Debug.Log(go.name+" | ColorMax: "+startColor.colorMax);
		float H,S,V;Color.RGBToHSV(_color,out H,out S,out V);
		Debug.Log(go.name+" | _color: "+_color + " | HSV("+H+", "+S+", "+V+")");*/
		if(_isColorDark(_color)){
			//Debug.Log(go.name+" - IsDark");
			mat=new Material(Shader.Find("UI Extensions/Particles/Alpha Blended"));
		}
		mat.SetTexture("_MainTex",_tex);
		psUI.material=mat;
	}
	public bool _isColorDark(Color color){bool b=false;float H,S,V;Color.RGBToHSV(color,out H,out S,out V);if(V<=0.3f){b=true;}return b;}
	public void MakeParticleLooping(ParticleSystem ps){var psMain=ps.main;psMain.loop=true;psMain.stopAction=ParticleSystemStopAction.None;}

	public class CoroutineWithData {
		public Coroutine coroutine { get; private set; }
		public object result;
		private IEnumerator target;
		public CoroutineWithData(MonoBehaviour owner, IEnumerator target){
			this.target=target;
			this.coroutine=owner.StartCoroutine(Run());
		}
	
		private IEnumerator Run(){
			while(target.MoveNext()){
				result=target.Current;
				yield return result;
			}
		}
 }
}

[System.Serializable]
public class GObject{
	public string name;
	public GameObject gobj;
}
[System.Serializable]
public class GSprite{
	public string name;
	public Sprite spr;
}
[System.Serializable]
public class SimpleAnim{
	public Sprite spr;
	public float delay;
}