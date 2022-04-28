using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    //public enum OrbType
    //{
    //    FEU,
    //    FERTILITE,
    //    FORGERON,
    //    FOUDRE
    //}

    //public OrbType orb;
    //public string dialogName;

    
    //public void UnlockOrb() {
    //    switch(orb) {
    //        case OrbType.FEU:
    //            PlayerAttack.Instance.hasThunderSkill = true;
    //            break;

    //        case OrbType.FERTILITE:
    //            PlayerAttack.Instance.hasFertilitySkill = true;
    //            break;

    //        case OrbType.FORGERON:
    //            PlayerAttack.Instance.hasHammerSkill = true;
    //            break;

    //        case OrbType.FOUDRE:
    //            PlayerAttack.Instance.hasRaySkill = true;
    //            break;
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (GameManager.gameState == GameManager.GameState.Tuto && GameManager.tutorialState == GameManager.TutorialState.Interaction)
            {
                GameManager.UpdateTutorial();
            }
        }
    }
}
