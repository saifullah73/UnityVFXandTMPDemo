using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleFireworks : MonoBehaviour
{
    // Start is called before the first frame update
    public FireworkController[] fireworks;
    public TextAnimate textAnimator;
    public Light light;
    private bool isPlaying = false;
    [HideInInspector]
    public bool trigger = false;
    public Color lightColor;
    private AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();   
    }

    public void Toggle()
    {
        trigger = !trigger;
        source.Play();
        AudioManager.instance.ToggleFireworkAudio();
        textAnimator.Toggle();
        Vibration.Vibrate(100);
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger && !isPlaying)
        {
            for (int i = 0; i < fireworks.Length; i++)
            {
                fireworks[i].Play();
            }
            isPlaying = true;
            light.color = lightColor;
        }
        else if (!trigger && isPlaying)
        {
            for (int i = 0; i < fireworks.Length; i++)
            {
                fireworks[i].Stop();
            }
            isPlaying = false;
            light.color = Color.white;
        }
    }

}
