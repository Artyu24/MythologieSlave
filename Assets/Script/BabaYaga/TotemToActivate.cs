using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemToActivate : MonoBehaviour
{
    public ActivateBoss script;
    [SerializeField] private Sprite totemOff, totemOn;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            script.ActivateATotem();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = totemOn;
        }
    }

    public void ResetTotem()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = totemOff;
    }
}
