using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DiceInteraction : MonoBehaviour
{
    // A altura do pulo
    public float jumpHeight = 0.5f;

    // A duração do pulo em segundos
    public float jumpDuration = 0.5f;

    // Um AudioSource para tocar o som
    private AudioSource audioSource;
    
    // A posição inicial do dado
    private Vector3 originalPosition;

    void Start()
    {
        // Encontra o AudioSource no objeto ou o adiciona se não existir
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Salva a posição inicial para o pulo
        originalPosition = transform.position;
    }

    // Esta função será chamada quando o dado for selecionado
    public void OnSelectEnter()
    {
        // Pula o dado usando uma animação
        Jump();

        // Toca o som, se houver um
        if (audioSource.clip != null)
        {
            audioSource.Play();
        }
    }

    private void Jump()
    {
        // Inicia a corrotina para a animação de pulo suave
        StartCoroutine(JumpRoutine());
    }

    private System.Collections.IEnumerator JumpRoutine()
    {
        float timer = 0f;
        while (timer < jumpDuration)
        {
            float progress = timer / jumpDuration;
            float yOffset = Mathf.Sin(progress * Mathf.PI) * jumpHeight;
            transform.position = originalPosition + new Vector3(0, yOffset, 0);
            timer += Time.deltaTime;
            yield return null;
        }

        // Garante que o dado volte para a posição exata
        transform.position = originalPosition;
    }
}