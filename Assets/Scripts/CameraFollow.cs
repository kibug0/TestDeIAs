using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private float cameraSmoothVelocity = 0.0f;

    [SerializeField] private Camera _playerCamera;

    private void Awake()
    {
        _playerCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        var posicaoDesejada = this.transform.position + cameraOffset;
        var posicaoSuavizada = Vector3.Lerp(_playerCamera.transform.position, posicaoDesejada, cameraSmoothVelocity);
        _playerCamera.transform.position = posicaoSuavizada;
    }
}
