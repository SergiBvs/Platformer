using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour {

    public int m_ammo = 3;
    public GameObject m_shot;

    public GameObject gunTip;

    public GameObject m_Player;

    int fliped = 1;

    bool m_weaponReady = true;
    Vector2 mousePos;

    public float WeaponCD = 2.5f;
    public float CurrentWeaponCD;
    public Slider WeaponCDSlider;
    public GameObject WeaponCDCanvas;

    private SoundManager m_ShotSound;

    // Use this for initialization
    void Start ()
    {
        m_ShotSound = GameObject.FindGameObjectWithTag("ShotSound").GetComponent<SoundManager>();
	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetMouseButton(0))
        {
            if (m_weaponReady)
            {
                WeaponCDCanvas.SetActive(true);
                CurrentWeaponCD = WeaponCD;
                //print(CurrentWeaponCD);
                m_ShotSound.m_AS.clip = m_ShotSound.m_ShotSound;
                m_ShotSound.m_AS.Play();

                m_weaponReady = false;
                m_shot = Instantiate((GameObject)Resources.Load("Shot"), gunTip.transform.position, gunTip.transform.rotation);
                m_shot.GetComponent<Rigidbody2D>().velocity = gunTip.transform.right * 7f;

                //Instantiate(m_shot, this.transform.position, transform.rotation);     
                
            }
        }

        WeaponCDSlider.value = CalculateSliderValue();

        if (CurrentWeaponCD <= 0)
        {
            CurrentWeaponCD = 0;
            m_weaponReady = true;
            WeaponCDCanvas.SetActive(false);
        }
        else if (CurrentWeaponCD > 0)
        {
            //print(CurrentWeaponCD);
            CurrentWeaponCD -= Time.deltaTime;
        }
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float AngleRad = Mathf.Atan2(mousePos.y - transform.position.y,mousePos.x - transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;

        float angle = AngleDeg;

        //print(AngleDeg);

        // Rotate Object
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

        float mouse = transform.position.x - mousePos.x;

        if (mouse > 0)
        {
                transform.localScale = new Vector2(-1, -1);
        }
        else if(mouse < 0)
        {
            transform.localScale = new Vector2(1, 1);
            
        }
    }

    
    IEnumerator WeaponCooldown()
    {
        yield return null;
    }
    float CalculateSliderValue()
    {
        return (CurrentWeaponCD / WeaponCD);
    }

}
