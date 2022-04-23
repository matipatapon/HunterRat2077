using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pauza : MonoBehaviour 
{
    bool czy_gra_zatrzymana = false;

    public GameObject interfejs;
   public void pauza() 
    {
        if (GameObject.Find("Controller").GetComponent<Controller>().typ_przegranej != "brak") return;
        if (!czy_gra_zatrzymana) 
        {
            Time.timeScale = 0;
            czy_gra_zatrzymana = true;
            AudioListener.pause = true;
            interfejs.SetActive(true);
        }
        else 
        {
            Time.timeScale = 1;
            czy_gra_zatrzymana = false;
            AudioListener.pause = false;
            interfejs.SetActive(false);
        }
    }
}
