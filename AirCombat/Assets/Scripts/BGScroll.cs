using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    public float speed = 4f;
    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        transform.Translate(Vector3.down * speed*Time.deltaTime);

        if(transform.position.y < -25f)
        {
            transform.position = startPosition;
        }
    }
}
