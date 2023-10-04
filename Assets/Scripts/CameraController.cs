using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float sensibility = -1f;
    private Vector3 _rotate;

    private float _y, _x;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            _y = Input.GetAxis("Mouse X");
            //_x = Input.GetAxis("Mouse Y");
            _rotate = new Vector3(0, _y * sensibility, 0);
            transform.eulerAngles = transform.eulerAngles - _rotate;
        }

    }
}
