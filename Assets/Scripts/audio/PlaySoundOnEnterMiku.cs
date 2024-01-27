using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnEnter : MonoBehaviour
{
    AudioSource getScoredSFX;
    Collider2D soundTrigger;

    void Awake() 
    {
        getScoredSFX = GetComponent<AudioSource>();
        soundTrigger = GetComponent<Collider2D>();
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("PlayerTag"))
        {
            getScoredSFX.Play();
        }
    }
}