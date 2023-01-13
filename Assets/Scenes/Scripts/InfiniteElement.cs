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

        void Update()
        {
            var children = GetComponentsInChildren<Transform>();
            if (children.Length == 0)
            {
                return;
            }
            for (int i = 0; i < children.Length; i++)
            {
                if (elementYFillsCameraView(children[i]))
                {
                    return;
                }
            }

            // Spawn a new element in the camera view
            var (cameraMin, cameraMax) = AutoMove.CameraViewVectors();
            var newElement = Instantiate(prefab, transform);
            newElement.transform.position = new Vector3(prefab.transform.position.x, cameraMax.y + newElement.transform.localScale.y, prefab.transform.position.z);
        }

        bool elementYFillsCameraView(Transform element)
        {
            var (cameraMin, cameraMax) = AutoMove.CameraViewVectors();
            var elementMinY = element.transform.position.y - element.transform.localScale.y / 2;
            var elementMaxY = element.transform.position.y + element.transform.localScale.y / 2;
            return elementMinY < cameraMin.y && elementMaxY > cameraMax.y;
        }
    }
}