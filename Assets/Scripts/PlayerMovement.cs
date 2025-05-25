using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float viteza;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject panelUI;
    private Rigidbody2D corp;
    [SerializeField] private float groundCheckDistance;

    private void Awake()
    {
        corp = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float orizontal = Input.GetAxis("Horizontal");
        corp.velocity = new Vector2(orizontal * viteza, corp.velocity.y);

        if (orizontal > 0.01f)
            transform.localScale = new Vector3(0.7f, 0.7f, 1);
        else if (orizontal < -0.01f)
            transform.localScale = new Vector3(-0.7f, 0.7f, 1);

        bool isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, LayerMask.GetMask("Ground"));

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            corp.velocity = new Vector2(corp.velocity.x, viteza * 2);

        if (transform.position.y < -10f)
        {
            gameOverUI.SetActive(true);
            panelUI.SetActive(false);

        }
    }


}
