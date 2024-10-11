using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
   public void SceneSwitcher()
   {
    SceneManager.LoadScene(1);
   }

    public void SceneRestart()
   {
     InputSystem.ResetHaptics(); 
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    
   }

    public void SceneQuit()
   {
    Application.Quit();
   }
}
