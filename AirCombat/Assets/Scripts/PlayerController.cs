using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb = null;

    [SerializeField] private float moveSpeed = 350f;
    private float inputX;
    private float inputY;
    private Vector2 inputPlayerMove;

    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] private float playerFireRate = 1f;
    private bool playerCanShoot = true;

    [SerializeField] private GameObject weaponUpgradePrefab = null;
    [SerializeField] private Text healthTextBox = null;
    [SerializeField] private Text upgradeTextBox = null;
    private int upgradeCount = 0;

    public int playerHealth = 3;
    void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        inputPlayerMove = new Vector2(inputX, inputY);

        if (Input.GetKey(KeyCode.Space) && playerCanShoot)
        {
            playerCanShoot = false;
            StartCoroutine(PlayerShooting());
        }

        PlayerMove();
        PlayerHealthManager();
    }

    private void PlayerMove()
    {
        Vector2 move = inputPlayerMove * moveSpeed * Time.deltaTime;
        rb.velocity = move;
    }

    private IEnumerator PlayerShooting()
    {
            bulletPrefab.transform.position = rb.transform.position;
            Instantiate(bulletPrefab);
            yield return new WaitForSeconds(playerFireRate);
            playerCanShoot=true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (weaponUpgradePrefab.CompareTag(collision.gameObject.tag))
        {
            upgradeCount++;
            upgradeTextBox.text = upgradeCount.ToString();
            playerFireRate -= 0.01f;
            Destroy(collision.gameObject);
        }
    }

    private void PlayerHealthManager()
    {
        if (playerHealth > 0)
        {
            healthTextBox.text = playerHealth.ToString();
        }
        else if (playerHealth < 1)
        {
            SceneManager.LoadScene(2);
            PlayerPrefs.SetInt("PlayerPointsSave", LevelManager.instance.playerPoints);
        }
    }
}
