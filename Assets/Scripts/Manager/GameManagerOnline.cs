using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerOnline : MonoBehaviour
{

	// public List<CharacterInfo> listCharacters;

 //    // Start is called before the first frame update
 //    void Start()
 //    {
 //        Physics2D.IgnoreLayerCollision(9, 8, true);
 //    }

 //    // Update is called once per frame
 //    void Update()
 //    {
        
 //    }

 //    public void DeleteDefaultCharacters(){

 //    }

 //    public List<CharacterInfo> getListCharacters(){
 //    	return new List<CharacterInfo>();
 //    }

 //    public void CreatePlayer(){

 //    }

 //    public void CreateEnemies(bool isSelfMove){

 //    }

 //    public void CreateDumpPlayer(string name){

 //    }


}


public class CharacterInfo {

	int id;
	Vector2 startPoint;
	GameObject prefab;
	bool isSelfMove;

	GameObject myInstance;

}