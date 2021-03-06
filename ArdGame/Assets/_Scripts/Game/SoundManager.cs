﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMono<SoundManager>
{
    [SerializeField] AudioClip[] m_collision;

    [SerializeField] AudioSource m_gingleSource;
    [SerializeField] AudioSource m_collisionSource;
    [SerializeField] AudioSource m_ballSource;

    public AudioSource m_doorSource;

    private void Awake()
    {
        //Camera.main.GetComponent<AudioListener>().enabled = false;
    }

    public void PlayGingle()
    {
        m_gingleSource.Play();
    }

    public void SetBallSpeed(float value)
    {
        m_ballSource.volume = Mathf.Min(value, 0.3f);
    }

    public void StopBallSound()
    {
        StartCoroutine(FadeBallSound());
    }

    IEnumerator FadeBallSound()
    {
        while(m_ballSource.volume > 0)
        {
            m_ballSource.volume -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    [ContextMenu("test")]
    public void PlayRandomCollision()
    {
        m_collisionSource.PlayOneShot(m_collision[Random.Range(0,m_collision.Length)]);
    }
}
