using UnityEngine;

public class DialogInteraction : MonoBehaviour
{
    [SerializeField] private DialogUI dialogUI;

    public DialogUI DialogUI => dialogUI;
    public IInteractable Interactable { get; set; }

    private void Update()
    {
        if (dialogUI.IsOpen) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            // Question mark controls if Interactable is not null
            Interactable?.Interact(this);
        }
    }
}
