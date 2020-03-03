using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public Transform[] pivots;
    private Transform currentTarget;

    private int targetInt = 0;
    int children;
    int dir = 1;

    // Use this for initialization
    void Awake () {
        currentTarget = pivots[0];
    }
	
	// Update is called once per frame
	void Update () {

        
        this.transform.position = Vector3.MoveTowards(this.transform.position, currentTarget.position, 0.2f);
        if(this.transform.position == currentTarget.position)
        {
            if(targetInt == pivots.Length -1)
            {
                dir = -1;
            }
            else if (targetInt == 0)
            {
                dir = 1;
            }

            targetInt += dir;
            currentTarget = pivots[targetInt];
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.parent = this.transform;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.parent = null;
    }

}
