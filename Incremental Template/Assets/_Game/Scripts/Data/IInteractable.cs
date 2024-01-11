using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
  InteractableData GetInteractableData();
  GameObject GetGameObjectReference();
  void SetData(InteractableData data);
}
