using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class UIPanelManager : MonoBehaviour
{
    // O GameObject que você quer mostrar e esconder (o Canvas)
    public GameObject uiPanel;

    // A Ação de Entrada que será usada para alternar a UI
    public InputActionProperty toggleUIAction;

    void OnEnable()
    {
        // Garante que a ação está habilitada e assina o evento
        if (toggleUIAction.action != null)
        {
            toggleUIAction.action.Enable();
            toggleUIAction.action.performed += OnToggleUI;
        }
    }

    void OnDisable()
    {
        // Desassina o evento quando o objeto é desativado
        if (toggleUIAction.action != null)
        {
            toggleUIAction.action.performed -= OnToggleUI;
            toggleUIAction.action.Disable();
        }
    }

    private void OnToggleUI(InputAction.CallbackContext context)
    {
        // Alterna a visibilidade do painel de UI
        uiPanel.SetActive(!uiPanel.activeSelf);
    }
}