using UnityEngine.SceneManagement;

/// <summary>
/// Like a door, but changes maps.
/// </summary>
public class Portal : Interactable
{
    public string sceneToLoad;

    void Start()
    {
        freezePlayer = true;
    }

    public override void Activate()
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
}