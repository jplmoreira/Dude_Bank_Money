using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float damage = 1;
    public LayerMask hittable;
    public Transform bulletTrail;

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
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 
                                            Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 barrelPosition = new Vector2(barrel.position.x, barrel.position.y);
        RaycastHit2D hit = Physics2D.Raycast(barrelPosition, mousePosition - barrelPosition, 100, hittable);
        Effect();
        Debug.DrawLine(barrelPosition, (mousePosition-barrelPosition)*100);
        if (hit.collider != null) {
            Debug.DrawLine(barrelPosition, hit.point, Color.red);
        }
    }

    void Effect() {
        float rotation = transform.parent.rotation.eulerAngles.z;
        if (transform.parent.localScale.x < 0) {
            rotation += 180;
        }
        Instantiate(bulletTrail, barrel.position, Quaternion.Euler(0f, 0f, rotation));
    }
}
