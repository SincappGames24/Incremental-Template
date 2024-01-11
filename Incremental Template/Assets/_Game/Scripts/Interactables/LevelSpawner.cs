using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    private void Awake()
    {
        string path = $"LevelDatas/Level_{PersistData.Instance.CurrentLevel}_Data";
        var interactableDataContainer = Resources.Load<InteractableDataContainer>(path);
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
