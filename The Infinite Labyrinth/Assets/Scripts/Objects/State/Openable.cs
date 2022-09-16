public class Openable : Interactable
{
    public bool _isOpened;
    public override void Interact()
    {
        if (!_isOpened)
        {
            GetComponent<SpawnItem>().enabled = true;

            _isOpened = true;
            this.enabled = false;
        }       
    }
}
