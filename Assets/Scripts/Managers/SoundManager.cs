using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    [HideInInspector] public AudioSource m_AS;

    public AudioClip m_BounceSound; //done
    public AudioClip m_CoinPickUpSound; //done
    public AudioClip m_DashSound; //done
    public AudioClip m_DeathSound; //done
    public AudioClip m_GrassSound1;
    public AudioClip m_GrassSound2;
    public AudioClip m_GrenadeExplosion; //done
    public AudioClip m_HealthUpSound;
    public AudioClip m_HurtSound;//done
    public AudioClip m_JumpSound;//done
    public AudioClip m_KillSound;//done
    public AudioClip m_LevelCompleteSound;//done
    public AudioClip m_PortalSound; //Done
    public AudioClip m_ShieldHitSound;//done
    public AudioClip m_ShotSound; //done
    public AudioClip m_SnowSound1;
    public AudioClip m_SnowSound2;


    void Start ()
    {
        m_AS = GetComponent<AudioSource>();
    }
	
}
