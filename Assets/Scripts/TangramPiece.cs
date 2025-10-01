using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class TangramPiece : MonoBehaviour
{
    // A tag da peça de molde que corresponde a esta peça
    public string correctMoldTag;

    // A lista de todas as zonas de encaixe (os moldes)
    private List<GameObject> snapZones = new List<GameObject>();

    // A distância máxima para que o encaixe ocorra
    public float snapDistance = 0.1f;

    void Start()
    {
        // Encontra todos os moldes na cena com a tag correspondente
        foreach (GameObject mold in GameObject.FindGameObjectsWithTag(correctMoldTag))
        {
            snapZones.Add(mold);
        }

        // Pega o componente XRGrabInteractable desta peça
        UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (interactable != null)
        {
            // Vincula a função de encaixe ao evento de soltar a peça
            interactable.selectExited.AddListener(OnSelectExited);
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        // Encontra a zona de encaixe mais próxima
        GameObject closestSnapZone = FindClosestSnapZone();

        if (closestSnapZone != null)
        {
            // Calcula a distância até a zona de encaixe
            float distance = Vector3.Distance(transform.position, closestSnapZone.transform.position);

            // Se a distância for menor que a distância de encaixe, faça o snap
            if (distance < snapDistance)
            {
                SnapToMold(closestSnapZone);
            }
        }
    }

    private GameObject FindClosestSnapZone()
    {
        GameObject closest = null;
        float minDistance = float.MaxValue;

        foreach (GameObject zone in snapZones)
        {
            if (zone != null)
            {
                float distance = Vector3.Distance(transform.position, zone.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closest = zone;
                }
            }
        }
        return closest;
    }

    private void SnapToMold(GameObject mold)
    {
        // Desativa a capacidade de agarrar
        UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (interactable != null)
        {
            interactable.enabled = false;
        }

        // Desativa a física
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // Teleporta a peça para a posição e rotação do molde
        transform.position = mold.transform.position;
        transform.rotation = mold.transform.rotation;
        
        // Esconde o molde para que a peça do usuário fique visível
        mold.GetComponent<MeshRenderer>().enabled = false;

        Debug.Log("Peça encaixada com sucesso!");
    }
}