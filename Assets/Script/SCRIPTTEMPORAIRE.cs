using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRIPTTEMPORAIRE : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            GameManager.UpdateTutorial();
            Destroy(gameObject);
        }
    }
}
