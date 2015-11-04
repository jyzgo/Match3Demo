using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using LitJson;

#pragma warning disable 219,414


public class TestCs : MonoBehaviour {

	// Use this for initialization
	void Start () {
//		testEnum ();
//		caller();
		inCaller();
		SaveToFile ();
	}
	
	void SaveToFile()
	{
		string str = @"
            {
                ""Name""     : ""yusong"",
                ""Age""      : 26,
				""Face""     : 12.5,
                ""Birthday"" : ""1986-11-21"",
 				""Thumbnail"":[
				{
           			""Url"":    ""http://xuanyusong.com"",
           			""Height"": 256,
           			""Width"":  ""200""
				},
				{
           			""Url"":    ""http://baidu.com"",
           			""Height"": 1024,
           			""Width"":  ""500""
				}
 
				]
            }";

		JsonData jd = JsonMapper.ToObject (str);
		Debug.Log (" name is " + (string)jd ["Name"]);
		Debug.Log("Age is " + (int)jd["Age"]);
		Debug.Log("Face is " + (double)jd["Face"]);
		JsonData jdItems = jd["Thumbnail"];
		for (int i = 0; i < jdItems.Count; i ++) {
			var curData = jdItems[i];
			var curURL = (string)curData["Url"];
			Debug.Log ("url " + curURL);
		}
	}


	void OnGUI()
	{
		if (GUILayout.Button("testenumer")) 
		{
			testEnumtor();
		}
	}


	void testEnumtor()
	{
		int[] arr = {1,2,3};
		beEns(arr);

	}

	IEnumerator beEns(int[] data)
	{
		foreach (var i in data) 
		{
			yield return i ;
		}
	}

	void testEnum()
	{
		var curHash = new HashSet<int> ();
		for (int i = 0; i < 3; i++) {
			curHash.Add (i);
		}

		var curEn = curHash.GetEnumerator ();
		while (curEn.MoveNext ()) {
			Debug.Log (curEn.Current);
		}
	}

	void testAction(Action curAct)
	{
		curAct ();
	}

	void caller()
	{
		Debug.Log ("call");
		Action dkd = para;
		testAction (dkd);	
	}

	void para()
	{
		Debug.Log ("para");
	}


	void testFunc(Func<float,double,decimal,int> curFunc,float b)
	{
		int num = curFunc (b,0.1,3);
		Debug.Log ("num " + num);
	}

	int inPar(float num,double curDouble,decimal curDec)
	{
		return (int)(num * 100);
	}

	void inCaller()
	{
		Func<float,double,decimal,int> curC = inPar;

		tte curT = new tte ();
			
		testFunc (curT.iii, 0.2f);
	}
}

public class tte
{
	public tte()
	{
		
	}
	public int iii(float num,double curDouble,decimal curDec)
	{
		return (int)(num * 2000);
	}
}

#pragma warning restore 219,414