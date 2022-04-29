using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Image img;
    [SerializeField] private Text text;
    [SerializeField] private Vector3 offset;

    public static PlayerUI Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        img.transform.position = Camera.main.WorldToScreenPoint(player.position + offset);
    }

    public void StartAffichageUI()
    {
        StartCoroutine(AffichageUI());
    }

    private IEnumerator AffichageUI()
    {
        img.color = new Color(255, 255, 255, 255);
        text.color = new Color(255, 255, 255, 255);
        yield return new WaitForSeconds(1f);
        img.color = new Color(255, 255, 255, 0);
        text.color = new Color(255, 255, 255, 0);
    }

    public void StartAffichageUITutorial(Sprite sprite)
    {
        StartCoroutine(AffichageUITutorial(sprite));
    }

    private IEnumerator AffichageUITutorial(Sprite sprite)
    {
        yield return new WaitForSeconds(3f);
        img.color = new Color(255, 255, 255, 255);
        img.sprite = sprite;
        yield return new WaitForSeconds(3f);
        img.color = new Color(255, 255, 255, 0);
    }

    public void ResetAffichage()
    {
        img.color = new Color(255, 255, 255, 0);
        text.color = new Color(255, 255, 255, 0);
    }
}
