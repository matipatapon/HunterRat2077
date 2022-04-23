using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class go_to_menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("menu");
    }

 
}
