using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Use object pooling to create a new audio source object set its position near the firework torus and play a random audio and then get deactived

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Sound[] sounds;
    public static AudioManager instance;
    public float fireworksAudioStartDelay = 1f;
    public bool shouldPlayFireworksAudio = false;
    //public float startDelay = 1f;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ToggleFireworkAudio()
    {
        shouldPlayFireworksAudio = !shouldPlayFireworksAudio;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
