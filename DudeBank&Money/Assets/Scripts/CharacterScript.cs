using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour {

    [System.Serializable]
    public class CharacterStats {
        public int health = 1;
    }

    public CharacterStats charStats = new CharacterStats();

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (gameObject.tag == "Enemy" && coll.gameObject.name == "MaceOnChain01")
            DamageCharacter(1);
    }

    public void DamageCharacter(int damage) {
        charStats.health -= damage;
        if (charStats.health <= 0)
            GameMaster.KillCharacter(this);
    }
}
