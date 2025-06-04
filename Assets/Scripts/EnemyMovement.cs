using System.Collections;
using UnityEngine;
using Cinemachine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameObject Win;
    public Transform player;
    public float detectionRadius = 5f;
    public float attackDistance = 1.5f;
    public float moveSpeed = 2f;
    public Animator animator;

    private bool isDead = false;
    private bool isAttacking = false;

    [Header("Cinemachine Cameras")]
    public CinemachineVirtualCamera enemyCam;
    public CinemachineVirtualCamera playerCam;

    void Update()
    {
        if (isDead || player == null) return;

        if (transform.position.y < -7f)
        {
            Die();
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < detectionRadius)
        {
            if (distance < attackDistance)
            {
                isAttacking = true;
                animator.SetBool("isAttacking", true);
                animator.SetBool("isWalking", false);
            }
            else
            {
                isAttacking = false;
                animator.SetBool("isAttacking", false);
                animator.SetBool("isWalking", true);

                Vector3 direction = player.position - transform.position;
                direction.y = 0f;
                direction.Normalize();
                transform.position += direction * moveSpeed * Time.deltaTime;

                if (direction.x > 0.01f)
                    transform.localScale = new Vector3(0.7f, 0.7f, 1);
                else if (direction.x < -0.01f)
                    transform.localScale = new Vector3(-0.7f, 0.7f, 1);
            }
        }
        else
        {
            isAttacking = false;
            animator.SetBool("isAttacking", false);
            animator.SetBool("isWalking", false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isDead) return;

        if (collision.gameObject.CompareTag("Spikes"))
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.deathSound);
            Die();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement playerScript = collision.gameObject.GetComponent<PlayerMovement>();
            if (playerScript == null) return;

            if (playerScript.IsAttacking() && !isDead)
            {
                AudioManager.instance.PlaySFX(AudioManager.instance.deathSound);
                Die();
            }
            else if (isAttacking && !playerScript.IsAttacking() && !playerScript.IsDead())
            {
                playerScript.Die();
            }
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }

    public void Die()
    {
        if (isDead) return;

        Win.SetActive(true);
        isDead = true;
        AudioManager.instance.PlaySFX(AudioManager.instance.deathSound);
        animator.SetTrigger("die");
        moveSpeed = 0f;

        if (enemyCam != null && playerCam != null)
        {
            enemyCam.Priority = 20;
            playerCam.Priority = 10;
            StartCoroutine(ResetCameraPriority());
        }
    }

    private IEnumerator ResetCameraPriority()
    {
        yield return new WaitForSeconds(2f);
        if (enemyCam != null && playerCam != null)
        {
            enemyCam.Priority = 10;
            playerCam.Priority = 20;
        }
    }
}
