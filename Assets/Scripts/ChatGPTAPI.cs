using UnityEngine;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class GeminiIntegration : MonoBehaviour
{
    // A chave da sua API do Google Gemini
    [SerializeField] private string gemini_API_Key;
    private readonly string gemini_API_Url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=AIzaSyBX-L_N3HYKLIQc61HmbV8l4bQOhmzXmy8";

    private static readonly HttpClient client = new HttpClient();

    // Classes para o JSON de requisição do Gemini
    [System.Serializable]
    public class Part
    {
        public string text;
    }

    [System.Serializable]
    public class Content
    {
        public string role;
        public Part[] parts;
    }

    [System.Serializable]
    public class GeminiRequest
    {
        public Content[] contents;
    }
    
    // Classes para o JSON de resposta do Gemini
    [System.Serializable]
    public class Candidate
    {
        public Content content;
    }

    [System.Serializable]
    public class GeminiResponse
    {
        public Candidate[] candidates;
    }


    public async Task<string> SendMessageToGemini(string userPrompt)
    {
        // Headers não são necessários para o Gemini, a chave está na URL
        client.DefaultRequestHeaders.Clear();
        
        var contents = new Content[]
        {
            new Content { role = "user", parts = new Part[] { new Part { text = userPrompt } } }
        };

        var geminiRequest = new GeminiRequest
        {
            contents = contents
        };
        
        var jsonPayload = JsonConvert.SerializeObject(geminiRequest);
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        try
        {
            HttpResponseMessage response = await client.PostAsync(gemini_API_Url + gemini_API_Key, content);
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();
            var geminiResponse = JsonConvert.DeserializeObject<GeminiResponse>(jsonResponse);

            return geminiResponse.candidates[0].content.parts[0].text;
        }
        catch (HttpRequestException e)
        {
            Debug.LogError($"Erro na requisição HTTP: {e.Message}");
            return "Ops! Parece que minha conexão com a rede falhou. Tente de novo!";
        }
    }
}