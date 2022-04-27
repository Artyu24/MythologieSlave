using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 movementInput;
    private Rigidbody2D rb;

    public float speed = 5;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Debug.Log("ahsbd");
        rb.MovePosition(rb.position + movementInput * Time.fixedDeltaTime * speed);
        if (movementInput != Vector2.zero && GameManager.GetGameState == GameManager.GameState.Tuto && GameManager.GetTutorialState == GameManager.TutorialState.Mouvement)
            GameManager.UpdateTutorial();
    }

    public void OnMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();
}
