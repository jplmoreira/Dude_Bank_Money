using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public int damage = 1;
    public int numBullets = 6;
    public Transform bulletTrail;
    public Transform muzzleFlash;

    public int Bullets {
        get { return numBullets; }
    }

    Transform barrel;

    // Use this for initialization
    void Awake() {
        barrel = transform.Find("GunBarrel");
        if (barrel == null) {
            Debug.LogError("GunBarrel not found.");
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    void Shoot() {
        if (numBullets > 0) {
            numBullets--;
            float rotation = transform.parent.rotation.eulerAngles.z;
            if (transform.parent.localScale.x < 0) {
                rotation += 180;
            }
            Instantiate(bulletTrail, barrel.position, Quaternion.Euler(0f, 0f, rotation));

            Transform clone = (Transform)Instantiate(muzzleFlash, barrel.position, barrel.rotation);
            clone.parent = barrel;
            float size = Random.Range(0.6f, 0.9f);
            clone.localScale = new Vector3(size, size, 0);
            Destroy(clone.gameObject, 0.02f);
        }
    }
}