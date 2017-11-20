using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public int damage = 1;
    public int numBullets = 6;
    public LayerMask hittable;
    public Transform bulletTrail;
    public Transform muzzleFlash;
    public Transform hitParticle;

    public int Bullets {
        get { return numBullets;  }
    }

    Transform barrel;

	// Use this for initialization
	void Awake () {
        barrel = transform.Find("GunBarrel");
        if (barrel == null) {
            Debug.LogError("GunBarrel not found.");
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
	}

    void Shoot() {
        if (numBullets > 0) {
            numBullets--;
            Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                            Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            Vector2 barrelPosition = new Vector2(barrel.position.x, barrel.position.y);
            RaycastHit2D hit = Physics2D.Raycast(barrelPosition, mousePosition - barrelPosition, 100, hittable);
            Debug.DrawLine(barrelPosition, (mousePosition - barrelPosition) * 100);
            if (hit.collider != null) {
                Debug.DrawLine(barrelPosition, hit.point, Color.red);
                EnemyScript enemy = hit.collider.GetComponent<EnemyScript>();
                if (enemy != null)
                    enemy.DamageEnemy(damage);
            }
            Vector3 hitPos;
            Vector3 hitNormal;
            if (hit.collider == null) {
                hitPos = (mousePosition - barrelPosition) * 30;
                hitNormal = new Vector3(9999, 9999, 9999);
            } else {
                hitPos = hit.point;
                hitNormal = hit.normal;
            }
            Effect(hitPos, hitNormal);
        }
    }

    void Effect(Vector3 hitPos, Vector3 hitNormal) {
        float rotation = transform.parent.rotation.eulerAngles.z;
        if (transform.parent.localScale.x < 0) {
            rotation += 180;
        }
        Transform trail = (Transform) Instantiate(bulletTrail, barrel.position, Quaternion.Euler(0f, 0f, rotation));
        LineRenderer lr = trail.GetComponent<LineRenderer>();
        if (lr != null) {
            lr.SetPosition(0, barrel.position);
            lr.SetPosition(1, hitPos);
        }
        Destroy(trail.gameObject, 0.05f);

        if (hitNormal != new Vector3(9999,9999,9999)) {
            Transform particle = Instantiate(hitParticle, hitPos, Quaternion.FromToRotation(Vector3.right, hitNormal));
            Destroy(particle.gameObject, 1f);
        }

        Transform clone = (Transform) Instantiate(muzzleFlash, barrel.position, barrel.rotation);
        clone.parent = barrel;
        float size = Random.Range(0.6f, 0.9f);
        clone.localScale = new Vector3(size, size, 0);
        Destroy(clone.gameObject, 0.02f);
    }
}
