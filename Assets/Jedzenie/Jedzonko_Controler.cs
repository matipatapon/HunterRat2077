using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SocialPlatforms;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Diagnostics.Contracts;

public class Jedzonko_Controler : MonoBehaviour
{
    // Start is called before the first frame update
    //Lista dostępnych rodzajów jedzenie
    public List<GameObject> jedzenie = new List<GameObject>();
    //Współczynik jaka jest szansa że ten obiekt zostanie wylosowany ^^ 
    public List<int> szansa = new List<int>();
    //Za ile ma się pojawić obiekt ponownie ? 
    float czas;
    public Vector2 zakres_czasu_pojawiania = new Vector2(1, 3);
    //Odlicza czas od upłynięcia 
    [SerializeField] private float timer = 0;
    //Który obiekt jest wylosowany ??? 
    public int który = 0;
    //Defaultowy dźwięk spożywania posiłku przez gipcia
    public AudioClip dźwięk_default;
    //rb
    Rigidbody2D rb;
    //
    GameObject Controller;


    void Start()
    {

        czas = Random.Range(zakres_czasu_pojawiania.x, zakres_czasu_pojawiania.y);
        który = który_losój();
        teleportacja();
        rb = GetComponent<Rigidbody2D>();
        Controller = GameObject.Find("Controller");

    }
    //Określa czy przedmiot jest blisko gipcia czy też nie :P 
    public bool czy_w_polu = false;

    public void Trigger_enter(Collider2D collision)
    {
        //Co ma zrobić jeśli jest w polu gipcia czyt blsiko gipcia ? 
        if (collision.name == "pole")
        {
            //czy_w_polu = true;

        }
        //Jeśli jest gipciem
        if (collision.name != "Gipcio")
        {

            return;
        }

        timer = -1;
        if (transform.childCount != 0)
        {
            //Po co ma się coś dziać jeśli nie ma interakcji 
            if (transform.GetChild(2).GetComponent<Wartosc>().czy_brak_interakcji == true)
            {
                //Debug.Log(transform.GetChild(2).GetComponent<Wartosc>().czy_brak_interakcji);
                return;

            }
            Debug.Log("destroy");
            Destroy(transform.GetChild(2).gameObject);
            który = Random.Range(0, jedzenie.Count);
            teleportacja();
            //dziecko 
            var child = transform.GetChild(2);
            var Wartosc = child.GetComponent<Wartosc>();
            //Sprawdzanie czy obiekt nie one shotuje 
            if (Wartosc.czy_one_shot)
            {
                var c = GameObject.Find("Controller").GetComponent<Controller>();
                c.typ_przegranej = "jedzonko";
            }
            else
            {
                //Dodawanie puntków 
                GameObject.Find("Gipcio").GetComponent<Gipcio_Controll>().aktualizuj_punkty(transform.GetChild(2).GetComponent<Wartosc>().punkty);
            }
            //Otwarzanie odgłosu pożerania świata 
            //if(GameObject.Find("Eat_Sound").GetComponent<AudioSource>().isPlaying!=true) 
            //GameObject.Find("Eat_Sound").GetComponent<AudioSource>().Play();
            var dźwięk = transform.GetChild(2).GetComponent<Wartosc>().dźwięk;
            if (dźwięk == null)
            {
                GameObject.Find("Eat_Sound").GetComponent<AudioSource>().clip = dźwięk_default;
                GameObject.Find("Eat_Sound").GetComponent<AudioSource>().Play();
            }
            else
            {

                GameObject.Find("Eat_Sound").GetComponent<AudioSource>().clip = dźwięk;
                GameObject.Find("Eat_Sound").GetComponent<AudioSource>().Play();
            }
        }
    }
    public void Trigger_exit(Collider2D collision)
    {
        //Mówie ok już możesz pojawiać obiekt bo gipcio go tak szybko nie zje bo nie jest blisko ;p 
        if (collision.name == "pole")
        {
            //czy_w_polu = false;

        }
        //Jeśli ma być brak interakcji nic nie rób 
        if (transform.GetChild(2).GetComponent<Wartosc>().czy_brak_interakcji == true)
        {
            //Debug.Log(transform.GetChild(2).GetComponent<Wartosc>().czy_brak_interakcji);
            return;

        }
        if (collision.name != "Gipcio")
        {
            return;
        }
        timer = 0;
        który = Random.Range(0, jedzenie.Count);
    }

