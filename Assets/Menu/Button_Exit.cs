using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Exit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        GetComponent<Button>().onClick.AddListener(Exit);
       
        //GameObject.Find("Exit").GetComponent<Button>().onClick.AddListener(exit);
    }
    void Exit() 
    {
        Application.Quit();
        
    }

}
