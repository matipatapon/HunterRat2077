using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser_control : MonoBehaviour
{
    //Przyspieszenie 
    public float przyspieszenie = 10;
    //RigidBody
    Rigidbody2D rb;
    //Kąt rozrzutu 
    public float kąt_rozrzut = 5;
    //Max kąt rozrzutu 
    public float max_kąt_rozrzut = 90;
    //Gipcio
    GameObject Gipcio;
    //Prędkość maksymalna
    public float max_velocity = 40;
    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        Gipcio = GameObject.Find("Gipcio");
        skalowanie();
        //Na początku przejmij prędkośc początkową od rodzica jeśli takiego rodzica posiada
        if (transform.parent != null)
        {
            //rb.velocity = transform.parent.GetComponent<Rigidbody2D>().velocity;
            //Następnie go porzuć
            transform.parent = null;
            //Rozrzut
            //Debug.Log("Inicjowanie rozrzutu xd");
            float rozrzut = Random.Range(-kąt_rozrzut, kąt_rozrzut);
            //Debug.Log("Kąt odchylenia " + rozrzut);
            transform.Rotate(0, 0, rozrzut);
        }
        else 
        {
            //Debug.Log("nie ma");
        }
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Przyspieszanie lasera 
        if(Mathf.Abs(rb.velocity.x)+ Mathf.Abs(rb.velocity.y)<max_velocity) rb.AddRelativeForce(new Vector3(0, przyspieszenie, 0));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Jeśli pocisk trafi w granice mapy ma zniknąć dlaczego ??? nie chce nieskończoności pocisków :P 
        if (collision.gameObject.name == "Granica") Destroy(gameObject);
        //Jeśli trafi gipcia wtedy gipcio przegrał ;p 
        var c = GameObject.Find("Controller").GetComponent<Controller>();
        if (collision.gameObject.name == "Gipcio" && c.typ_przegranej == "brak") c.typ_przegranej = "laser";
        //Jeśli obiekt trafił w jedzenie
        if (collision.gameObject.name == "Jedzonko(Clone)"||collision.name == "Jedzonko") 
        {
            if (collision.GetComponent<Jedzonko_Controler>().spalenie()) Destroy(gameObject);
        }
    
    }

    //Czym więcej punktów gracz zdobędzie tym cięższa gra się staje :P tu zwiększa prędkość i kat rozrzutu ^^
    void skalowanie() 
    {
        float punkty = Gipcio.GetComponent<Gipcio_Controll>().punkty;
        kąt_rozrzut += punkty / 1000;
        if (kąt_rozrzut > max_kąt_rozrzut) kąt_rozrzut = max_kąt_rozrzut;
        max_velocity += punkty / 10000;
        
    }

}
