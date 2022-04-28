using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public enum OrbType
    {
        FEU,
        FERTILITE,
        FORGERON,
        FOUDRE
    }

    public OrbType orb;
    public string dialogName;

    
    public void UnlockOrb() {
        switch(orb) {
            case OrbType.FEU:
                PlayerAttack.Instance.hasThunderSkill = true;
                break;

            case OrbType.FERTILITE:
                PlayerAttack.Instance.hasFertilitySkill = true;
                break;

            case OrbType.FORGERON:
                PlayerAttack.Instance.hasHammerSkill = true;
                break;

            case OrbType.FOUDRE:
                PlayerAttack.Instance.hasRaySkill = true;
                break;
        }
    }
}