    void Update()
    {
        //Jeśli gracz przegrał przestań robić cokolwiek 
        //if (Controller.GetComponent<Controller>().typ_przegranej != "brak") return;

        if (czy_była_teleportacja>0)czy_była_teleportacja --;
        if (timer != -1)
        {
            timer += Time.deltaTime;
            //Jeśli obiekt jest "niewidoczny" wyłącz kolizje
            GetComponent<CircleCollider2D>().enabled = false;

        }
        else
        {
            GetComponent<CircleCollider2D>().enabled = true;

        }
        if (timer == -1)
        {
            respawn();
        }
        //Debug.Log(timer);
        //Jeśli czas się zakończył
        if (timer > czas && !czy_w_polu)
        {

            czas = Random.Range(zakres_czasu_pojawiania.x, zakres_czasu_pojawiania.y);
            timer = -1;
            
            var o = Instantiate(jedzenie[który], transform.position, transform.rotation);
            o.transform.parent = transform;
            //Debug.Log(który);

        }
        //Wartości kontrolne
        // Debug.Log(timer+"timer");
        // Debug.Log(timer_respawn + "timer respawn");

        

    }
    void teleportacja()
    {
        //Mapa 
        var mapa = GameObject.Find("Mapa_render").gameObject;
        var x_min = 10 + mapa.transform.position.x - mapa.GetComponent<Map_generator>().mnożnik / 2;
        var x_max = -10 + mapa.transform.position.x - mapa.GetComponent<Map_generator>().mnożnik / 2 + mapa.GetComponent<Map_generator>().mnożnik * mapa.GetComponent<Map_generator>().rozmiar_mapy;
        //Debug.Log(x_max);
        //Debug.Log(x_min);
        var y_min = 10 + mapa.transform.position.y - mapa.GetComponent<Map_generator>().mnożnik / 2;
        var y_max = -10 + mapa.transform.position.y - mapa.GetComponent<Map_generator>().mnożnik / 2 + mapa.GetComponent<Map_generator>().mnożnik * mapa.GetComponent<Map_generator>().rozmiar_mapy;
        //Debug.Log(y_max);
        //Debug.Log(y_min);
        transform.position = new Vector3(Random.Range(x_min, x_max), Random.Range(y_min, y_max), transform.position.z);
        czy_była_teleportacja = 10;
    }

