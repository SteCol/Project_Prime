using UnityEngine;
using System.Collections;

public class LockCursor : MonoBehaviour {

    public bool lockCursor, lockCursorPulse;

    void Start() {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.L)) {
            lockCursor = !lockCursor;
        }

        if (lockCursor != lockCursorPulse) {
            if (lockCursor == true)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.lockState = CursorLockMode.Confined;

            }

            lockCursorPulse = lockCursor;
        }
	}
}
