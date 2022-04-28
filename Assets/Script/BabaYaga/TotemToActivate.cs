using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemToActivate : MonoBehaviour
{
    public ActivateBoss script;
    [SerializeField] private Sprite totemOn;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            script.ActivateATotem();
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            gameObject.GetComponent<SpriteRenderer>().sprite = totemOn;
        }
    }
}
