using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    public Animator[] keyAnim;
    bool ledtdone;
    bool rightdone;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Horizontal") < 0 && !rightdone)
        {
            keyAnim[0].SetTrigger("OUT");
            keyAnim[1].SetTrigger("OUT");
            rightdone = true;
        }
        else if (Input.GetAxis("Horizontal") > 0 && !ledtdone)
        {
            keyAnim[2].SetTrigger("OUT");
            keyAnim[3].SetTrigger("OUT");
            ledtdone = true;
        }
    }
}
