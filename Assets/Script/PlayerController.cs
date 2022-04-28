using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 movementInput;
    private Rigidbody2D rb;

    public float speed = 5;

    public bool collideWithTotem;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        DontDestroyOnLoad(this.gameObject);
    }

    void FixedUpdate()
    {
        if (GameManager.gameState != GameManager.GameState.Paused)
        {
            rb.MovePosition(rb.position + movementInput * Time.fixedDeltaTime * speed);
            if (movementInput != Vector2.zero && GameManager.gameState == GameManager.GameState.Tuto && GameManager.tutorialState == GameManager.TutorialState.Mouvement)
                GameManager.UpdateTutorial();
        }
    }

    public void OnMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();

}
