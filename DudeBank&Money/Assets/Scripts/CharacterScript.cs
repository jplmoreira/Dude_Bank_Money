using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour {

    [System.Serializable]
    public class CharacterStats {
        public int health = 1;
    }

    public CharacterStats charStats = new CharacterStats();

    public void DamageCharacter(int damage) {
        charStats.health -= damage;
        GameMaster.KillCharacter(this);
    }
}
