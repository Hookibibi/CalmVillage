using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForceLeaveGame : MonoBehaviour
{
    public void GoBackToMenu(GameObject gobj)
    {
        Destroy(gobj);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("mainMenu");
    }
}
