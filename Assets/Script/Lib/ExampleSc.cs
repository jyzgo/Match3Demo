using UnityEngine;
using System.Collections;
//using ActionExt;

public class ExampleSc : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		MoveTo curAct = new MoveTo (new Vector3(10,10,10),5);
		this.runAction (curAct);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		if (GUILayout.Button ("dest")) {
			Destroy (gameObject);
		}
	}
}
