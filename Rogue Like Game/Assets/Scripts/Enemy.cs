using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int damage;

    [SerializeField] GameObject deathDropPrefab;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float attackChance = 0.5f;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            if (deathDropPrefab != null)
            {
                Instantiate(deathDropPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }

        StartCoroutine("DamageFlash");

        if (Random.value > attackChance)
        {
            player.TakeDamage(damage);
        }
    }

    IEnumerator DamageFlash()
    {
        Color defaultColor = spriteRenderer.color;
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = defaultColor;
    }

    public void Move()
    {
        if (Random.value < 0.5f)
        {
            return;
        }

        Vector3 direction = Vector3.zero;
        bool canMove = false;

        while (canMove == false)
        {
            direction = GetRandomDirection();

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1.0f, layerMask);

            if (hit.collider == null)
            {
                canMove = true;
            }
        }

        transform.position += direction;
    }

   
    Vector3 GetRandomDirection()
    {
        int random = Random.Range(0,4);

        if (random == 0)
        {
            return Vector3.up;
        }
        else if (random == 1)
        {
            return Vector3.down;
        }
        else if (random == 2)
        {
            return Vector3.left;
        }
        else if (random == 3)
        {
            return Vector3.right;
        }
        return Vector3.up;
    }
}
