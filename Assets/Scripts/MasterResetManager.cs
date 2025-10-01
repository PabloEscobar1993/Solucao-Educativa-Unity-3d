using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class MasterResetManager : MonoBehaviour
{
    // A lista de objetos que serão resetados
    public List<GameObject> objectsToReset;

    // Classe interna para armazenar os dados iniciais de cada objeto
    [System.Serializable]
    public class InitialState
    {
        public Vector3 position;
        public Quaternion rotation;
        public bool isKinematic;
        public bool isInteractableEnabled;
        public bool isRendererEnabled;
    }

    private Dictionary<GameObject, InitialState> initialStates = new Dictionary<GameObject, InitialState>();

    void Start()
    {
        StoreInitialTransforms();
    }

    private void StoreInitialTransforms()
    {
        foreach (GameObject obj in objectsToReset)
        {
            if (obj != null)
            {
                InitialState state = new InitialState();
                state.position = obj.transform.position;
                state.rotation = obj.transform.rotation;

                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    state.isKinematic = rb.isKinematic;
                }

                UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable interactable = obj.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
                if (interactable != null)
                {
                    state.isInteractableEnabled = interactable.enabled;
                }

                MeshRenderer mr = obj.GetComponent<MeshRenderer>();
                if (mr != null)
                {
                    state.isRendererEnabled = mr.enabled;
                }
                
                initialStates[obj] = state;
            }
        }
    }

    public void ResetObjects()
    {
        foreach (GameObject obj in objectsToReset)
        {
            if (obj != null && initialStates.ContainsKey(obj))
            {
                InitialState state = initialStates[obj];

                obj.transform.position = state.position;
                obj.transform.rotation = state.rotation;
                
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = state.isKinematic;
                }
                
                UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable interactable = obj.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
                if (interactable != null)
                {
                    interactable.enabled = state.isInteractableEnabled;
                }
                
                MeshRenderer mr = obj.GetComponent<MeshRenderer>();
                if (mr != null)
                {
                    mr.enabled = state.isRendererEnabled;
                }
            }
        }
    }
}