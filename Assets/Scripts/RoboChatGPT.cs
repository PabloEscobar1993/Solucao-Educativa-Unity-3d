using UnityEngine;
using TMPro; // Use para UI de texto!

public class RobotController : MonoBehaviour
{
    // Referência ao script de integração
    public GeminiIntegration geminiIntegration;

    // Referência ao seu componente de texto na interface (UI)
    public TMP_Text robotSpeechBubble;

    // Esta função é chamada quando um objeto com Collider (Is Trigger) entra em contato
    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que entrou em contato é um polígono e se a plataforma é a incorreta
        if (other.CompareTag("Polygon") && gameObject.CompareTag("IncorrectPlatform"))
        {
            // Pega o nome do polígono para usar no prompt
            string polygonName = other.name;

            // Cria o prompt dinâmico para o ChatGPT
            string userPrompt = $"Eu coloquei o polígono '{polygonName}' em uma plataforma errada. Diga algo divertido sobre o meu erro.";

            // Chama o método assíncrono do ChatGPT
            StartCoroutine(CallChatGPTCoroutine(userPrompt));
        }
    }

    private System.Collections.IEnumerator CallChatGPTCoroutine(string prompt)
    {
        // Chama a função assíncrona
        var task = geminiIntegration.SendMessageToGemini(prompt);
        yield return new WaitUntil(() => task.IsCompleted);

        // Pega o resultado
        string response = task.Result;

        // Exibe a resposta do robô
        robotSpeechBubble.text = response;
        Debug.Log("Resposta do Gemini: " + response);
    }
}