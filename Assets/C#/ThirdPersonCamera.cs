using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    // Публичные переменные для настройки в инспекторе
    public Transform target; // Сюда нужно перетащить объект персонажа
    public float mouseSensitivity = 2f;
    public float maxViewAngle = 60f;
    public float minViewAngle = -60f;
    private Transform trCam;
    public Vector3 offset;
    [SerializeField] private float transitionSpeed = 9f;
    // Приватные переменные
    private float rotationX = 0f;
    private float rotationY = 0f;
    Vector3 input;
    void Start()
    {
        trCam = GetComponent<Transform>();
        offset = trCam.position - target.position;
        // Скрываем курсор мыши и блокируем его в центре экрана
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        input = HandleMouseRotation();
    }
    void LateUpdate()
    {
        // Управление вращением камеры с помощью мыши
        

        FollowCamera();

        RotateCamera(input);
    }
    private void RotateCamera(Vector3 input)
    {
        Quaternion newRot = Quaternion.Euler(input.x, input.y, 0);
        trCam.rotation = Quaternion.Slerp(trCam.rotation, newRot, Time.smoothDeltaTime * transitionSpeed);
    }

    public void FollowCamera()
    {
        Vector3 newPosition = trCam.localRotation * offset + target.position;
        trCam.position = Vector3.Lerp(trCam.position, newPosition, Time.deltaTime * transitionSpeed);
    }

    Vector3 HandleMouseRotation()
    {
        // Получаем ввод мыши
        rotationX += Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationY -= Input.GetAxis("Mouse Y") * mouseSensitivity;


        // Ограничиваем угол обзора по вертикали
        rotationY = Mathf.Clamp(rotationY, minViewAngle, maxViewAngle);

        return new Vector3(rotationY, rotationX, 0);
    }
}