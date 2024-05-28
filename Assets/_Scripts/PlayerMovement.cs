using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 7f;
    [SerializeField] private float _maximumPlayerSpeed;
    [SerializeField] private float _playerSpeedIncreaseRate;
    [SerializeField] private InputManager _inputManager;

    private Vector3 _positiveRotation = new Vector3(0, 90, 0);
    private Vector3 _negativeRotation = new Vector3(0, -90, 0);
    private CharacterController cc;

    public void Rotate(Vector2 screenPosition, float time)
    {
        if (screenPosition.x > Screen.width * 0.67)
        {
            transform.Rotate(_positiveRotation);
        }
        else if (screenPosition.x < Screen.width * 0.33)
        {
            transform.Rotate(_negativeRotation);
        }
    }

    public void Rotate(bool positiveRot)
    {
        if (positiveRot)
        {
            transform.Rotate(_positiveRotation);
        }
        else
        {
            transform.Rotate(_negativeRotation);
        }

    }

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        Physics.SyncTransforms();
        cc.Move(transform.forward * _playerSpeed * Time.deltaTime);
        if (_playerSpeed < _maximumPlayerSpeed)
        {
            _playerSpeed += Time.deltaTime * _playerSpeedIncreaseRate;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            transform.Rotate(_negativeRotation);
        else if (Input.GetKeyDown(KeyCode.D)|| Input.GetKeyDown(KeyCode.RightArrow))
            transform.Rotate(_positiveRotation);
    }
}