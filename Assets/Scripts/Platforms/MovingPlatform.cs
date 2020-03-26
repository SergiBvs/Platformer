using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public Transform[] pivots;
    private Transform currentTarget;

    private int targetInt = 0;
    int dir = 1;

    public bool needsPlayerOnTop;
    public bool needsButton;
    public bool needsButtonPressed;

    bool buttonDown;
    bool buttonPressed;
    bool playerOnTop;

    // Use this for initialization
    void Awake () {
        currentTarget = pivots[0];
    }
	
	// Update is called once per frame
	void Update () {

        if (needsButton)
        {
            if (buttonDown) Movement();
        }
        else if (needsButtonPressed)
        {
            if (buttonPressed) Movement();
        }
        else if (needsPlayerOnTop)
        {
            if (playerOnTop) Movement();
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

    private void Movement()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, currentTarget.position, 0.1f);
        if (this.transform.position == currentTarget.position)
        {
            if (targetInt == pivots.Length - 1)
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

    public void OnButtonPress()
    {
        buttonDown = true;
        buttonPressed = true;
    }

    public void OnButtonOut()
    {
        buttonPressed = false;
    }

}
