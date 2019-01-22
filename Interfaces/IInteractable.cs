/// <summary>
/// Any object that the player can interact with 
/// by pressing the use key.
/// </summary>
public interface IInteractable
{
    /// <summary>
    /// The method to run when the player uses the interactable.
    /// </summary>
    void Activate();

    /// <summary>
    /// The action that should listen for when the activate
    /// event is triggered.
    /// </summary>
    System.Action activateListener { get; set; }
}