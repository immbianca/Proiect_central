using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject panelUI;
    [SerializeField] private GameObject npcUI;
    private Rigidbody2D corp;
    private Animator animatie;
    private bool grounded;

    private void Awake()
    {
        corp = GetComponent<Rigidbody2D>();
        animatie = GetComponent<Animator>();
    }

    private void Update()
    {
        float orizontal = Input.GetAxis("Horizontal");
        corp.velocity = new Vector2(orizontal * speed, corp.velocity.y);

        if (orizontal > 0.01f)
            transform.localScale = new Vector3(0.7f, 0.7f, 1);
        else if (orizontal < -0.01f)
            transform.localScale = new Vector3(-0.7f, 0.7f, 1);

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Jump();
        }

        animatie.SetBool("walk", orizontal != 0);


        if (Input.GetKeyDown(KeyCode.Z))
        {
            animatie.SetTrigger("block");
            AudioManager.instance.PlaySFX(AudioManager.instance.attackSound);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            animatie.SetTrigger("attack");
            AudioManager.instance.PlaySFX(AudioManager.instance.attackSound);
        }


        animatie.SetBool("ground", grounded);

      
        if (transform.position.y < -10f)
        {
            gameOverUI.SetActive(true);
            panelUI.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spikes")
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.deathSound);
            StartCoroutine(HandleDeath());
        }

        if (collision.gameObject.tag == "NPC")
        {
            npcUI.SetActive(true);

        }

        if (collision.gameObject.tag == "NLC")
        {
            SceneManager.LoadScene("Level1");

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            npcUI.SetActive(false);
        }
    }

    private IEnumerator HandleDeath()
    {
        animatie.SetTrigger("die");
        gameOverUI.SetActive(true);
        yield return new WaitForSecondsRealtime(0.75f);
        Time.timeScale = 0f;
    }

    private void Jump()
    {
        corp.velocity = new Vector2(corp.velocity.x, 10.3f);
        animatie.SetTrigger("jump");
        AudioManager.instance.PlaySFX(AudioManager.instance.jumpSound);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }
}
