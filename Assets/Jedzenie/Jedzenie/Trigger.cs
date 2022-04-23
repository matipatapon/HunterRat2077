using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    //Miałem problem z tym że żeby miec Kolizje w jedzonku to po dezaktywowaniu jej (bo obiekt się teleportuje i jest niewidzialny ) wychwytywał mi że 
    //Oo ooo obiekt wyszedł z kolizji więc przeniosłem kolizje do innego obiektu ^^ 
        
    private void OnTriggerEnter2D(Collider2D other)
    {
        transform.parent.transform.parent.GetComponent<Jedzonko_Controler>().Trigger_enter(other);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        transform.parent.transform.parent.GetComponent<Jedzonko_Controler>().Trigger_exit(other);
    }

}
