using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float vitezaDeMers = 5f;
    [SerializeField] private float fortaSaritura = 8f;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject panelUI;
    private Rigidbody2D corp;
    private Animator animatie;
    private bool groundCheck = false;

    private void Awake()
    {
        corp = GetComponent<Rigidbody2D>();
        animatie = GetComponent<Animator>();
    }

    private void Update()
    {
        float orizontal = Input.GetAxis("Horizontal");
        corp.velocity = new Vector2(orizontal * vitezaDeMers, corp.velocity.y);

        if (orizontal > 0.01f)
            transform.localScale = new Vector3(0.7f, 0.7f, 1);
        else if (orizontal < -0.01f)
            transform.localScale = new Vector3(-0.7f, 0.7f, 1);

        if (Input.GetKeyDown(KeyCode.Space) && groundCheck)
        {
            jump();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            animatie.SetBool("attack", true);
            Invoke(nameof(ResetAttack), 0.3f);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            animatie.SetBool("block", true);
            Invoke(nameof(ResetBlock), 0.3f);
        }

        if (transform.position.y < -10f)
        {
            gameOverUI.SetActive(true);
            panelUI.SetActive(false);
        }

        animatie.SetBool("walk", orizontal != 0);
        animatie.SetBool("ground", groundCheck);
    }

    private void jump()
    {
        corp.velocity = new Vector2(corp.velocity.x, fortaSaritura);
        // Nu setăm groundCheck aici — îl setăm doar la coliziune
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            groundCheck = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            groundCheck = false;
        }
    }

    private void ResetAttack()
    {
        animatie.SetBool("attack", false);
    }

    private void ResetBlock()
    {
        animatie.SetBool("block", false);
    }
}
