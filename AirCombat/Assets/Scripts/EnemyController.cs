using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private float moveSpeed = 200f;
    [SerializeField] private int enemyPointsValue = 1;
    public int enemyHealth = 2;
    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] private float delayBetweenFire = 1f;
    [SerializeField] private float fireRateVariable = 0.25f;
    [SerializeField] private GameObject destroyEffect = null;

    private float fireRateDeviation = 0;
    private bool canShoot = true;

    void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = moveSpeed * Time.deltaTime * transform.up;

        if (rb.position.y < 3 && canShoot)
        {
            canShoot = false;
            StartCoroutine(EnemyShooting());
        }

        if (enemyHealth < 1)
        {
            LevelManager.instance.ChangePlayerPoints(enemyPointsValue);
            Destroy(this.gameObject);
        }

    }

    private IEnumerator EnemyShooting()
    {
        bulletPrefab.transform.position = rb.transform.position;
        Instantiate(bulletPrefab);
        fireRateDeviation = Random.Range(-fireRateVariable, fireRateVariable);
        yield return new WaitForSeconds(delayBetweenFire + fireRateDeviation);
        canShoot = true;
    }

    private void OnDestroy()
    {
        if (!this.gameObject.scene.isLoaded) return;
        Instantiate(destroyEffect, this.gameObject.transform.position, Quaternion.identity);
    }
}
