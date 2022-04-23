using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pole_wykrywacz : MonoBehaviour
{

    //Służy ta funkcja do tego żeby wykrywać czy jest obiekt w polu czy też nie :P 
    //Ogólnie powinienem rozbijać działanie obiektu na więcej obiektów funkcji ...

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "pole") transform.parent.GetComponent<Jedzonko_Controler>().czy_w_polu = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "pole") transform.parent.GetComponent<Jedzonko_Controler>().czy_w_polu = false;
    }

}
