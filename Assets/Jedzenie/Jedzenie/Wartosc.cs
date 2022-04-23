using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wartosc : MonoBehaviour
{
    //Określa ile jest punktów za dane jedzonko 
    public int punkty;
    //Text określający ilosć punktów ! 
    public GameObject text;
    //Odgłos podczas jedzenia 
    public AudioClip dźwięk;
    //Czy obiekt ma zabić bohatera po dotknięciu ? 
    public bool czy_one_shot = false;
    //Czy obiekt ma być np kubkiem czyli po prostu być ? 
    public bool czy_brak_interakcji = false;
    private void Start()
    {
     
        
        aktualizuj_napis();

    }
    public void zmiana_punktów(int ile)
    {
       
        punkty = ile;
        aktualizuj_napis();

    }
    //Ustawiam ilość punktów na tą podaną 
    void aktualizuj_napis() 
    {
        //Jeśli obiekt nie ma żadnej interakcji (po stronie skryptów po co je wywoływąć ???
        if (czy_brak_interakcji == true) return;
        if (punkty > 0) text.GetComponent<TextMeshPro>().text = punkty.ToString();
    }

}
