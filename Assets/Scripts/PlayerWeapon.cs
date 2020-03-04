using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {

    public int m_ammo = 3;
    public GameObject m_shot;

    

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
                m_shot = (GameObject)Resources.Load("Shot");
                Instantiate(m_shot, this.transform.position + new Vector3(1,1,0), Quaternion.Euler(0, 0, 45));
                
                StartCoroutine(WeaponCooldown());
            }
        }
	}

    IEnumerator WeaponCooldown()
    {
        yield return new WaitForSeconds(3f);
        m_weaponReady = true;
    }

}
