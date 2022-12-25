using UnityEngine;
using UnityEngine.InputSystem;

public class DialogInteraction : MonoBehaviour
{
    [SerializeField] private DialogUI dialogUI;

    public DialogUI DialogUI => dialogUI;
    public IInteractable Interactable { get; set; }

    private bool isInteracting;

    private void Update()
    {
        if (dialogUI.IsOpen) return;
        
        if (isInteracting)
        {
            // Question mark controls if Interactable is not null
            Interactable?.Interact(this);
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            isInteracting = true;
        }
        else if (context.canceled)
        {
            isInteracting = false;
        }
    }
}
