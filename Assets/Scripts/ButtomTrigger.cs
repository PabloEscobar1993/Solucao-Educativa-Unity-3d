using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    // Arraste o GameObject do ResetManager aqui no Inspector
    public ResetManager resetManager;

    // Esta função é chamada quando outro Collider com "Is Trigger" toca o botão
    private void OnTriggerEnter(Collider other)
    {
        // Verifique se o objeto que tocou o botão é a mão do jogador.
        // Você pode usar o nome ou uma tag para identificar a mão.
        if (other.CompareTag("Hand")) // Certifique-se de que a mão tem a tag "Hand"
        {
            // Chame a função de reset do ResetManager
            if (resetManager != null)
            {
                resetManager.RetornaPontoInicial();
            }
            
            Debug.Log("Botão ativado!");
        }
    }
}