using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public string typ_przegranej = "brak";

    public Texture2D cukierek_cursor;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        //Zmiana kursora na cukierka 
       // zmień_kursor(cukierek_cursor);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void zmień_kursor(Texture2D cursor) 
    {
       // Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

}
