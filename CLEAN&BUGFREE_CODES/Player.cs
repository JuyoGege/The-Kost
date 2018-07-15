using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CharacterController))]

public class Player : MonoBehaviour
{
    AudioSource audioSource;
    CharacterController charControl;
    public GameManager gm;
    Transform player;

    /*Default Settings:
     mSpeed 3
     stamina 100
     initialHeight 1.5
     crouchHeight 0.8
     staminaDrain 5
     replenish 4
     sideMove 0.8
     backMove 0.5
     crouchSpeed 0.5
     runSpeed 1.2
     slowSpeed 0.6
     stepInterval 4
         */


    [Header("General Settings")]
    public float healthPoint;
    public float moveSpeed;
    public float initialHeight;
    public float crouchHeight;
    public bool isDead;

    [Header("Stamina Settings")]
    public float stamina;
    public float staminaDrainTime;
    public float replenishTime;

    [Header("Movement Multiplier")]
    [Range(0.5f, 1f)]
    public float side;
    [Range(0.5f, 1f)]
    public float back;
    [Range(0.5f, 1f)]
    public float crouch;
    [Range(1f, 2f)]
    public float run;
    [Range(0.5f, 1f)]
    public float slow;

    [Header("Footstep Settings")]
    public float stepInterval;
    public AudioClip[] footstepSounds;

    /*----------*/

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

    private float lastHeight;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        charControl = GetComponent<CharacterController>();
        gm = FindObjectOfType<GameManager>();
        player = GetComponent<Transform>();
    }

    void Start()
    {
        isDead = false;
        speed = moveSpeed;
        stepCycle = 0f;
        nextStep = stepCycle / 2f;
        runAllowed = false;
        refresh = false;
    }

    void Update()
    {
        Health();

        if (Input.GetKeyDown(KeyCode.G))
        {
                healthPoint = 0f;
        }

        if (!isDead)
        {
            MovePlayer();

            if (refresh)
            {
                Replenish();
            }
        }
    }

    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    void MovePlayer()
    {

        if (vertical > 0)
        {
            if (stamina == 0)
            {
                speed = moveSpeed * slow;
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
                    speed = moveSpeed * run;
                    stamina -= (100f / staminaDrainTime) * Time.smoothDeltaTime;
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
        { // move backward
            vertical *= back;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            player.position = new Vector3(player.position.x, player.position.y * (crouchHeight / initialHeight), player.position.z);
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            player.localScale =
                new Vector3(player.localScale.x, Mathf.Lerp(player.localScale.y, crouchHeight, 10f * Time.smoothDeltaTime), player.localScale.z);
            speed = moveSpeed * crouch;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            player.position = new Vector3(player.position.x, player.position.y * (initialHeight / crouchHeight), player.position.z);
            player.localScale = new Vector3(player.localScale.x, initialHeight, player.localScale.z);
            speed = moveSpeed;
        }

        directionX = transform.right * horizontal * speed; //move left or right
        directionZ = transform.forward * vertical * speed; //move forward or backward

        charControl.SimpleMove(directionX);
        charControl.SimpleMove(directionZ);

        StepCycle();
    }

    void Refresh()
    {
        refresh = true;
    }

    void Replenish()
    {
        stamina += (replenishTime * (100f / staminaDrainTime)) * Time.smoothDeltaTime;

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
            stepCycle += (charControl.velocity.magnitude + (speed * (!runAllowed ? 1f : 1.5f))) * Time.smoothDeltaTime;

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

    void Health()
    {
        if (isDead)
        {
            print("You're Dead");
            gm.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }

        if (healthPoint <= 0)
        {
            healthPoint = 0;
            isDead = true;
        }
    }
}