    //Jeśli obiekt stoi za długo w 1 miejsu powiedzmy 60s to ma się zresetować 
    //Liczy czas ile upłyneło od ostatniego restartu obiektu ? 
    [SerializeField] private float timer_respawn = 0;
    //Czas do resetu 
    float time_respawn_min = 30;
    float time_respawn_max = 60;
    float time_respawn = 20;
    void respawn(bool czy_respawnic_juz = false)
    {

        if (czy_w_polu)
        {
            timer_respawn = 0;
            //Losowanie za ile ma się zresetować obiekt 
            time_respawn = Random.Range(time_respawn_min, time_respawn_max);
        }
        else
        {
            if(timer == -1) timer_respawn += Time.deltaTime;
            if (timer_respawn > time_respawn || czy_respawnic_juz )
            {
                //Zniszczenie obiektu następnie wylosowanie jaki obiekt ma się pojawić następny przeteleportowanie kaj i zrestowanie liczników 
                //Debug.Log("destroy");
                if(transform.childCount==3)Destroy(transform.GetChild(2).gameObject);
                który = który_losój();
                teleportacja();
                timer_respawn = 0;
                timer = 0;
                czy_spalony = false;
                rb.freezeRotation = false;

            }
        }

    }
    //Losowanie który obiekt ma się spawnowac :P 
    /* Jak to ma działać ??? 
     * Planuje coś takiego : 
     * Powiedzmy że 1 item w liscie ma szanse 5 czyli (start od zera) jeśli liczba w przekroju od 0 do 4 zostanie wylosowana to on jest wybreany 
     * 2 Obiekt ma szanse 2 czyli jeśli liczba od 5 do 6 się wylosuje to on jest wybierany itd 
     */
    int który_losój()
    {
        var ile = jedzenie.Count;
        //Debug.Log(ile+"ile jest jedzonka ???");
        int[] min = new int[ile];
        int[] max = new int[ile];
        var obecna_wartość = 0;
        var koniec_przedziału = 0;
        //Ustawianie przedziałów liczb poszczególnych jedzonek 
        //Debug.Log("Ustawianie przedziałów liczb poszczególnych jedzonek ");
        for (int i = 0; i < ile; i++)
        {
            //Debug.Log("Pętla : " + i);
            var s = szansa[i];
            min[i] = obecna_wartość;
            max[i] = obecna_wartość + s - 1;
            obecna_wartość = max[i] + 1;
            //Debug.Log("Min : " + min[i]);
            //Debug.Log("Max : " + max[i]);



        }

        koniec_przedziału = max[ile - 1];
        //Debug.Log("Koniec przedziału to : " + koniec_przedziału);
        int wylosowany = Random.Range(0, koniec_przedziału);
        //Debug.Log("Wylosowana Wartość : "+wylosowany);

        //Debug.Log("Sprawdzanie do jakiego jedzonka ta wartość została wylosowana");
        for (int i = 0; i < ile; i++)
        {
            if (min[i] <= wylosowany && wylosowany <= max[i])
            {
                //Debug.Log("Wylosowana : " + i);
                return i;
            }
        }

        return 1;
    }

    //Po dostaniu np laserem jedzenko zmieni się w kupkę popiołu zwraca czy udało się wykonać spalanie obiektu
    //Prefab popiołu 
    public GameObject popiół_prefab;
    //Określa czy obiekt zotał spalony
    public bool czy_spalony = false;
    public bool spalenie() 
    {
        //1 rzecz to sprawdzić czy jedzenie jest zrespione czyt czy timer != -1 i transform.GetChild(2)==null :P 
        if (timer != -1 || transform.GetChild(2) == null||czy_spalony) return false;
        //Usuwanie jedzonka które zostało spalone żywcem 
        Destroy(transform.GetChild(2).gameObject);
        //Pojawianie się popiołu 
        var p = Instantiate(popiół_prefab, transform);
        p.transform.parent = transform;
        timer = -1;
        czy_spalony = true;
        //Zresetowanie obrotu
        transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        //Zablokowanie obrotu
        rb.freezeRotation = true;
        
        
        return true;
    }
    int czy_była_teleportacja = 0;
    public bool czy_tepać = false;
    public void OnTriggerStay2D(Collider2D collision)
    {
        
        if (czy_była_teleportacja == 0) 
        {
            if (collision.name == "osobista" && !czy_w_polu)collision.transform.parent.GetComponent<Jedzonko_Controler>().respawn(true);
        }
        //Funkcja która będzie zapobiegała respieniu się obiektów w sobie czyt po przeteleportowaniu obiekt patrzy czy nie jest w kolizji z żadnym z obiektów 
        else if(collision.name == "osobista" &&timer!=-1 || collision.name == "osobista" && czy_w_polu == false) 
        {
            respawn(true);
           
        }
        /*if (collision.name == "Jedzonko(Clone)" && timer != -1 || collision.name == "Jedzonko(Clone)" && czy_w_polu == false && collision.transform.parent.transform.position != transform.position)
        {
            var czy = false;
            if (transform.childCount == 3) czy = true;
            respawn(true, czy);
            Debug.Log("Jedzonko za blisko ! ");
        }
        */
       
    }









}
