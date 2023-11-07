using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] private float timeToDestroy;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("DestroyBullet");
    }

    void FixedUpdate()
    {
        rb.velocity = this.transform.up * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.CompareTag("Player"))
        {
            if (collision.transform.CompareTag("Enemie"))
            {
                Destroy(collision.gameObject);
                StopCoroutine("DestroyBullet");
            }
            Destroy(this.gameObject);
        }
        
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Debug.LogWarning("Coroutine didn't stop");
        Destroy(this.gameObject);
    }
}
