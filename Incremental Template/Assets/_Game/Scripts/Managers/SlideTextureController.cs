using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SlideTextureController : MonoBehaviour
{
    [SerializeField] private float _speed = 3.63f;
    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        _meshRenderer.material.mainTextureOffset+=Vector2.down * (Time.deltaTime * _speed);
    }
}
