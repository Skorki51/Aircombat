using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private float bulletSpeed = 1000f;
    [SerializeField] private int bulletSpeedVariable = 100;
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private bool IsAPlayerBullet = false;

    private void Awake()
    {
        bulletSpeed += Random.Range(-bulletSpeedVariable/2, bulletSpeedVariable/2);
        bulletDamage *= -1;
    }

    private void FixedUpdate()
    {
        if (IsAPlayerBullet)
        {
            rb.velocity = bulletSpeed * Time.deltaTime * transform.up;
        }
        else
        {
            rb.velocity = bulletSpeed * Time.deltaTime * -transform.up;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            if (!IsAPlayerBullet)
            {
                collision.GetComponent<PlayerController>().playerHealth += bulletDamage;
                Instantiate(hitEffect, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
        if (collision.GetComponent<EnemyController>() != null)
        {
            if (IsAPlayerBullet)
            {
                collision.GetComponent<EnemyController>().enemyHealth += bulletDamage;
                Instantiate(hitEffect, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }
}

