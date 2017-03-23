using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class SROptions
{
    [Category("Scene")]
    public void ReloadScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    public void SetBasicLevel()
    {
        PlayerPrefs.SetString("currentWorldName", "basic");
        ReloadScene();
    }

    public void SetRockLevel()
    {
        PlayerPrefs.SetString("currentWorldName", "rock");
        ReloadScene();
    }
}