using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class TangramManager : MonoBehaviour
{
    // Crie um GameObject para as peças manipuláveis
    public GameObject manipulablePiecesParent;
    
    // E um para as peças do molde
    public GameObject moldPiecesParent;

    // Dicionários para armazenar as posições e rotações iniciais
    private Dictionary<GameObject, Vector3> initialPositions = new Dictionary<GameObject, Vector3>();
    private Dictionary<GameObject, Quaternion> initialRotations = new Dictionary<GameObject, Quaternion>();

    void Start()
    {
        // Salva as posições e rotações iniciais
        StoreInitialTransforms(manipulablePiecesParent);
        StoreInitialTransforms(moldPiecesParent);
    }

    private void StoreInitialTransforms(GameObject parent)
    {
        if (parent == null) return;
        
        foreach (Transform child in parent.transform)
        {
            initialPositions[child.gameObject] = child.position;
            initialRotations[child.gameObject] = child.rotation;
        }
    }

    // Esta função será chamada pelo Player Input
    public void ResetTangramPieces()
    {
        // Reseta as peças manipuláveis
        ResetToInitialTransforms(manipulablePiecesParent);
        
        // E as peças do molde
        ResetToInitialTransforms(moldPiecesParent);
    }
    
    private void ResetToInitialTransforms(GameObject parent)
    {
        if (parent == null) return;
        
        foreach (Transform child in parent.transform)
        {
            GameObject piece = child.gameObject;
            
            // Reseta a posição e a rotação
            piece.transform.position = initialPositions[piece];
            piece.transform.rotation = initialRotations[piece];

            // Ativa a peça caso ela tenha sido desativada
            piece.SetActive(true);

            // Ativa o Rigidbody e o XR Grab Interactable, se existirem
            Rigidbody rb = piece.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }

            UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable interactable = piece.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
            if (interactable != null)
            {
                interactable.enabled = true;
            }
            
            // Ativa o MeshRenderer da peça de molde
            MeshRenderer mr = piece.GetComponent<MeshRenderer>();
            if (mr != null)
            {
                mr.enabled = true;
            }
        }
    }
}