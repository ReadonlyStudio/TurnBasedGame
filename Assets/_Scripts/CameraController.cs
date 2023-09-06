using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private const float MIN_FOLLOW_Y_OFFSET = 2f;
    private const float MAX_FOLLOW_Y_OFFSET = 12f;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineTransposer cineOptions;
    private Vector3 targetFollowOffset;

    private void Start()
    {
        cineOptions = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetFollowOffset = cineOptions.m_FollowOffset;
    }
    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }
    private void HandleMovement()
    {
        Vector3 inputMoveDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.UpArrow))
        {
            inputMoveDir.z = 1f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            inputMoveDir.z = -1f;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            inputMoveDir.x = -1f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            inputMoveDir.x = 1f;
        }
        float moveSpeed = 10f;
        Vector3 moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
        transform.position += moveVector * moveSpeed * Time.deltaTime;
    }
    private void HandleRotation()
    {
        Vector3 rotationVector = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.A))
        {
            rotationVector.y = 1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rotationVector.y = -1f;
        }
        float rotationSpeed = 100f;
        transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
    }
    private void HandleZoom()
    {
        float zoomAmount = 1f;
        if (Input.mouseScrollDelta.y > 0)
        {
            targetFollowOffset.y -= zoomAmount;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            targetFollowOffset.y += zoomAmount;
        }
        float zoomSpeed = 5f;
        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);
        cineOptions.m_FollowOffset =
            Vector3.Lerp(cineOptions.m_FollowOffset, targetFollowOffset, Time.deltaTime * zoomSpeed);
    }
}
