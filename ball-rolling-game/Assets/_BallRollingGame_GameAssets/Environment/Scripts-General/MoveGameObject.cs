using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class MoveGameObject : MonoBehaviour
{
    [Header("The game objects to move (with the same displacement values): ")]
    [SerializeField] private List<Transform> _transformList;
    [Header("Modify the components of the displacement: ")]
    [SerializeField] private float _xDisplacement;
    [SerializeField] private float _yDisplacement;
    [SerializeField] private float _zDisplacement;

    [Header("Time for the gameObjec(s) to move: ")]
    [SerializeField] private float _cycleTime = 2.0f;
    [Header("The type of easing that will be applied to the movement: ")]
    [SerializeField] private Ease _typeOfEasing;
    [Header("Number of loops to do the movement (input -1 for infinite loops): ")]
    [SerializeField] private int _numberOfLoops;
    [Header("The type of looping (Use 'yoyo' to make it go back to where it started)")]
    [SerializeField] private LoopType _loopType;

    private void Start()
    {
        // Actually move the gameObjects.
        for (int i = 0; i < _transformList.Count; i++)
        {
            // Use the objects starting position and displacement values to create the final position vector.
            Vector3 _startingPos = new Vector3(_transformList[i].position.x,
                _transformList[i].position.y, _transformList[i].position.z);
            Vector3 targetPos = new Vector3(_startingPos.x + _xDisplacement,
                _startingPos.y + _yDisplacement, _startingPos.z + _zDisplacement);

            _transformList[i].DOMove(targetPos, _cycleTime).SetEase(_typeOfEasing).SetLoops(_numberOfLoops, _loopType);
        }
    }
}