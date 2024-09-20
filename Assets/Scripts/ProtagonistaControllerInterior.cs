using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistaController : MonoBehaviour
{

    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;

    //variables para el salto único
    public float fuerzaSalto=10f;
    public float longitudRaycast = 0.1f;
    public LayerMask capaSuelo;

    private bool enSuelo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("Estado", 0);

        rb.velocity = new Vector2(0, rb.velocity.y);


        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(25, rb.velocity.y);
            sr.flipX = false;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity=new Vector2(-25, rb.velocity.y);
            sr.flipX = true;
        }

        if (rb.velocity.x != 0)
        {
            animator.SetInteger("Estado", 1);
        }

        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, 30);
        //    //falta corregir el salto
        //}

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitudRaycast, capaSuelo);
        enSuelo = hit.collider != null;

        if (enSuelo && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(transform.position, transform.position+Vector3.down* longitudRaycast);
    //}
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitudRaycast);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="PlataformaDinamica1")
        {
            transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlataformaDinamica1")
        {
            transform.parent = null;
        }
    }
}
