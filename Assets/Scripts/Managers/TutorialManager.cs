using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    public Animator[] keyAnim;
	private bool done1;
	private bool done2;
    public KeyCode[] key;
	public bool needMore = false;
	
	void Start()
	{
		done1 = false;
		done2 = false;
	}

	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(key[0]) || Input.GetKeyDown(key[1]) && !done1)
		{
			keyAnim[0].SetTrigger("OUT");
			keyAnim[1].SetTrigger("OUT");
			done1 = true;
		}

		if (needMore)
		{
			if (Input.GetKeyDown(key[2]) || Input.GetKeyDown(key[3]) && !done2)
			{
				keyAnim[2].SetTrigger("OUT");
				keyAnim[3].SetTrigger("OUT");
				done2 = true;
			}
		}
		
    }
}
