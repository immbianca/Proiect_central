using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 11f;

    public GameObject gameOverUI;
    public GameObject panelUI;
    public GameObject npcUI;
    public GameObject winUI;

    private Rigidbody2D rb;
    private Animator animator;
    private bool grounded = true;
    private bool isDead = false;
    private bool isBlocking = false;
    private bool isAttacking = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    
        if (animator.runtimeAnimatorController == null)
        {
            Debug.LogWarning("Animator RuntimeAnimatorController missing! Please assign one.");
        }
    }

    private void Start()
    {
      
        if (CaracterManager.instance != null)
        {
            CaracterManager.instance.ApplyCaracter();
        }
    }

    void Update()
    {
        if (isDead) return;

        float horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (horizontal > 0.01f)
            transform.localScale = new Vector3(0.7f, 0.7f, 1);
        else if (horizontal < -0.01f)
            transform.localScale = new Vector3(-0.7f, 0.7f, 1);

        animator.SetBool("walk", Mathf.Abs(horizontal) > 0.01f);
        animator.SetBool("ground", grounded);

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            isBlocking = true;
            AudioManager.instance?.PlaySFX(AudioManager.instance.attackSound);
            animator.SetTrigger("block");
            StartCoroutine(ResetBlock());
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            isAttacking = true;
            AudioManager.instance?.PlaySFX(AudioManager.instance.attackSound);
            animator.SetTrigger("attack");
            StartCoroutine(ResetAttack());
        }

        if (transform.position.y < -10f)
        {
            Die();
        }
    }

    private IEnumerator ResetBlock()
    {
        yield return new WaitForSeconds(0.5f);
        isBlocking = false;
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.3f);
        isAttacking = false;
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        AudioManager.instance?.PlaySFX(AudioManager.instance.jumpSound);
        animator.SetTrigger("jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            grounded = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isDead) return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyMovement enemy = collision.gameObject.GetComponent<EnemyMovement>();
            if (enemy == null) return;

            if (isAttacking && !enemy.IsDead())
            {
                enemy.Die();
            }
            else if (enemy.IsAttacking() && !isBlocking && !isDead)
            {
                Die();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spikes"))
        {
            Die();
        }

        if (collision.CompareTag("NPC"))
        {
            if (npcUI != null) npcUI.SetActive(true);
        }

        if (collision.CompareTag("NLC"))
        {
            SceneManager.LoadScene("Level1");
        }

        if (collision.CompareTag("Win"))
        {
            AudioManager.instance?.PlaySFX(AudioManager.instance.winSound);
            if (winUI != null) winUI.SetActive(true);
            Time.timeScale = 0f;
        }

        if (collision.CompareTag("Enemy"))
        {
            AudioManager.instance?.PlaySFX(AudioManager.instance.hurtSound);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            if (npcUI != null) npcUI.SetActive(false);
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;
        AudioManager.instance?.PlaySFX(AudioManager.instance.deathSound);
        animator.SetTrigger("die");
        StartCoroutine(HandleDeath());
    }

    private IEnumerator HandleDeath()
    {
        yield return new WaitForSecondsRealtime(0.75f);
        if (gameOverUI != null) gameOverUI.SetActive(true);
        if (panelUI != null) panelUI.SetActive(false);
        Time.timeScale = 0f;
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }

    public bool IsBlocking()
    {
        return isBlocking;
    }

    public bool IsDead()
    {
        return isDead;
    }
}
