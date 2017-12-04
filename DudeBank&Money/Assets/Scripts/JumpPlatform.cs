using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class JumpPlatform : MonoBehaviour {
	void OnCollisionStay2D(Collision2D collider){
        if (collider.gameObject.tag == "Player" && (Input.GetKeyDown(KeyCode.S) || Input.GetKey(KeyCode.S))){ //&& Input.GetKeyDown("space") ->doesnt work, character jumps up :s
            jumpThrough();
            Invoke("jumpThrough", 0.5f);
        }
	}
    public void jumpThrough(){
        gameObject.GetComponent<Collider2D>().enabled = !gameObject.GetComponent<Collider2D>().enabled;
    }
}