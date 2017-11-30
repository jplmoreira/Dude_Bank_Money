using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform bulletTrail;
    public Transform muzzleFlash;
    public Transform hitParticle;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Effect();
    }

    void Effect()
    {
        Transform particle = Instantiate(hitParticle, gameObject.transform.position, Quaternion.identity);
        Destroy(particle.gameObject, 1f);
    }
}
