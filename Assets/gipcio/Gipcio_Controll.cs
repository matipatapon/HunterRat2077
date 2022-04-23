using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gipcio_Controll : MonoBehaviour
{
    //Globalne zmienna gipciorucha 
     //Rigidbody 
     Rigidbody2D rb;
    //Ilość punktów 
    public int punkty = 0;
    //Obiekt gdzie wpisać punkty trzeba 
    public GameObject licznik;
    public GameObject Controller;
    //Ekran przegranej 
    public GameObject przegrana_ekran;

    // Start is called before the first frame update
    
    void Start()
    {
        //Pozyskiwanie rb 
        rb = gameObject.GetComponent<Rigidbody2D>();
        //Controller
        Controller = GameObject.Find("Controller");
      


    }

    // Update is called once per frame
    void Update()
    {
        var c = Controller.GetComponent<Controller>();
        if (punkty < 0 && c.typ_przegranej =="brak") c.typ_przegranej = "punkty" ;
        if (c.typ_przegranej != "brak") 
        { 
             przegrana(); 
        }
        //Pauzowanie gry 
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            GetComponent<Pauza>().pauza();
        }
        
       
    }
    bool czy_jest_sterowanie = true;
    private void FixedUpdate()
    {
        if(czy_jest_sterowanie)sterowanie();
    }
    //Sterowanie Graczem
        //Przyspieszenie
        public float przyspieszenie = 1;
        //Maksymalna prędkość 
        public float max_velocity = 4;
        //Prędkość obracania Gipciem 
        public float obrót = 100;
        //Czy Gipcio jest nieśmiertelny ???
        public bool nieśmiertelność = false;
        
    void sterowanie() 
    {
       /* //Ruch w przód i w tył gipcia 
        if (Input.GetKey(KeyCode.W)&&max_velocity>rb.velocity.x) 
        {
            rb.AddRelativeForce(new Vector2(0, przyspieszenie));
           
        }
        if (Input.GetKey(KeyCode.S) && max_velocity > -rb.velocity.x)
        {
            rb.AddRelativeForce(new Vector2(0, -przyspieszenie));
        }

        //Obrót Gipcia 
        /*     //Resetowanie obrotu gipcia
             rb.angularVelocity = 0;
         if (Input.GetKey(KeyCode.A)) 
         {
             //rb.AddTorque(obrót);
             rb.angularVelocity = obrót;

         }
         if (Input.GetKey(KeyCode.D))
         {
             //rb.AddTorque(-obrót);
             rb.angularVelocity = -obrót;
         }
         */
         
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
       // Debug.Log(Mathf.Abs(dir.x) + Mathf.Abs(dir.y));
        if (Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y)<max_velocity&&Mathf.Abs(dir.x)+Mathf.Abs(dir.y)>10) 
        {
            rb.AddRelativeForce(new Vector2(0, przyspieszenie));
        }
        
        
        
    }
    public void aktualizuj_punkty(int ilość) 
    {
        punkty += ilość;
        licznik.GetComponent<Text>().text = "Score : "+punkty.ToString();
    }

    int przegrana_krok = 0;
    void przegrana()
    {
        //Jeśli jednak gipcio nie chce umierać 
        if (nieśmiertelność) return;
        //Jeśli punkty spadły do 0 
        var typ_przegranej = GameObject.Find("Controller").GetComponent<Controller>().typ_przegranej;
        switch (przegrana_krok)
        {
            case 0:
            switch (typ_przegranej)
            {
                case "laser":
                    GetComponent<Animator>().SetInteger("śmierć", 1);
                        rb.freezeRotation = true;
                        transform.rotation = Quaternion.identity;
                       
                        
                        

                        break;
                case "jedzonko":
                        GetComponent<Animator>().SetInteger("śmierć", 2);
                        break;
                case "szczur":
                        GetComponent<Animator>().SetInteger("śmierć", 3);
                        break;
                case "punkty":
                        GetComponent<Animator>().SetInteger("śmierć", 2);
                        break;
               
                  

            }
                przegrana_krok++;
                czy_jest_sterowanie = false;
                //Zatrzymywanie szczura zniszczenia 
                var Rat = GameObject.Find("Rat");
                Rat.GetComponent<Rat_Controler>().limit_velocity = 0;
                Rat.GetComponent<Animator>().SetInteger("przegrana", 1);
                przegrana_ekran.SetActive(true);
                break;
        }   

        
    }
}
