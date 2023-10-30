using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBox : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private GameObject playerPrefab = null;
    [SerializeField] private GameObject soundPrefab = null;
    [SerializeField] private float moveSpeed = 50f;
    [SerializeField] private int healAmount = 1;

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.velocity = moveSpeed * Time.deltaTime * -transform.up;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == playerPrefab.name)
        {
            if(collision.GetComponent<PlayerController>().playerHealth < 5)
            {
                collision.GetComponent<PlayerController>().playerHealth += healAmount;
            }

            Instantiate(soundPrefab);
            Destroy(this.gameObject);
        }
    }
}
