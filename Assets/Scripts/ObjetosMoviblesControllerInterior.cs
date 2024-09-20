using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetosMoviblesController : MonoBehaviour
{
    public GameObject ObjetoAmover;
    public Transform StartPoint;
    public Transform EndPoint;

    public float velocidad;

    private Vector3 MoverHacia;

    void Start()
    {
        MoverHacia= EndPoint.position;
    }

    void Update()
    {
        ObjetoAmover.transform.position=Vector3.MoveTowards(ObjetoAmover.transform.position,MoverHacia, velocidad*Time.deltaTime);
        if(ObjetoAmover.transform.position==EndPoint.position)
        {
            MoverHacia = StartPoint.position;
        }
        if (ObjetoAmover.transform.position == StartPoint.position)
        {
            MoverHacia = EndPoint.position;
        }
    }
}
