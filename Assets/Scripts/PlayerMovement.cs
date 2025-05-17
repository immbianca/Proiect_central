using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float viteza;
    private Rigidbody2D corp;

    private void Awake()
    {
        corp = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float orizontal = Input.GetAxis("Horizontal");
        corp.velocity = new Vector2(orizontal * viteza, corp.velocity.y);

        //Flip caracter
        if (orizontal > 0.01f)
            transform.localScale = new Vector3(0.7f, 0.7f, 1);
        else if (orizontal < -0.01f)
            transform.localScale = new Vector3(-0.7f, 0.7f, 1);

        if(Input.GetKey(KeyCode.Space))
            corp.velocity = new Vector2(corp.velocity.x, viteza);

    }
}
