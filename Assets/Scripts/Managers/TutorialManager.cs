using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    public Animator[] keyAnim;
	private bool[] done;
    public KeyCode[] key;
	public bool needMore = false;
	
	void Start()
	{
		done[0] = false;
		done[1] = false;
	}

	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(key[0]) || Input.GetKeyDown(key[1]) && !done[0])
		{
			keyAnim[0].SetTrigger("OUT");
			keyAnim[1].SetTrigger("OUT");
			done[0] = true;
		}

		if (needMore)
		{
			if (Input.GetKeyDown(key[2]) || Input.GetKeyDown(key[3]) && !done[1])
			{
				keyAnim[2].SetTrigger("OUT");
				keyAnim[3].SetTrigger("OUT");
				done[1] = true;
			}
		}
		
    }
}
