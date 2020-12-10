using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public Transform[] pivots;
    private Transform currentTarget;
    public int step = 5;
    public Transform father;

    private int targetInt = 0;
    int dir = 1;

    Vector3 playerOriginalSize;


    // Use this for initialization
    void Awake () {
        currentTarget = pivots[0];
    }
	
	// Update is called once per frame
	void Update () {

        
        this.transform.position = Vector3.MoveTowards(this.transform.position, currentTarget.position, step * Time.deltaTime);
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
        //Movement();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(father);

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.parent = null;
        //StartCoroutine(prueba(collision));
    }

    //private void Movement()
    //{
    //    this.transform.position = Vector3.MoveTowards(this.transform.position, currentTarget.position, 0.1f);
    //    if (this.transform.position == currentTarget.position)
    //    {
    //        if (targetInt == pivots.Length - 1)
    //        {
    //            dir = -1;
    //        }
    //        else if (targetInt == 0)
    //        {
    //            dir = 1;
    //        }

    //        targetInt += dir;
    //        currentTarget = pivots[targetInt];
    //    }
    //}

    //IEnumerator prueba(Collision2D collision)
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    collision.transform.parent = null;
    //}


}
