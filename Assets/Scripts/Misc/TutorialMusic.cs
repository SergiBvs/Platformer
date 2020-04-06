using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialMusic : MonoBehaviour {

	public static TutorialMusic instance = null;
	
	void Awake()
	{
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		string sc = SceneManager.GetActiveScene().name;
		if (sc == "AMainHub" || sc == "ENDING")
		{
			instance = null;
			Destroy(this.gameObject);
		}
	}
	
}
