using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player; //posicion en la que esta el jugador
    public float detectionRadius = 5.0f; //para que se mueva unicamente si esta cerca de el jugador
    public float speed = 2.0f; //velocidad con la cual se movera hacia el jugador

    private Rigidbody2D rb; // para moverlo
    private Vector2 movement; //direccion hacia donde se va a mover (-1 <- y -> +1)
    private bool enMovimiento;
    private Animator animator;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //obtener el animator del enemigo
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        //distaceToPlayer -> almacena la distancia que tiene el enemigo hacia el jugador
        //Distance -> recibe 2 vectores a y b la cual nos devolvera la distancia entre estas posiciones (posicion del enemigo y la posicion del jugador)
        float distaceToPlayer = Vector2.Distance(transform.position, player.position);

        //verificamos si la distancia del jugador es menor que el radio de deteccion que se creo en la parte superior
        if (distaceToPlayer < detectionRadius)
        {
            // se le resta posiciones entre el jugador y nosotros (esto dará valores - o + dependiendo en la direccion en la que el jugador se encuentre)
            Vector2 direction = (player.position - transform.position).normalized;
            //Para que gire el enemigo
            if (direction.x < 0) 
            {
                transform.localScale = new Vector3(1,1,1);
            }
            if (direction.x > 0) 
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }


            //a esta variable movement se le dice que va a ser igual a un vector2 en el cual unicamente nos moveremos de izq o derecha por lo tanto se le envia la direccion q se obtuvo en el eje x y el segundo eje 0
            movement = new Vector2(direction.x, 0);

            enMovimiento = true;
        }
        else 
        {
            //si el jugador sale de la deteccion del enemigo entonces no deberá moverse
            movement = Vector2.zero;

            enMovimiento = false;
        }

        //para mover al enemigo se debe usar su RigidBody y la funcion MovePosition la cual moverá el cuerpo hacía una direccion, como parámettro va a recibir unicamente un vector2 el cual va a ser una posicion q lo que hara sera mover a nuestro enemigo hacia cierta posicion..
        // para ello le enviamos el vector de nuestra posicion + la direccion del movimiento * la velocidad * time
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);

        animator.SetBool("enMovimiento", enMovimiento);
    }

    //para saber hasta donde llegue su rango
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
