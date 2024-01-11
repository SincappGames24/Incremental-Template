using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour,IInteractable
{
    protected void AddProperty(InteractableData data, string propertyName, string propertyValue)
    {
        InteractableValues.PropertyValuesData propertyData = new InteractableValues.PropertyValuesData
        {
            Name = propertyName,
            Value = propertyValue
        };
        
        data.InteractableValueList.Add(new InteractableValues
        {
            PropertyValues = propertyData
        });
    }

    protected void AddTransformValues(InteractableData data, Quaternion rotation, Vector3 position)
    {
        InteractableValues.TransformValuesData transformData = new InteractableValues.TransformValuesData
        {
            Rotation = rotation,
            Position = position
        };
        
        data.TransformValues = transformData;
    }

    public abstract InteractableData GetInteractableData();
  
    public GameObject GetGameObjectReference()
    {
        return gameObject;
    }

    public void SetData(InteractableData data)
    {
        foreach (var value in data.InteractableValueList)
        {
            if (value.PropertyValues != null)
            {
                var field = this.GetType().GetField(value.PropertyValues.Name, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

                if (field != null)
                {
                    object deserializedValue = Convert.ChangeType(value.PropertyValues.Value, field.FieldType,NumberFormatInfo.InvariantInfo);
                    field.SetValue(this, deserializedValue);
                }
            }

            if (data.TransformValues != null)
            {
                transform.position = data.TransformValues.Position;
                transform.rotation = data.TransformValues.Rotation;
            }
        }
    }
}
