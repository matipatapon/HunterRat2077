using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_generator : MonoBehaviour
{
    // Start is called before the first frame update
    //
    public List<GameObject> kafelki = new List<GameObject>();
    //
    public int rozmiar_mapy = 2;
    //Ile razy ma być pomnożony x i y 
    public float mnożnik = 10;

    public GameObject stół;

    public bool czy_tworzyć_stół = false;

    void Start()
    {
       
        //Tworzenie kafelków w pizzeri 
        for (int y = 0; y < rozmiar_mapy; y++)
        {
            for(int x = 0; x < rozmiar_mapy; x++) 
            {
                var o = Instantiate(kafelki[1], new Vector3(x*mnożnik, y* mnożnik, 0), Quaternion.identity);
                o.transform.parent = transform;
                o.transform.localScale = new Vector3(mnożnik, mnożnik, 1);
               
            }
        }
        //Tworzenie granicy 
            //Dół
        int y_1 = -1;
       for(int x = -1; x < rozmiar_mapy + 1; x++) 
        {
            var o = Instantiate(kafelki[0], new Vector3(x * mnożnik, y_1 * mnożnik, 0), Quaternion.identity);
            o.transform.parent = transform;
            o.transform.localScale = new Vector3(mnożnik, mnożnik, 1);
        }
            //Góra
        y_1 = rozmiar_mapy;
        for (int x = -1; x < rozmiar_mapy + 1; x++)
        {
            var o = Instantiate(kafelki[0], new Vector3(x * mnożnik, y_1 * mnożnik, 0), Quaternion.identity);
            o.transform.parent = transform;
            o.transform.localScale = new Vector3(mnożnik, mnożnik, 1);
        }
        //Lewo 
        int x_1 = -1;
        for (int y = -1; y < rozmiar_mapy + 1; y++)
        {
            var o = Instantiate(kafelki[0], new Vector3(x_1 * mnożnik, y * mnożnik, 0), Quaternion.identity);
            o.transform.parent = transform;
            o.transform.localScale = new Vector3(mnożnik, mnożnik, 1);
        }
        //Prawo 
        x_1 = rozmiar_mapy;
        for (int y = -1; y < rozmiar_mapy + 1; y++)
        {
            var o = Instantiate(kafelki[0], new Vector3(x_1 * mnożnik, y * mnożnik, 0), Quaternion.identity);
            o.transform.parent = transform;
            o.transform.localScale = new Vector3(mnożnik, mnożnik, 1);
        }
        //Wymyśliłem sobie żeby uprościć sobie życie jako programista :p 
        //Stół (chce stół !!!) będzie pojawiał się na środku mapy ^^ 
        //Spawn gipcia - dolny lewy róg mapy 
        //Spawn szczóra prawy górny róg mapy 
        //Bo losowanie na całej mapie ich pozycji i sprawdzanie czy nie są za blisko siebie może było by lepsze ale ... więcej możliwości na błedy :P 

        //Spawnowanie stołu na środku mapy :P 
        if (czy_tworzyć_stół)
        {
            Vector3 pozyjca_stołu = new Vector3(rozmiar_mapy * mnożnik / 2 - mnożnik / 2, rozmiar_mapy * mnożnik / 2 - mnożnik / 2, 0);
            var s = Instantiate(stół, pozyjca_stołu, Quaternion.identity);
        }
        //Teleport gipcio w lewy dół mapy !!! 
        GameObject.Find("Gipcio").transform.position = transform.position;
        //Teleport Szczura w lewy górny róg mapy !!! 
        GameObject.Find("Rat").transform.position = transform.position + new Vector3((rozmiar_mapy-1)*mnożnik,(rozmiar_mapy-1)*mnożnik,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
