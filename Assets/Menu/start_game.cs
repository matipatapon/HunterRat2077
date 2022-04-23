using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class start_game : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(load_game);
    }
    
    public void load_game() 
    {
      SceneManager.LoadSceneAsync("p1");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
