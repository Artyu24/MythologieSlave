using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemChest : MonoBehaviour
{
    [SerializeField] private Sprite soinSprite, shieldSprite, damageSprite, speedSprite, orbeSprite;
    [SerializeField] private string soinText, shieldText, damageText, speedText, orbeText;

    [SerializeField] private int addLife, addShield, addDamage, addSpeed;

    private Image objectImg;
    private Text objectText;

    private void Awake()
    {
        objectImg = GameObject.FindGameObjectWithTag("ChestUI").GetComponent<Image>();
        objectText = objectImg.gameObject.transform.GetChild(0).GetComponent<Text>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("SilverChest"))
            {
                //Jouer l'anim d'ouverture du coffre
                objectImg.gameObject.GetComponent<PlayerUI>().StartAffichageUI();
                int i = Random.Range(0, 4);
                switch (i)
                {
                    case 0:
                        objectImg.sprite = soinSprite;
                        objectText.text = soinText;
                        col.gameObject.GetComponent<PlayerHealth>().AddLife(addLife);
                        break;
                    case 1:
                        objectImg.sprite = shieldSprite;
                        objectText.text = shieldText;
                        col.gameObject.GetComponent<PlayerHealth>().AddShield();
                        //Add Shield
                        break;
                    case 2:
                        objectImg.sprite = damageSprite;
                        objectText.text = damageText;
                        col.gameObject.GetComponent<PlayerAttack>().UpgradeDamage(5);
                        break;
                    case 3:
                        objectImg.sprite = speedSprite;
                        objectText.text = speedText;
                        col.gameObject.GetComponent<PlayerController>().speed += 2;
                        break;
                }
            }
            else
            {
                //AFFICHAGE DE LA BOULE CHOISIS ET DE SON AMELIORATION
            }
            Destroy(gameObject);
        }
    }
}
