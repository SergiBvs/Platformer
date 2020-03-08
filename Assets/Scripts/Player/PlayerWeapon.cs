using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {

    public int m_ammo = 3;
    public GameObject m_shot;

    public GameObject gunTip;

    bool m_weaponReady = true;
    
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            if (m_weaponReady)
            {
                m_weaponReady = false;
                m_shot = Instantiate((GameObject)Resources.Load("Shot"), gunTip.transform.position, gunTip.transform.rotation);
                m_shot.GetComponent<Rigidbody2D>().velocity = gunTip.transform.right * 7f; 
                //Instantiate(m_shot, this.transform.position, transform.rotation);      
                StartCoroutine(WeaponCooldown());
            }
        }
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float AngleRad = Mathf.Atan2(mousePos.y - transform.position.y,mousePos.x - transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;

        // Rotate Object
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
    }

    IEnumerator WeaponCooldown()
    {
        yield return new WaitForSeconds(3f);
        m_weaponReady = true;
    }

}
