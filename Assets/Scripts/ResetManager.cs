using UnityEngine;
using System.Collections.Generic;

public class ResetManager : MonoBehaviour
{
    // A lista de GameObjects (polígonos) que você quer reiniciar.
    // Arraste e solte seus polígonos aqui no Inspector.
    public List<GameObject> objetosParaResetar;

    // Um dicionário para guardar a posição e rotação iniciais de cada objeto.
    private Dictionary<GameObject, Vector3> posicoesIniciais = new Dictionary<GameObject, Vector3>();
    private Dictionary<GameObject, Quaternion> rotacoesIniciais = new Dictionary<GameObject, Quaternion>();

    void Awake()
    {
        // Esta função é chamada uma vez, quando o jogo começa.
        // Ela armazena a posição e a rotação inicial de cada objeto na lista.
        foreach (GameObject obj in objetosParaResetar)
        {
            if (obj != null)
            {
                posicoesIniciais[obj] = obj.transform.position;
                rotacoesIniciais[obj] = obj.transform.rotation;
            }
        }
    }

    // Esta é a função pública que o seu botão de "Reiniciar" chamará.
    public void RetornaPontoInicial()
    {
        foreach (GameObject obj in objetosParaResetar)
        {
            if (obj != null)
            {
                // Move o objeto de volta para a posição e rotação iniciais.
                obj.transform.position = posicoesIniciais[obj];
                obj.transform.rotation = rotacoesIniciais[obj];

                // Reseta a física do Rigidbody (velocidade e rotação), se houver.
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }
        }

        Debug.Log("Todos os objetos foram reiniciados.");
    }
}