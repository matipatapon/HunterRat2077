using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jedzenie_Spawn : MonoBehaviour
{
    //Funkcja ta tworzy podaną ilość jedzenia na mapie ! ! ! Gipcio głodny nie będzie 
    public int ile = 1;
    //PreFab jedzenia 
    public GameObject jedzenie;
    // Start is called before the first frame update
    void Start()
    {
        //Muszę podzielić respienie się obiektów bo ... umiera ta pętla : P 
       /* for (int i = 0; i < ile; i++)
        {
            var o = Instantiate(jedzenie);
            o.transform.parent = transform;
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        //Resp 1 jedzenie na klatke fakt odrazu wszystko się nie zrespi ale :P no co mogę począc 
        
        if(ile>0) 
        {
            var o = Instantiate(jedzenie);
            o.transform.parent = transform;
            ile--;
        }
            
    }
}
