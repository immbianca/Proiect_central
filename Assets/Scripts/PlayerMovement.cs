using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject panelUI;
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
        animatie.SetBool("block", Input.GetKey(KeyCode.Z));
        animatie.SetBool("attack", Input.GetKey(KeyCode.X));
        animatie.SetBool("ground", grounded);


        if (transform.position.y < -10f)
        {
            gameOverUI.SetActive(true);
            panelUI.SetActive(false);
        }
    }

    private void Jump()
    {
        corp.velocity = new Vector2(corp.velocity.x, 10.3f);
        animatie.SetTrigger("jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;

    }


}
