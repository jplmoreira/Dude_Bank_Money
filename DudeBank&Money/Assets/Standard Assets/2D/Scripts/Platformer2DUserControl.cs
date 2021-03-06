using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;

        


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            //float h = CrossPlatformInputManager.GetAxis("Horizontal");

            float h = 0.0f;
            bool left = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
            bool right = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);

            if (left && !right)
            {
                h -= 1.0f;
            }

            if (right && !left)
            {
                h += 1.0f;
            }

            // Pass all parameters to the character control script.
            if (m_Character.timeStop && m_Character.timeStopActions > 0)
            {
                m_Character.Move(h, crouch, m_Jump);
                m_Jump = false;
            }
            else if (m_Character.timeStop)
            {
                m_Jump = false;
                m_Character.Move(h, crouch, m_Jump);
            }
            else
            {
                m_Character.Move(h, crouch, m_Jump);
                m_Jump = false;
            }
        }
    }
}
