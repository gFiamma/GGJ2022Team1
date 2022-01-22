using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMove : MonoBehaviour
{
    public enum PrioritisedInput
    {
        None,
        XAxis,
        YAxis
    }

    public PrioritisedInput InputConPriorita = PrioritisedInput.None;

    //------------------------------------------------------------------------------------------
    //--------------VARIABILI-------------------------------------------------------------------

    [SerializeField]
    public float velMovimento = 5f;

    //componenti
    Rigidbody2D rb2D;

    //variabili locali
    public Vector2 DirMovimento;

    //------------------------------------------------------------------------------------------
    //---------------METODI---------------------------------------------------------------------

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Asseggno a movement l'input nell'asse X e Y
        DirMovimento.x = Input.GetAxisRaw("Horizontal");
        DirMovimento.y = Input.GetAxisRaw("Vertical");

        if (DirMovimento.y == 0 && DirMovimento.x == 0)
        {
            // Reimposta il valore se non � stato rilevato alcun input 
            InputConPriorita = PrioritisedInput.None;
        }
        else if (DirMovimento.y != 0 && DirMovimento.x == 0)
        {
            // Ricevi asse X input, ma nessun input dell'asse Y. Priorit� Asse y
            InputConPriorita = PrioritisedInput.YAxis;
        }
        else if (DirMovimento.y == 0 && DirMovimento.x != 0)
        {
            // ricevi asse Y input, ma nessun input dell'asse X. Priorit� Asse X
            InputConPriorita = PrioritisedInput.XAxis;
        }
        else if (DirMovimento.y != 0 && DirMovimento.x != 0 && InputConPriorita == PrioritisedInput.None)
        {
            // Cosa succede se entrambi gli assi ricevono un input nello stesso frame?
            // Da la priorit� all'asse X
            InputConPriorita = PrioritisedInput.XAxis;
        }

        if (InputConPriorita == PrioritisedInput.XAxis)
        {
            DirMovimento.y = 0;
        }
        else if (InputConPriorita == PrioritisedInput.YAxis)
        {
            DirMovimento.x = 0;
        }
    }


    private void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + DirMovimento * velMovimento * Time.fixedDeltaTime);
    }
}
