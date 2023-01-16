using UnityEngine;

namespace CameraMovement
{
    public class AutoSwitch : MonoBehaviour
    {
        public Rigidbody2D player;
        private Camera[] _cameras;
        private AudioListener[] _audioListeners;
        
        private void Start()
        {
            _cameras = GetComponentsInChildren<Camera>();
            _audioListeners = GetComponentsInChildren<AudioListener>();
        }
        
        private void Update()
        {
            for (int i = 0; i < _cameras.Length; i++)
            {
                // Check if player is in camera view
                var playerPos = player.position;
                var cameraPos = _cameras[i].transform.position;
                var cameraSize = _cameras[i].orthographicSize;
                var cameraWidth = cameraSize * _cameras[i].aspect;
                var cameraHeight = cameraSize;
                var cameraMin = new Vector2(cameraPos.x - cameraWidth, cameraPos.y - cameraHeight);
                var cameraMax = new Vector2(cameraPos.x + cameraWidth, cameraPos.y + cameraHeight);
                var inCamera = playerPos.x > cameraMin.x && playerPos.x < cameraMax.x && playerPos.y > cameraMin.y &&
                               playerPos.y < cameraMax.y;
                _cameras[i].gameObject.SetActive(inCamera);
                //turn on audio listener only when camera is active
                _audioListeners[i].enabled = inCamera;
            }
        }
    }
}