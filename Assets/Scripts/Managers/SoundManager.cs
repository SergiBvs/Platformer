using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    [HideInInspector] public AudioSource m_AS;

    public AudioClip m_CoinPickupSound;
    public AudioClip m_DashSound;
    public AudioClip m_DeathSound;
    public AudioClip m_GrassSound1;
    public AudioClip m_GrassSound2;
    public AudioClip m_GrenadeExplosion;
    public AudioClip m_HurtSound;
    public AudioClip m_JumpSound;
    public AudioClip m_KillSound;
    public AudioClip m_LevelCompleteSound;
    public AudioClip m_PortalSound;
    public AudioClip m_ShieldHitSound;
    public AudioClip m_ShotSound;
    public AudioClip m_SnowSound1;
    public AudioClip m_SnowSound2;


    void Start ()
    {
        m_AS = GetComponent<AudioSource>();
    }
	
}
