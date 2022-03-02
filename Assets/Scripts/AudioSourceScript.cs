using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceScript : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource source;
    private Sound sound;
    private int maxRange;
    private float delay;
    private void Awake()
    {
    }
    void Start()
    {

        maxRange = AudioManager.instance.sounds.Length;
        delay = AudioManager.instance.fireworksAudioStartDelay;
        source = GetComponent<AudioSource>();
        //InvokeRepeating("playAudio",1f,3f);   
    }

    private void Update()
    {
        if (AudioManager.instance.shouldPlayFireworksAudio)
        {
            if (delay >= 0f)
                delay -= Time.deltaTime;
            else if (!source.isPlaying)
            {
                int audioindex = Random.Range(0, maxRange);
                sound = AudioManager.instance.sounds[audioindex];
                source.clip = sound.clip;
                source.pitch = sound.pitch + Random.Range(-0.1f, +0.1f);
                source.volume = sound.volume;
                source.loop = sound.loop;
                source.Play();
            }
        }
    }

}
