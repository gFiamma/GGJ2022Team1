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

    [SerializeField]
    SpriteRenderer playerSprite;
    [SerializeField]
    Sprite Left, Right, Up, Down;
    //componenti
    Rigidbody2D rb2D;

    //variabili locali
    public Vector2 DirMovimento;

        //variabili utili per la morte del player
    public static bool isDead;

    //variabili per il movimento
    public bool isMoving;
    private Vector3 originPos, targetPos;
    private float timeToMove = 0.2f;
    float movementX;
    float movementY;

    //------------------------------------------------------------------------------------------
    //---------------METODI---------------------------------------------------------------------

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Assegno a movement l'input nell'asse X e Y
        DirMovimento.x = Input.GetAxisRaw("Horizontal");
        DirMovimento.y = Input.GetAxisRaw("Vertical");

        //if (Input.GetAxisRaw("Horizontal") > 0)
        //{
        //    playerSprite.sprite = Right;
        //}
        //if (Input.GetAxisRaw("Horizontal") < 0)
        //{
        //    playerSprite.sprite = Left;
        //}
        //if (Input.GetAxisRaw("Vertical") > 0)
        //{
        //    playerSprite.sprite = Up;
        //}
        //if (Input.GetAxisRaw("Vertical") < 0)
        //{
        //    playerSprite.sprite = Down;
        //}

        //rob
        if (DirMovimento.y == 0 && DirMovimento.x == 0)
        {
            // Reimposta il valore se non è stato rilevato alcun input 
            InputConPriorita = PrioritisedInput.None;
        }
        else if (DirMovimento.y != 0 && DirMovimento.x == 0)
        {
            // Ricevi asse X input, ma nessun input dell'asse Y. Priorità Asse y
            InputConPriorita = PrioritisedInput.YAxis;
        }
        else if (DirMovimento.y == 0 && DirMovimento.x != 0)
        {
            // ricevi asse Y input, ma nessun input dell'asse X. Priorità Asse X
            InputConPriorita = PrioritisedInput.XAxis;
        }
        else if (DirMovimento.y != 0 && DirMovimento.x != 0 && InputConPriorita == PrioritisedInput.None)
        {
            // Cosa succede se entrambi gli assi ricevono un input nello stesso frame?
            // Da la priorità all'asse X
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

    //rob
    private void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + DirMovimento * velMovimento * Time.fixedDeltaTime);
    }
}
