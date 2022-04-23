using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public GameObject gipcio;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(gipcio.transform.position.x, gipcio.transform.position.y,-10);


    }
}
