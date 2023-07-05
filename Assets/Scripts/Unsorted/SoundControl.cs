using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    private static SoundControl _instance;
    public static SoundControl Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(_instance.gameObject);
        }

        

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.targetVolume = s.volume;
        }
        soundsStatic = sounds;
    }
    public Sound[] sounds;
    public Sound[] soundsStatic;
    public float volumeTargetCrystal = 0;

    public void Play(string name)
    {
        Sound s = Array.Find(soundsStatic, SoundControl => SoundControl.name == name);
        if (s == null)
            return;
        s.source.Play();
    }
    public void SetVolume(string name, float targetVolume)
    {
        Sound s = Array.Find(soundsStatic, SoundControl => SoundControl.name == name);
        if (s == null)
            return;
        s.source.volume = targetVolume;
    }
    public void IncreaseVolume(string name, float targetVolume)
    {
        Sound s = Array.Find(soundsStatic, SoundControl => SoundControl.name == name);
        if (s == null)
            return;
        s.source.volume = s.source.volume + targetVolume;
    }
    public void FadeVolume(string name, float targetVolume)
    {
        Sound s = Array.Find(soundsStatic, SoundControl => SoundControl.name == name);
        if (s == null)
            return;
        s.targetVolume = targetVolume;
    }
    public void IncreaseFadeVolume(string name, float targetVolume)
    {
        Sound s = Array.Find(soundsStatic, SoundControl => SoundControl.name == name);
        if (s == null)
            return;
        s.targetVolume = s.targetVolume + targetVolume;
    }
    

    void Start()
    {
        Play("BGMusic");
        Play("BGMusic2");
        Play("BGMusic3");
        Play("BGMusic4");
        Play("Credits");
        Play("CrystalChime");
        Play("MineCartWheel");
        

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Fade("CrystalChime", 0.001f);
        Fade("MineCartWheel", 0.1f);
        Fade("BGMusic", 0.1f);
        Fade("BGMusic2", 0.1f);
        Fade("BGMusic3", 0.1f);
        Fade("BGMusic4", 0.1f);
        Fade("Credits", 0.01f);
    }

    private void Fade(string name, float fadeSpeed)
    {
        Sound s = Array.Find(soundsStatic, SoundControl => SoundControl.name == name);
        if (s == null)
            return;
        if (s.source.volume != s.targetVolume && fadeSpeed != 0)
        {
            if(s.source.volume - s.targetVolume < 0)
            {
                if (Mathf.Abs(fadeSpeed) >= Mathf.Abs(s.source.volume - s.targetVolume))
                    s.source.volume = s.targetVolume;
                else
                {
                    s.source.volume += fadeSpeed;
                }
            }
            else
            {
                if (Mathf.Abs(fadeSpeed) >= Mathf.Abs(s.source.volume - s.targetVolume))
                    s.source.volume = s.targetVolume;
                else
                {
                    s.source.volume -= fadeSpeed;
                }
            }
        }
    }
}
