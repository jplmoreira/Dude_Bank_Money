using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScr : MonoBehaviour
{

    private Animator anim;
    
	void Start ()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        anim.enabled = true;
    }
}
