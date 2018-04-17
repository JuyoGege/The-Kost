using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    AudioSource audioSource;
    CharacterController charControl;
    Transform player;

    [SerializeField]
    float moveSpeed = 3f;

    [SerializeField]
    float stamina = 100f;

    [SerializeField]
    float initialHeight = 1.5f;

    [SerializeField]
    float crouchHeight = 0.8f;

    [Range(0, 100)]
    [SerializeField]
    float drainSpeed = 5f; //5 seconds

    [Range(1, 10)]
    [SerializeField]
    float replenishSpeedMultiplier = 4f; //how fast stamina regain

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
    float slowMultiplier = 0.7f;

    [SerializeField]
    float stepInterval = 4f;

    [SerializeField]
    AudioClip[] footstepSounds;

    private float lastHeight;

    private float horizontal;
    private float vertical;

    private float stepCycle;
    private float nextStep;

    private float speed;

    private Vector3 newPos;

    private Vector3 directionX;
    private Vector3 directionZ;

    private bool runAllowed;
    private bool refresh;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        charControl = GetComponent<CharacterController>();
        player = GetComponent<Transform>();

        speed = moveSpeed;

        stepCycle = 0f;
        nextStep = stepCycle / 2f;
        runAllowed = false;
        refresh = false;
    }

    void Update()
    {
        MovePlayer();
        if (refresh)
            Replenish();
    }

    void MovePlayer()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (vertical > 0)
        {
            if (stamina == 0)
            {
                speed = moveSpeed * slowMultiplier;
            }
            else
            {
                speed = moveSpeed;
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    if (stamina > 20)
                    {
                        runAllowed = true;
                        CancelInvoke();
                    }
                }

                if (runAllowed && stamina > 0)
                {
                    speed = moveSpeed * runMultiplier;
                    stamina -= (100f / drainSpeed) * Time.smoothDeltaTime;
                }

                else if (stamina <= 0)
                {
                    stamina = 0;
                }
            }

            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed = moveSpeed;
                runAllowed = false;
                Invoke("Refresh", 4);
            }
        }

        if (vertical < 0)
        {
            vertical *= backMoveMultiplier;
        }

        /*----------*/

        if (Input.GetKey(KeyCode.LeftControl))
        {
            speed = moveSpeed * crouchMultiplier;
            //not done
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            speed = moveSpeed;
            //not done
        }

        /*----------*/

        directionX = transform.right * horizontal * speed; //move left or right
        directionZ = transform.forward * vertical * speed; //move forward or backward

        charControl.SimpleMove(directionX);
        charControl.SimpleMove(directionZ);

        /*----------*/

        StepCycle();
    }

    void Refresh()
    {
        refresh = true;
    }

    void Replenish()
    {
        stamina += (replenishSpeedMultiplier * (100f / drainSpeed)) * Time.smoothDeltaTime;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            refresh = false;
        }

        if (stamina >= 100)
        {
            stamina = 100;
            refresh = false;
        }
    }

    void StepCycle()
    {
        if (charControl.velocity.sqrMagnitude > 0 && (horizontal != 0 || vertical != 0))
        {
            stepCycle += (charControl.velocity.magnitude + (speed * (!runAllowed ? 1f : 1.5f))) * Time.smoothDeltaTime;
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