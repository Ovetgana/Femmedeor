using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    public class MouseCamera
    {
        public float XSensitivity = 2f;
        public float YSensitivity = 2f;

        private Quaternion m_CharacterTargetRot;

        public void Init(Transform character)
        {
            m_CharacterTargetRot = character.localRotation;
        }

        public void LookRotation(Transform character)
        {
            float yRot = Input.GetAxis("Mouse X") * XSensitivity;
            float xRot = Input.GetAxis("Mouse Y") * YSensitivity;
           
            Vector3 rotation_y_global = new Vector3(0f, yRot, 0f);

            // transformation rotation around Y-axis from global to local coordinates in order to keep bottom edge of camera parallel to the horizon
            Vector3 rotation_y_local = character.transform.InverseTransformVector(rotation_y_global);
            
            m_CharacterTargetRot *= Quaternion.Euler(rotation_y_local) * Quaternion.Euler(-xRot, 0, 0f);
            
            character.localRotation = m_CharacterTargetRot;
        }
    }

    

}
