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

    }

    public void OnAttackDown(InputAction.CallbackContext context)
    {

    }

    public void OnAttackLeft(InputAction.CallbackContext context)
    {

    }

    public void OnAttackRight(InputAction.CallbackContext context)
    {

    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
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
}
