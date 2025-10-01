using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneResetManager : MonoBehaviour
{
    // Lista de GameObjects cujas posições e rotações devem ser resetadas
    public List<GameObject> resettableObjects;

    // Dicionário para armazenar as posições e rotações iniciais dos objetos
    private Dictionary<GameObject, Vector3> initialPositions = new Dictionary<GameObject, Vector3>();
    private Dictionary<GameObject, Quaternion> initialRotations = new Dictionary<GameObject, Quaternion>();

    void Awake()
    {
        // Armazena as posições e rotações iniciais de todos os objetos resetáveis
        foreach (GameObject obj in resettableObjects)
        {
            if (obj != null)
            {
                initialPositions[obj] = obj.transform.position;
                initialRotations[obj] = obj.transform.rotation;
            }
        }
    }

    public void ReloadCurrentScene()
    {
        Debug.Log("Recarregando a cena atual...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResetPolygonPositions()
    {
        Debug.Log("Resetando posições dos polígonos...");
        foreach (GameObject obj in resettableObjects)
        {
            if (obj != null && initialPositions.ContainsKey(obj) && initialRotations.ContainsKey(obj))
            {
                obj.transform.position = initialPositions[obj];
                obj.transform.rotation = initialRotations[obj];
                // Se os objetos tiverem Rigidbody, também resetar a velocidade
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }
        }
    }
}

