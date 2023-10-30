using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBox : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private GameObject soundPrefab = null;
    [SerializeField] private float moveSpeed = 50f;
    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.velocity = moveSpeed * Time.deltaTime * -transform.up;
    }

    private void OnDestroy()
    {
        if (!this.gameObject.scene.isLoaded) return;
        Instantiate(soundPrefab);
    }
}
