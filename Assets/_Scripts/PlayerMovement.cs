using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 7f;
    [SerializeField] private float maximumPlayerSpeed;
    [SerializeField] private float playerSpeedIncreaseRate;

    private Vector3 positiveRotation = new Vector3(0, 90, 0);
    private Vector3 negativeRotation = new Vector3(0, -90, 0);
    private CharacterController cc;

    public void Rotate(Vector2 screenPosition, float time)
    {
        if (screenPosition.x > Screen.width * 0.67)
        {
            transform.Rotate(positiveRotation);
        }
        else if (screenPosition.x < Screen.width * 0.33)
        {
            transform.Rotate(negativeRotation);
        }
    }

    public void Rotate(bool positiveRot)
    {
        if (positiveRot)
        {
            transform.Rotate(positiveRotation);
        }
        else
        {
            transform.Rotate(negativeRotation);
        }

    }

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        Physics.SyncTransforms();
        cc.Move(transform.forward * playerSpeed * Time.deltaTime);
        if (playerSpeed < maximumPlayerSpeed)
        {
            playerSpeed += Time.deltaTime * playerSpeedIncreaseRate;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            transform.Rotate(negativeRotation);
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            transform.Rotate(positiveRotation);
    }
}