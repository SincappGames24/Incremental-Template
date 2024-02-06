using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public static class MovementHelper
{
    public enum MovementTypes
    {
        Standard,
        Horizontal,
        Backward,
        Forward,
        Cross,
        PingPong
    }

    public static void Move(Transform obj, MovementTypes movementType, float movementSpeed,
        float horizontalMoveOffset,
        float verticalMoveOffset, float pingPongOffset, Action callback = null)
    {
        float targetXPosHorizontalMove;

        if (obj.transform.position.x > 0)
        {
            targetXPosHorizontalMove = 1.65f;
        }
        else
        {
            targetXPosHorizontalMove = -1.65f;
        }

        Tween moveTween = null;

        if (movementType == MovementTypes.Cross)
        {
            moveTween = CrossMove(verticalMoveOffset, targetXPosHorizontalMove, movementSpeed, obj);
        }
        else if (movementType == MovementTypes.Horizontal)
        {
            HorizontalMove(horizontalMoveOffset, targetXPosHorizontalMove, movementSpeed, obj);
        }
        else if (movementType == MovementTypes.PingPong)
        {
            PingPongMove(movementSpeed, pingPongOffset, obj);
        }
        else if (movementType == MovementTypes.Backward || movementType == MovementTypes.Forward)
        {
            moveTween = VerticalMove(verticalMoveOffset, movementSpeed, movementType, obj);
        }

        moveTween.OnComplete(() => { callback?.Invoke(); });
    }

    private static void PingPongMove(float movementSpeed, float distance, Transform obj)
    {
        float targetPos = obj.position.z + distance;

        obj.DOMoveZ(targetPos, movementSpeed).SetSpeedBased().SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo)
            .OnComplete(() => { targetPos = obj.position.z - distance; });
    }

    private static void HorizontalMove(float moveOffset, float targetXPosHorizontalMove, float movementSpeed,
        Transform obj)
    {
        if (moveOffset != 0)
        {
            float startX = obj.position.x;
            targetXPosHorizontalMove = startX + moveOffset;

            obj.DOMoveX(targetXPosHorizontalMove, movementSpeed).SetEase(Ease.Linear).SetSpeedBased()
                .OnComplete(() =>
                {
                    targetXPosHorizontalMove = startX - moveOffset;
                    obj.DOMoveX(targetXPosHorizontalMove, movementSpeed).SetSpeedBased()
                        .SetEase(Ease.Linear)
                        .SetLoops(-1, LoopType.Yoyo).OnComplete(() =>
                        {
                            targetXPosHorizontalMove = startX + moveOffset;
                        });
                });
        }
        else
        {
            obj.DOMoveX(targetXPosHorizontalMove, movementSpeed).SetEase(Ease.Linear).SetSpeedBased()
                .OnComplete(
                    () =>
                    {
                        targetXPosHorizontalMove = -targetXPosHorizontalMove;
                        obj.DOMoveX(targetXPosHorizontalMove, movementSpeed).SetSpeedBased()
                            .SetEase(Ease.Linear)
                            .SetLoops(-1, LoopType.Yoyo).OnComplete(() =>
                            {
                                targetXPosHorizontalMove = -targetXPosHorizontalMove;
                            });
                    });
        }
    }

    private static Tween CrossMove(float verticalMoveOffset, float targetXPosHorizontalMove, float movementSpeed,
        Transform obj)
    {
        float distance = 10000;
        Tween xMoveTween = null;

        if (verticalMoveOffset != 0)
        {
            distance = verticalMoveOffset;
        }

        xMoveTween = obj.DOMoveX(targetXPosHorizontalMove, movementSpeed).SetEase(Ease.Linear)
            .SetSpeedBased().OnComplete(
                () =>
                {
                    targetXPosHorizontalMove = -targetXPosHorizontalMove;
                    xMoveTween = obj.DOMoveX(targetXPosHorizontalMove, movementSpeed).SetSpeedBased()
                        .SetEase(Ease.Linear)
                        .SetLoops(-1, LoopType.Yoyo).OnComplete(() =>
                        {
                            targetXPosHorizontalMove = -targetXPosHorizontalMove;
                        });
                });

        return obj.DOMoveZ(obj.position.z + distance, movementSpeed).SetEase(Ease.Linear).SetSpeedBased()
            .OnComplete(
                () => { xMoveTween.Kill(); });
    }

    private static Tween VerticalMove(float verticalMoveOffset, float movementSpeed, MovementTypes moveType,
        Transform obj)
    {
        float distance = 10000;

        if (verticalMoveOffset != 0)
        {
            distance = verticalMoveOffset;
        }

        if (moveType == MovementTypes.Backward)
        {
            distance *= -1;
        }

        return obj.DOMoveZ(obj.position.z + distance, movementSpeed).SetEase(Ease.Linear).SetSpeedBased();
    }
}