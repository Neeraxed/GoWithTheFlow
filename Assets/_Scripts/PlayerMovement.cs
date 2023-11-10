using UnityEngine;

public class PlayerMovement : MonoBehaviour
{  
    [SerializeField] private float speedMultiplier;
    [SerializeField] private InputManager inputManager;

    private Vector3 positiveRotation = new Vector3(0, 90, 0);
    private Vector3 negativeRotation = new Vector3(0, -90, 0);
    private CharacterController cc;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Physics.SyncTransforms();
        cc.Move(transform.forward * speedMultiplier * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.A))
            transform.Rotate(negativeRotation);
        else if(Input.GetKeyDown(KeyCode.D))
            transform.Rotate(positiveRotation);
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += Rotate;
    }
    private void OnDisable()
    {
        inputManager.OnEndTouch -= Rotate;
    }

    public void Rotate(Vector2 screenPosition, float time)
    {
        if (screenPosition.x >  Screen.width * 0.67)
        {
            transform.Rotate(positiveRotation);
        }
        else if (screenPosition.x < Screen.width * 0.33)
        {
            transform.Rotate(negativeRotation);
        }
    }
}
