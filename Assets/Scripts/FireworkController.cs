using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class FireworkController : MonoBehaviour
{
    public VisualEffect vfx;
    //public float delay = 1f;
    void Start()
    {
        //vfx = GetComponent<VisualEffect>();
        List<string> names = new List<string>();
        Gradient gt = vfx.GetGradient("ExplosionColor");
        vfx.outputEventReceived += OnOutputEventRecieved;
        //Debug.Log(gt.alphaKeys[0].alpha);

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Play()
    {
        vfx.SendEvent("OnPlay");
    }

    public void Stop()
    {
        vfx.SendEvent("OnStop");
    }

    void OnOutputEventRecieved(VFXOutputEventArgs args)
    {
        //StartCoroutine(playAudio()); 
        //args.eventAttribute.
    }
}
