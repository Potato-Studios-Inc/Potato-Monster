using System.Collections.Generic;
using System.Linq;
using CameraMovement;
using UnityEngine;

namespace Scenes.Scripts
{
    public class InfiniteElement : MonoBehaviour
    {
        public AutoMove AutoMove;
        public GameObject prefab;
        private Vector3 _lastPosition;
        private float _elementHeight;

        void Start()
        {
            _lastPosition = prefab.transform.position;
            var prefabSprite = prefab.GetComponentInChildren<SpriteRenderer>();
            _elementHeight = prefabSprite.size.y * prefabSprite.transform.localScale.y;
            Debug.Log("Element height: " + _elementHeight);
        }

        private Vector3 GetNextPosition()
        {
            var next = _lastPosition;
            next.y = _lastPosition.y + _elementHeight;

            return next;
        }

        void Update()
        {
            var (cameraMin, cameraMax) = AutoMove.CameraViewVectors();

            if (cameraMax.y < _lastPosition.y) return;
            var pos = GetNextPosition();
            var newPlatform = Instantiate(prefab, transform);
            newPlatform.transform.position = pos;
            _lastPosition = pos;
        }
    }
}