using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
     
    




    public void ReloadScene()
    {
        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        yield return new WaitForSeconds(1);
        yield return SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        
    }
}
