using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    [SerializeField] private AudioSource enemyKilledEffect;
    [SerializeField] private float strikeForce;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Animator enemyAnim = collision.gameObject.GetComponent<Animator>();
            enemyKilledEffect.Play();
            rb2D.velocity = new Vector2(rb2D.velocity.x, strikeForce);
            enemyAnim.SetBool("isHit", true);
            Destroy(collision.gameObject, enemyAnim.GetCurrentAnimatorStateInfo(0).length);
        }
    }
}
