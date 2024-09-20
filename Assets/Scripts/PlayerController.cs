using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mov : MonoBehaviour
{
    public float velocidad = 5f;

    public float fuerzaSalto = 10f;
    public float longitudRaycast = 0.1f; //la línea roja
    public LayerMask capaSuelo;

    private bool enSuelo;
    private Rigidbody2D rb;

    public Animator animator;


    public int indiceNivel;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movimiento horizontal
        float velocidadX = Input.GetAxis("Horizontal") * Time.deltaTime * velocidad;

        // Control de animación (si usas animaciones)
        animator.SetFloat("movement", velocidadX * velocidad);

        // Cambiar dirección según la tecla presionada
        if (velocidadX < 0)
        {
            // Mira a la izquierda
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (velocidadX > 0)
        {
            // Mira a la derecha
            transform.localScale = new Vector3(1, 1, 1);
        }

        // Movimiento del personaje (izquierda/derecha)
        Vector3 posicion = transform.position;
        transform.position = new Vector3(velocidadX + posicion.x, posicion.y, posicion.z);

        // Comprobar si está en el suelo (//posición del jugador, mirar hacia abajo, va a ser tmñ de la long raycast que se asignó, busca colisionar con la capa suelo)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitudRaycast, capaSuelo);
        //si la line acolisiona con el suelo sería verdadero, en cambio si la linea no esta colisionando con el suelo eso va a ser falso
        enSuelo = hit.collider != null;

        // Salto
        if (enSuelo && Input.GetKeyDown(KeyCode.Space))
        {
            //al rb se le añade una fuerza que va a ser igual al vector2 que en x va a ser 0 y en 'y' se pone la fuerza del salto
            rb.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
        }

        animator.SetBool("ensuelo", enSuelo);
    }

    void OnDrawGizmos() //figura imaginarias que unicamente se van a ver en el editor más no en el juego
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitudRaycast);
                        //De donde nace el vecor - hacia donde va
    }

    public void cambiarNivel(int indice)
    {
        SceneManager.LoadScene(indice);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PuertaExterior")
        {
            indiceNivel=1;
            cambiarNivel(indiceNivel);
        }
    }

    //private void //OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "PuertaExterior")
    //    {

    //    }
    //}

}


