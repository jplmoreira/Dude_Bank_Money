using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public int damage = 100;
    public int numBullets = 6;
    public Transform bulletTrail;
    public Transform muzzleFlash;

    public int Bullets {
        get { return numBullets; }
    }

    Transform barrel;

    private float nextShot = 1;
    public float shotCooldown = 2;

    // Use this for initialization
    void Awake() {
        barrel = transform.Find("GunBarrel");
        if (barrel == null) {
            Debug.LogError("GunBarrel not found.");
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (Input.GetButtonDown("Fire1")  && Time.time > nextShot) {
            nextShot = Time.time + shotCooldown;
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
            Transform clone = (Transform)Instantiate(bulletTrail, barrel.position, Quaternion.Euler(0f, 0f, rotation));
            Vector3 mousePosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                                Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            Vector3 dir = mousePosition - barrel.position;
            clone.GetComponent<MoveTrail>().Shoot(dir.normalized);

            clone = (Transform)Instantiate(muzzleFlash, barrel.position, barrel.rotation);
            clone.parent = barrel;
            float size = Random.Range(0.6f, 0.9f);
            clone.localScale = new Vector3(size, size, 0);
            Destroy(clone.gameObject, 0.02f);
        }
    }

    public void ResetShot()
    {
        nextShot = 0;
    }
}