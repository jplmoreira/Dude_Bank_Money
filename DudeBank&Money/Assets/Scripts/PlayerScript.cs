using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    [System.Serializable]
    public class PlayerStats {
        public int health = 1;
    }

    public PlayerStats playerStats = new PlayerStats();
    public int fallBoundary = -10;

    private void Update() {
        if (transform.position.y <= fallBoundary || transform.position.x >= 39)
            DamagePlayer();
    }

    public void DamagePlayer() {
        GameMaster.KillPlayer(this);
    }
}
