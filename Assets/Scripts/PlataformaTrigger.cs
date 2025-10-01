using UnityEngine;
using System.Collections;
using TMPro;

public class PlataformaTrigger : MonoBehaviour
{
    public PlataformaCorreta plataformaCorreta;
    public string tagEsperada = "Triangulo";
    public GeminiIntegration robo; 
    public TMP_Text textoDoRobo;
    
    private void OnTriggerEnter(Collider other)
    {
        string promptParaChatGPT;

        if (other.CompareTag(tagEsperada))
        {
            promptParaChatGPT = $"O usuário acabou de colocar a forma '{other.gameObject.name}' na plataforma correta. Diga algo positivo e encorajador, mas seja breve.";

            if (plataformaCorreta != null)
            {
                plataformaCorreta.AoColocarCorreto();
            }
        }
        else
        {
            promptParaChatGPT = $"O usuário colocou a forma '{other.gameObject.name}' na plataforma errada. A forma correta seria uma com a tag '{tagEsperada}'. Diga para ele tentar de novo, mas com poucas palavras.";
            
            if (plataformaCorreta != null)
            {
                plataformaCorreta.AoColocarErrado();
            }
        }
        
        StartCoroutine(ComunicarComRobo(promptParaChatGPT));
    }
    
    IEnumerator ComunicarComRobo(string prompt)
    {
        var task = robo.SendMessageToGemini(prompt);
        yield return new WaitUntil(() => task.IsCompleted);

        string resposta = task.Result;

        if (textoDoRobo != null)
        {
            textoDoRobo.text = resposta;
        }
        
        Debug.Log("Resposta do Gemini: " + resposta);
    }
}