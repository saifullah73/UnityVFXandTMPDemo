using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //public float speed = 20.0f;
    //public GameObject player;
    //private Vector3 offset = new Vector3(0,2,-7);
    public float roationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        //transform.position = player.transform.position + offset;
        float horizontalInput = Input.GetAxis("Horizontal");
        //this.transform.Rotate(Vector3.up, horizontalInput * roationSpeed * Time.deltaTime);
    }
}
