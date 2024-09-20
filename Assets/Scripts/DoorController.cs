using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator animator;
    private bool isPlayerNear = false;
    private bool isOpen = false; // Controla si la puerta está abierta o cerrada

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (!isOpen)
            {
                animator.SetTrigger("Open"); // Abre la puerta
            }
            else
            {
                animator.SetTrigger("Close"); // Cierra la puerta
            }
            isOpen = !isOpen; // Cambia el estado de la puerta
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
        {
            isPlayerNear = false;
        }
    }
}
