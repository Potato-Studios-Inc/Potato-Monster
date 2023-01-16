using System;
using UnityEngine;

namespace CameraMovement
{
    public class AutoMove : MonoBehaviour
    {
        public Rigidbody2D player;
        public Camera camera;

        private void Update()
        {
            var playerPos = player.position;
            var (cameraMin, cameraMax) = CameraViewVectors();
            var playerInView = InView(playerPos, cameraMin, cameraMax);
            var cameraSize = camera.orthographicSize;
            var cameraWidth = 2 * cameraSize * camera.aspect;
            var cameraHeight = 2 * cameraSize;
            switch (playerInView)
            {
                case "in":
                    return;
                case "left":
                    camera.transform.position += Vector3.left * cameraWidth;
                    break;
                case "right":
                    camera.transform.position += Vector3.right * cameraWidth;
                    break;
                case "up":
                    camera.transform.position += Vector3.up * cameraHeight;
                    break;
                case "down":
                    camera.transform.position += Vector3.down * cameraHeight;
                    break;
            }
        }

        public static String InView(Vector2 objPos, Vector2 viewMin, Vector2 viewMax)
        {
            var rightOfMin = objPos.x > viewMin.x;
            var leftOfMax = objPos.x < viewMax.x;
            var aboveMin = objPos.y > viewMin.y;
            var belowMax = objPos.y < viewMax.y;
            if (rightOfMin && leftOfMax && aboveMin && belowMax)
            {
                return "in";
            }

            if (rightOfMin && leftOfMax)
            {
                if (aboveMin)
                {
                    return "up";
                }

                return "down";
            }

            if (aboveMin && belowMax)
            {
                if (rightOfMin)
                {
                    return "right";
                }

                return "left";
            }

            return "out of view";
        }

        public (Vector2 cameraMin, Vector2 cameraMax) CameraViewVectors()
        {
            var cameraPos = camera.transform.position;
            var cameraSize = camera.orthographicSize;
            var cameraWidth = cameraSize * camera.aspect;
            var cameraHeight = cameraSize;
            var cameraMin = new Vector2(cameraPos.x - cameraWidth, cameraPos.y - cameraHeight);
            var cameraMax = new Vector2(cameraPos.x + cameraWidth, cameraPos.y + cameraHeight);
            return (cameraMin, cameraMax);
        }
    }
}