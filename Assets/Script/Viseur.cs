using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viseur : MonoBehaviour
{
    [SerializeField] private GameObject viseur;

    public Vector3 orbePosition;

    public static Viseur Instance { get; set; }

    private void Awake()
    {
        Instance = this;
        viseur = GameObject.FindGameObjectWithTag("Viseur");
    }

    private void Update()
    {
        if (orbePosition != Vector3.zero)
        {
            viseur.transform.position = transform.position + (orbePosition - gameObject.transform.position).normalized * 1.5f;
        }
    }

    public void OrbPos(Vector3 pos)
    {
        viseur.SetActive(true);
        orbePosition = pos;
    }

    public void ResetViseur()
    {
        viseur.SetActive(false);
        orbePosition = Vector3.zero;
    }
}
