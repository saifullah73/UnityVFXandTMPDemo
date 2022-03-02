using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLights : MonoBehaviour
{

    public Light light1;
    public Light light2;
    public bool trigger = false;
    public float timeScale = 0f;
    public Color color1;
    public Color color2;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();      
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger)
        {
            StartCoroutine(ToggleLight());
        }
    }

    public void Trigger()
    {
        trigger = true;
        source.Play();
        Vibration.Vibrate(50);
    }

    IEnumerator ToggleLight()
    {
        trigger = false;
        float waitTime = 0f;
        while (waitTime < 0.99f)
        {
            light1.color = Color.Lerp(color1, color2,waitTime);
            light2.color = Color.Lerp(color1, color2, waitTime);
            //yield return null;
            waitTime += Time.deltaTime / timeScale;
        }
        yield return new WaitForSeconds(1);
        waitTime = 0f;
        while (waitTime < 0.99f)
        {
            light1.color = Color.Lerp(color2, color1, waitTime);
            light2.color = Color.Lerp(color2, color1, waitTime);
            //yield return null;
            waitTime += Time.deltaTime / timeScale;
        }
    }
}
