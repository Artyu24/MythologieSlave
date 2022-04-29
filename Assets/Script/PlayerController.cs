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

    public Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        DontDestroyOnLoad(this.gameObject);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movementInput * Time.fixedDeltaTime * speed);
        if (movementInput != Vector2.zero && GameManager.GetGameState == GameManager.GameState.Tuto && GameManager.GetTutorialState == GameManager.TutorialState.Mouvement)
            GameManager.UpdateTutorial();

        float speedAnim = 0f;

        if ((movementInput.x > 0.1f || movementInput.y > 0.1f) || (movementInput.x < -0.1f || movementInput.y < -0.1f))
            speedAnim = 1f;
        else
            speedAnim = 0f;

        animator.SetFloat("Speed",speedAnim);
        animator.SetFloat("Horizontal", movementInput.x);
        animator.SetFloat("Vertical", movementInput.y);
    }

    public void OnMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();


    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.tag == "Totem") {
            collideWithTotem = true;

            Dialog dialog = DialogController.Instance.GetDialogByName(collision.collider.gameObject.transform.GetChild(0).gameObject.GetComponent<Orb>().dialogName);
            DialogController.Instance.currentDialog = dialog;
            DialogController.Instance.isInDialog = true;
            StartCoroutine(DialogController.Instance.ShowText(dialog.Content[0], dialog.Content[0].Length));
        } 
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if(collision.collider.tag == "Totem") {
            collideWithTotem = false;
        }
    }
}
