using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat_Controler : MonoBehaviour
{

    // Start is called before the first frame update
    Rigidbody2D rb;
    //Gipcio 
    GameObject Gipcio;
    GameObject Controller;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Gipcio = GameObject.Find("Gipcio");
        Controller = GameObject.Find("Controller");   
    }

    // Przyspieszenie
    public float przyszpieszenie = 100;
    // max velocity startowe 
    public float startowe_max_velocity = 10;
    //Maksymalne velocity do którego może zostać podniesiona ta wartość przez skalowanie 
    public float limit_velocity = 20;
    [SerializeField] float obecne_max_velocity = 10;
    void FixedUpdate()
    {
        //Jeśli gracz przegrał przestań robić co kolwiek :P 
        if (Controller.GetComponent<Controller>().typ_przegranej != "brak") return;

        //Obracanie Szczura w strone gipcia 
        var gipcio = GameObject.Find("Gipcio");
        var v = new Vector2(gipcio.transform.position.x,gipcio.transform.position.y);
        v -= new Vector2(transform.position.x, transform.position.y);
        var angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        //Rozpędzanie Gipcia 
        if (Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y) < obecne_max_velocity)
        {
            rb.AddRelativeForce(new Vector2(0, przyszpieszenie));
        }
    }
    //Co ma zrobić po wjeściu w kolizje ?
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Jeśli to nie gipcio nic nie rób 
        if (collision.gameObject.name != "Gipcio") return;
        //Jeśli to gipcio przegrałeś gre :P 
        if(GameObject.Find("Controller").gameObject.GetComponent<Controller>().typ_przegranej=="brak") GameObject.Find("Controller").gameObject.GetComponent<Controller>().typ_przegranej = "szczur";
        
    }
    private void Update()
    {
        //Jeśli gracz przegrał przestań robić co kolwiek :P 
        if (Controller.GetComponent<Controller>().typ_przegranej != "brak") return; 


        skalowanie();
        laser();
        
    }
    //Funkcja odpowiedzialna za strzelanie szczura z oczu xd 
    public GameObject laser_prefab;
    //Cooldown strzelania laserem 
    public float Cooldown_laser = 1;
    public float start_cooldown_laser = 1;
    public float max_cooldown_laser = 0.25f;
    //odmierzanie czasu do wystrzelenia pocisku 
    float timer_laser = 0; 
    
    void laser() 
    {
        if (Cooldown_laser < timer_laser)
        {
            Instantiate(laser_prefab, transform);
            timer_laser = 0;
            return;
        }
        timer_laser += Time.deltaTime;
    }

    void skalowanie() 
    {
        //Prędkosć
        float punkty = Gipcio.GetComponent<Gipcio_Controll>().punkty;
        obecne_max_velocity = startowe_max_velocity + punkty / 10000;
        if (obecne_max_velocity > limit_velocity) obecne_max_velocity = limit_velocity;
        //Prędkosć nawalania z laserowej dzidy 
        Cooldown_laser = start_cooldown_laser - punkty / 10000;
        if (Cooldown_laser < max_cooldown_laser) Cooldown_laser = max_cooldown_laser;
    }
}
