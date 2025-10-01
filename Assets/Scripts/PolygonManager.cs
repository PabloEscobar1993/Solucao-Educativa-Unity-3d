using System.Collections.Generic;
using UnityEngine;

public class PolygonManager : MonoBehaviour
{
    // A lista de todos os polígonos que precisam ser reiniciados.
    public List<Transform> polygonsToReset;

    // Um dicionário para armazenar a posição e rotação iniciais de cada polígono.
    private Dictionary<Transform, Vector3> initialPositions = new Dictionary<Transform, Vector3>();
    private Dictionary<Transform, Quaternion> initialRotations = new Dictionary<Transform, Quaternion>();

    void Start()
    {
        // Salva a posição e a rotação iniciais de cada polígono.
        foreach (Transform polygon in polygonsToReset)
        {
            initialPositions[polygon] = polygon.position;
            initialRotations[polygon] = polygon.rotation;
        }
    }

    /// <summary>
    /// Esta função move todos os polígonos de volta para suas posições e rotações iniciais.
    /// </summary>
    public void ResetAllPolygons()
    {
        foreach (Transform polygon in polygonsToReset)
        {
            // Pega a posição e rotação inicial do dicionário.
            Vector3 initialPos = initialPositions[polygon];
            Quaternion initialRot = initialRotations[polygon];

            // Define a posição e a rotação do polígono para os valores iniciais.
            polygon.position = initialPos;
            polygon.rotation = initialRot;

            // Se você estiver usando um Rigidbody para simulação de física, é bom também
            // zerar a velocidade e a velocidade angular para evitar comportamentos indesejados.
            Rigidbody rb = polygon.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
        Debug.Log("Todos os polígonos foram resetados para a posição inicial!");
    }

    public void OnResetAction()
    {
        // Esta função é chamada quando a ação é ativada.
        ResetAllPolygons();
    }
    
}