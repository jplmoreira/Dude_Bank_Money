using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour {

    public int rotationOffset = 0;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.

    // Update is called once per frame
    void Update () {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; // Get position difference between the mouse cursor and the player
        difference.Normalize();                                                                        // Normalize the vector

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;                          // Find the angle
        if (m_FacingRight && ((rotZ > 90) || (rotZ < -90))) {
            Flip();
        } else if (!m_FacingRight && (rotZ < 90) && (rotZ > -90)) {
            Flip();
        }

        if (m_FacingRight)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
        } else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset - 180);
        }
	}

    private void Flip() {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
