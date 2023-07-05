using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class lamp : MonoBehaviour
{

    public BoxCollider2D box;
    public ParticleSystem sparks;
    public ParticleSystem transition;
    public AudioSource CrystalNoise;
    public bool isOn;

    public Action BorderEnter;
    public Action BorderExit;

    public float volume = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        isOn = false;
        sparks.Stop();
        transition.Stop();
        SoundControl.Instance.FadeVolume("CrystalChime", 0.0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D()
    {
        BorderEnter();
        isOn = true;
        SoundControl.Instance.IncreaseFadeVolume("CrystalChime", volume);
        //PlayCrystal();
        sparks.Play();
    }
    private void OnTriggerStay2D()
    {
        
        //Debug.Log("ja");
    }
    private void OnTriggerExit2D()
    {
        BorderExit();
        SoundControl.Instance.IncreaseFadeVolume("CrystalChime", -volume);
     
        sparks.Stop();
        //StopCrystal();
        isOn = false;
    }
    public void PlayCrystal()
    {
        CrystalNoise.Play();
    }
    public void StopCrystal()
    {
        CrystalNoise.Stop();
    }
    public void PlayFinal()
    {
        transition.Play();
    }
}
