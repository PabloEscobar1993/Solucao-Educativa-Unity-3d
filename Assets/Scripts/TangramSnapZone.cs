using UnityEngine;

using System.Linq;

public class TangramSnapZone : MonoBehaviour
{
    // A tag da peça que deve ser encaixada nesta zona
    public string correctPieceTag;
    
    // A distância que a peça de molde vai recuar para não colidir
    public float retreatDistance = 0.05f;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que entrou tem a tag correta
        if (other.CompareTag(correctPieceTag))
        {
            // Pega o componente XR Grab Interactable da peça
            UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable interactablePiece = other.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

            if (interactablePiece != null)
            {
                // Dispara a lógica de encaixe
                SnapPiece(interactablePiece);
            }
        }
    }

    private void SnapPiece(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable piece)
    {
        // 1. Desativa a capacidade de agarrar na peça do usuário
        piece.enabled = false;

        // 2. Desativa a física na peça do usuário
        Rigidbody rb = piece.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // 3. Se a peça estiver sendo agarrada, solte-a
        if (piece.isSelected)
        {
            UnityEngine.XR.Interaction.Toolkit.Interactors.IXRSelectInteractor interactor = piece.interactorsSelecting.FirstOrDefault();
            if (interactor != null)
            {
                piece.interactionManager.SelectExit(interactor, (UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable)piece);
            }
        }
        
        // 4. Move a peça do usuário para a posição exata do molde
        piece.transform.position = transform.position;
        piece.transform.rotation = transform.rotation;
        
        // 5. Move a peça do molde para trás, para que a peça do usuário fique na frente
        transform.position += transform.forward * -retreatDistance;

        Debug.Log("Peça encaixada com sucesso!");
    }
}