using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    void reset_poziomu()
    {
        
        GameObject.Find("Controller").GetComponent<Controller>().typ_przegranej = "brak";
        SceneManager.LoadSceneAsync("p1");
    }
    void menu_główne() 
    {
        GameObject.Find("Controller").GetComponent<Controller>().typ_przegranej = "brak";
        SceneManager.LoadSceneAsync("menu");
        
    }
    private void Start()
    {
        GameObject.Find("Tak").GetComponent<Button>().onClick.AddListener(reset_poziomu);
        GameObject.Find("Nie").GetComponent<Button>().onClick.AddListener(menu_główne);
    }

}
