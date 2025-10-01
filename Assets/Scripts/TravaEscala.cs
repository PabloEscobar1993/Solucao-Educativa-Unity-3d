using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FixScaleOnGrab : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;
    private Vector3 originalScale;

    void Awake()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        originalScale = transform.localScale;

        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        transform.localScale = originalScale;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        transform.localScale = originalScale;
    }
}
