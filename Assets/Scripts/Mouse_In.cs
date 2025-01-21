using Unity.Mathematics;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Transform Player;
    public float mouseSens = 200f;
    private float xRotation;
    void Awake()
    {
       Cursor.lockState = CursorLockMode.Locked; ///Oyun başlayınca mosue imlecini ekrandan kaldırır
    }

    void Update()
    {
        float mouseXpose = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseYpose = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
        
        xRotation -= mouseYpose;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); /// Karakterin yukarı aşşağı bakmasını sınırlandırır

        transform.localRotation = Quaternion.Euler(xRotation,0f,0f);

        Player.Rotate(Vector3.up * mouseXpose);
    }
}
