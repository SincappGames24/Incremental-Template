using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSpawner : MonoBehaviour
{
    public InteractableDataContainer interactableDataContainer;

    private void Start()
    {
        var interactableDataList = interactableDataContainer.interactableDataList;
        
        foreach (InteractableData interactableData in interactableDataList)
        {
            var instantiatedObject = Instantiate(interactableData.InteractableReference);

            IInteractable interactableComponent = instantiatedObject.GetComponent<IInteractable>();
            
            if (interactableComponent != null)
            {
                interactableComponent.SetData(interactableData);
            }
        }
    }
}
