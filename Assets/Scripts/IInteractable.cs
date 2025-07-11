public interface IInteractable
{
    public void Interact(PlayerManager player);
    public void OnEnterInteractRange();
    public void OnExitInteractRange();

}