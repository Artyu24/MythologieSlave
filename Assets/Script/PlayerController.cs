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
<<<<<<< HEAD
        if (GameManager.gameState != GameManager.GameState.Paused)
        {
            rb.MovePosition(rb.position + movementInput * Time.fixedDeltaTime * speed);
            if (movementInput != Vector2.zero && GameManager.gameState == GameManager.GameState.Tuto && GameManager.tutorialState == GameManager.TutorialState.Mouvement)
                GameManager.UpdateTutorial();
        }

        float speedAnim = 0f;

        if ((movementInput.x > 0.1f || movementInput.y > 0.1f) || (movementInput.x < -0.1f || movementInput.y < -0.1f))
            speedAnim = 1f;
        else
            speedAnim = 0f;

        animator.SetFloat("Speed",speedAnim);
        animator.SetFloat("Horizontal", movementInput.x);
        animator.SetFloat("Vertical", movementInput.y);
=======
        rb.MovePosition(rb.position + movementInput * Time.fixedDeltaTime * speed);
        if (movementInput != Vector2.zero && GameManager.GetGameState == GameManager.GameState.Tuto && GameManager.GetTutorialState == GameManager.TutorialState.Mouvement)
            GameManager.UpdateTutorial();
>>>>>>> parent of 0738ab4 (Add animations)
    }

    public void OnMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();

}
