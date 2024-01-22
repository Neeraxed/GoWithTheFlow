using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 7f;
    [SerializeField] private float maximumPlayerSpeed;
    [SerializeField] private float playerSpeedIncreaseRate;
    [SerializeField] private InputManager inputManager;

    private Vector3 positiveRotation = new Vector3(0, 90, 0);
    private Vector3 negativeRotation = new Vector3(0, -90, 0);
    private CharacterController cc;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        //inputManager.OnStartTouch += Rotate;
    }

    private void Update()
    {
        Physics.SyncTransforms();
        cc.Move(transform.forward * playerSpeed * Time.deltaTime);

        if (playerSpeed < maximumPlayerSpeed)
        {
            playerSpeed += Time.deltaTime * playerSpeedIncreaseRate;
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            transform.Rotate(negativeRotation);
        else if (Input.GetKeyDown(KeyCode.D)|| Input.GetKeyDown(KeyCode.RightArrow))
            transform.Rotate(positiveRotation);
    }

    private void OnDestroy()
    {
        //inputManager.OnEndTouch -= Rotate;
    }

    // private void OnEnable()
    // {
    //     inputManager.OnStartTouch += Rotate;
    // }
    // private void OnDisable()
    // {
    //     inputManager.OnEndTouch -= Rotate;
    // }

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
}