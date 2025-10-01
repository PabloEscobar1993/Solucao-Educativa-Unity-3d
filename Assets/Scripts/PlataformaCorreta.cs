using UnityEngine;

public class PlataformaCorreta : MonoBehaviour
{
    [Header("Som")]
    public AudioSource somCorreto;
    public AudioSource somErrado;

    [Header("Parede")]
    public Renderer paredeRenderer;
    public Color corCorreta = Color.green;
    public Color corErrada = Color.red;

    [Header("Partículas")]
    public ParticleSystem particulasParede;

    public void AoColocarCorreto()
    {
        // 🎨 Troca a cor da parede
        if (paredeRenderer != null)
            paredeRenderer.material.color = corCorreta;

        // 🎵 Toca o som
        if (somCorreto != null)
            somCorreto.Play();

        // 💥 Dispara partículas
        if (particulasParede != null)
            particulasParede.Play();
    }
    
    public void AoColocarErrado()
    {
        if (paredeRenderer != null)
            paredeRenderer.material.color = corErrada;
            
        if (somErrado != null)
            somErrado.Play();
    }
}