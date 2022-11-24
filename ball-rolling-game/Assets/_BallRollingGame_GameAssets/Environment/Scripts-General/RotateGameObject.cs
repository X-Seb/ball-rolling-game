using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RotateGameObject : MonoBehaviour
{
    [Header("The game object to move: ")]
    [SerializeField] private Transform _transform;

    [SerializeField] float rotationSpeed;
    [Header("The value for the rotation on each axis")]
    [SerializeField] float _xRotation;
    [SerializeField] float _yRotation;
    [SerializeField] float _zRotation;
    [Header("How long should each rotation last?")]
    [SerializeField] float _rotationDuration;
    [Header("How many rotation should the object do? (Input -1 for infinite rotations)")]
    [SerializeField] int _loops;
    [Header("How should the game object rotate?")]
    [SerializeField] RotateMode _rotationMode;
    [SerializeField] Ease _ease;

    private void Start()
    {
        Vector3 _rotation = new Vector3(_xRotation, _yRotation, _zRotation);

        _transform.DORotate(_rotation, _rotationDuration, _rotationMode).SetEase(_ease).SetLoops(_loops);
    }
}
