using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class InteractableMovementController : MonoBehaviour, IDataCollectable
{
    [Title("Movement Type"),GUIColor(.4f,1f,.4f)]
    [PropertyOrder(100)] 
    public MovementHelper.MovementTypes _movementType;
    
    [PropertyOrder(100)] 
    public float _movementSpeed = 2.5f;
    
    [PropertyOrder(100)] 
    public float _horizontalMoveOffset;
    
    [PropertyOrder(100)] 
    public float _verticalMoveOffset;
    
    [PropertyOrder(100)] 
    public float _pingPongOffset = 3.0f;

    private bool _isMoving;

    private void Awake()
    {
        if (_movementType == MovementHelper.MovementTypes.Horizontal || _movementType == MovementHelper.MovementTypes.PingPong)
        {
            MovementHelper.Move(transform, _movementType, _movementSpeed, _horizontalMoveOffset,
                _verticalMoveOffset, _pingPongOffset);
            _isMoving = true;
        }
    }
    
    public void Move()
    {
        if (!_isMoving)
        {
            MovementHelper.Move(transform, _movementType, _movementSpeed, _horizontalMoveOffset, _verticalMoveOffset, _pingPongOffset);
            _isMoving = true;
        }
    }

    public InteractableData GetInteractableData()
    {
        InteractableData interactableData = new InteractableData();
        
        LevelDataHandler.AddProperty(interactableData, (
            "MovementType", _movementType.ToString()),
            ("MovementSpeed", _movementSpeed.ToString()),
            ("HorizontalMoveOffset", _horizontalMoveOffset.ToString()),
            ("VerticalMoveOffset", _verticalMoveOffset.ToString()),
            ("PingPongOffset", _pingPongOffset.ToString()));

        return interactableData;
    }

    public GameObject GetGameObjectReference()
    {
        return gameObject;
    }
}