using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    public int currentHP;
    public int maxHp;
    public int coins;
    public bool hasKey;
    public LayerMask moveLayerMask;

    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Move(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, moveLayerMask);

        if (hit.collider == null)
        {
            transform.position += new Vector3(direction.x, direction.y, 0);
            EnemyManager.instance.OnPlayerMove();
        }
    }

   public void OnMoveUp(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Move(Vector2.up);
        }
    }

    public void OnMoveDown(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Move(Vector2.down);
        }
    }

    public void OnMoveLeft(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Move(Vector2.left);
        }
    }

    public void OnMoveRight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Move(Vector2.right);
        }
    }

    public void OnAttackUp(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            TryAttack(Vector2.up);
        }
    }

    public void OnAttackDown(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            TryAttack(Vector2.down);
        }

    }

    public void OnAttackLeft(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            TryAttack(Vector2.left);
        }
    }

    public void OnAttackRight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            TryAttack(Vector2.right);
        }
    }

    void TryAttack(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, 1 << 7);

        if (hit.collider)
        {
            hit.transform.GetComponent<Enemy>().TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        UI.instance.UpdateHealth(currentHP);
        StartCoroutine(DamageFlash());

        if (currentHP <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator DamageFlash()
    {
        Color defaultColor = spriteRenderer.color;
        spriteRenderer.color = Color.white;

        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = defaultColor;
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UI.instance.UpdateCoinText(coins);
    }

    public bool AddHealth(int amount)
    {
        if (currentHP + amount <= maxHp)
        {
            currentHP += amount;
            UI.instance.UpdateHealth(currentHP);
            return true;
        }

        return false;
    }
}
