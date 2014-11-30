﻿using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataShare : MonoBehaviour {

	public static DataShare dataShare;
	private string route;

	public int highScore = 0;
	public int maxDistance = 0;

	void Awake () {

		route = Application.persistentDataPath + "/data.dat";

		if (dataShare == null){
			dataShare = this;
			DontDestroyOnLoad(gameObject);
		}else if(dataShare !=this){
			Destroy(gameObject);
		}
	}

	void Start(){
		Load();
	}

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(route);
		
		toSave datos = new toSave();
		datos.highScore = highScore;
		datos.maxDistance = maxDistance;
		
		bf.Serialize(file, datos);
		
		file.Close();
	}
	
	void Load(){
		if(File.Exists(route)){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(route, FileMode.Open);
			
			toSave datos = (toSave) bf.Deserialize(file);
			
			highScore = datos.highScore;
			maxDistance = datos.maxDistance;
			
			file.Close();
		}else{
			highScore = 0;
			maxDistance = 0;
		}
	}

}

[Serializable]
class toSave{
	public int highScore;
	public int maxDistance;
}