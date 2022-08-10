using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    // TODO: Supports pixel perfect camera
    
    public float speed = 10.0f;
    public float scrollSpeed = 1;
    public float minCameraScale = 1;
    public float maxCameraScale = 10;

    private Camera _camera;
    private float _cameraScale = 8;
    private bool isPointerDown = false;
    
    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _camera.orthographic = true;
    }

    private void Update()
    {
        Movement(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Scrolling();
    }

    private void Movement(float horizontal, float vertical)
    {
        float speedReverse = 0.1f * _cameraScale * this.speed;
        transform.Translate(Vector2.right * horizontal * speedReverse * Time.deltaTime);
        transform.Translate(Vector2.up * vertical * speedReverse * Time.deltaTime);
    }

    private void Scrolling()
    {
        if (Input.GetKey(KeyCode.Q)) _cameraScale += scrollSpeed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.E)) _cameraScale -= scrollSpeed * Time.deltaTime;

        _cameraScale = Mathf.Clamp(_cameraScale, minCameraScale, maxCameraScale);
        _camera.orthographicSize = _cameraScale * Screen.height / Screen.width * 0.5f;
    }
}
