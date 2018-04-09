using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    private AudioSource audioSource;
    private CharacterController charControl;

    [SerializeField]
    float moveSpeed = 3f;

    [SerializeField]
    float stamina = 100f;

    [Range(0, 100)]
    [SerializeField]
    float drainSpeed = 5f; //5 seconds

    [SerializeField]
    float stepInterval = 4f;

    [Range(0, 1)]
    [SerializeField]
    float sideMoveMultiplier = 0.8f;

    [Range(0, 1)]
    [SerializeField]
    float backMoveMultiplier = 0.5f;

    [Range(0, 1)]
    [SerializeField]
    float crouchMultiplier = 0.5f;

    [Range(1, 2)]
    [SerializeField]
    float runMultiplier = 1.2f;

    [Range(0, 1)]
    [SerializeField]
    float slowMultiplier = 0.8f;

    [SerializeField]
    AudioClip[] footstepSounds;

    private float horizontal;
    private float vertical;
    
    private float stepCycle;
    private float nextStep;

    private float speed;
    private bool isWalking;

    private Vector3 directionX;
    private Vector3 directionZ;

    private bool runAllowed;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        charControl = GetComponent<CharacterController>();

        stepCycle = 0f;
        nextStep = stepCycle / 2f;

        isWalking = true;
        runAllowed = false;
    }

    void Update()
    {
        isWalking = true;
        speed = isWalking ? moveSpeed : (moveSpeed * runMultiplier);
        MovePlayer();
    }

    void MovePlayer()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        horizontal *= sideMoveMultiplier;

        if (vertical > 0)
        {
            if(stamina == 0)
            {
                vertical *= slowMultiplier;
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                CancelInvoke();

                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    Debug.Log("Shift is pressed");
                    if (stamina > 60)
                    {
                        Debug.Log("Stamina is enough to run");
                        runAllowed = true;
                    }
                }

                if (runAllowed && stamina > 0)
                {
                    Debug.Log("Running");
                    isWalking = false;
                    vertical *= runMultiplier;
                    stamina -= (100f / drainSpeed) * Time.smoothDeltaTime;
                }

                else if (stamina <= 0)
                {
                    Debug.Log("Stamina drained completely");
                    stamina = 0;
                }
            }

            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                Debug.Log("Shift is released");
                runAllowed = false;
                Invoke("Refresh", 5);
            }
        }

        if (vertical < 0)
        {
            vertical *= backMoveMultiplier;
        }

        directionX = transform.right * horizontal * moveSpeed; //move left or right
        directionZ = transform.forward * vertical * moveSpeed; //move forward or backward

        charControl.SimpleMove(directionX);
        charControl.SimpleMove(directionZ);

        StepCycle();
    }

    void Refresh()
    {
        Debug.Log("Refreshing");
        while (stamina < 100)
        {
            stamina += (2 * drainSpeed) * Time.smoothDeltaTime;
            if (stamina > 100)
            {
                stamina = 100;
            }
        }
    }

    void StepCycle()
    {
        if (charControl.velocity.sqrMagnitude > 0 && (horizontal != 0 || vertical != 0))
        {
            stepCycle += (charControl.velocity.magnitude + (speed * (isWalking ? 1f : 1.5f))) * Time.smoothDeltaTime;
        }

        if (!(stepCycle > nextStep))
        {
            return;
        }
        
        nextStep = stepCycle + stepInterval;

        PlayStep();
    }

    void PlayStep()
    {
        int n = Random.Range(1, footstepSounds.Length);
        audioSource.clip = footstepSounds[n];
        audioSource.PlayOneShot(audioSource.clip);
        footstepSounds[n] = footstepSounds[0];
        footstepSounds[0] = audioSource.clip;   
    }

}