using Unity.VisualScripting;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] int moveSpeed;
    [SerializeField] int jumpForce;
    [SerializeField] int gravity;
    [SerializeField] int jumpMax;
    [SerializeField] int jumpSpeed;
    [SerializeField] int sprintMod;

    [SerializeField] FatigueManager fatigueManager;
    [SerializeField] CharacterController controller;
    [SerializeField] Animator pcAnimator;
   
    Vector3 velocity;
    Vector3 moveDirection;
    int jumpCount;
    bool isGrounded;
    bool isSprinting;

    private FarmTile currentfarmTile;
    private MiningNode currentMiningNode;
    private FishingTrigger currentFishingSpot;


    [Header("Sound Clips")]
    [SerializeField] private AudioSource soundSource;
    [SerializeField] AudioClip[] footSteps;
    [SerializeField] AudioClip miningSound;
    [SerializeField] AudioClip plantSound;
    [SerializeField] AudioClip waterSound;
    [SerializeField] AudioClip harvestSound;
    [SerializeField] AudioClip tillSound;
    [SerializeField] AudioClip castSound;
    [SerializeField] AudioClip catchSound;
    [SerializeField] AudioClip[] itemGather;

    public void PlayFootstep() => soundSource.PlayOneShot(footSteps[Random.Range(1,footSteps.Length)]);
    public void PlayGather() => soundSource.PlayOneShot(footSteps[Random.Range(1, itemGather.Length)]);
    public void PlayMine() => soundSource.PlayOneShot(miningSound);
    public void PlayPlant() => soundSource.PlayOneShot(plantSound);
    public void PlayWater() => soundSource.PlayOneShot(waterSound);
    public void PlayHarvest() => soundSource.PlayOneShot(harvestSound);
    public void PlayTill() => soundSource.PlayOneShot(tillSound);
    public void PlayCast() => soundSource.PlayOneShot(castSound);
    public void PlayCatch() => soundSource.PlayOneShot(catchSound);




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {     

        //Tyler// Testing out the Fatigue on the character
        if(Input.GetKeyDown(KeyCode.T))
        {
            fatigueManager.UseStamina(10);
            Debug.Log("Used 10 Stamina");
        }
        //Tyler// Testing out the Fatigue on the character

        if(Input.GetAxis("Horizontal") !=0 || Input.GetAxis("Vertical") !=0)
        {
            pcAnimator.SetBool("isWalking", true);
        }
        else
        {
            pcAnimator.SetBool("isWalking", false);
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            if (currentfarmTile != null)
            {
                if (!currentfarmTile.IsTilled())
                {
                    pcAnimator.SetTrigger("doTill");
                }
                else if (!currentfarmTile.IsPlanted())
                {
                    pcAnimator.SetTrigger("doPlant");
                }
                else if (!currentfarmTile.IsWatered())
                {
                    pcAnimator.SetTrigger("doWater");
                }
                else if (!currentfarmTile.IsFullyGrown())
                {
                    pcAnimator.SetTrigger("doHarvest");
                }
            }
            else if(currentMiningNode != null)
            {
                pcAnimator.SetTrigger("doMine");
            }
            else if (currentFishingSpot != null)
            {
                pcAnimator.SetTrigger("doFish");
            }
        }

        if (moveDirection.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(moveDirection.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        movement();
        Sprint();
        Jump();
    }

    void movement()
    {
        if (controller.isGrounded)
        {
            jumpCount = 0;
            velocity = Vector3.zero;
        }

        moveDirection = (transform.forward * Input.GetAxis("Vertical"))
            + (transform.right * Input.GetAxis("Horizontal"));

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        controller.Move(velocity * Time.deltaTime);
        velocity.y -= gravity * Time.deltaTime;
       
    }

    void Sprint()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            moveSpeed += sprintMod;
            isSprinting = true;
        }else if (Input.GetButtonUp("Sprint"))
        {
            moveSpeed -= sprintMod;
            isSprinting = false;
        }
    }

  
    void Jump()
    {
        if(Input.GetButtonDown("Jump") && jumpCount < jumpMax)
        {
            jumpCount++;
            velocity.y = jumpSpeed;
        }       
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out FarmTile farm)) currentfarmTile = farm;
        if (other.TryGetComponent(out MiningNode node)) currentMiningNode = node;
        if (other.TryGetComponent(out FishingTrigger spot)) currentFishingSpot = spot;

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out FarmTile farm) && farm == currentfarmTile) currentfarmTile = null;
        if (other.TryGetComponent(out MiningNode node) && node == currentMiningNode) currentMiningNode = null;
        if (other.TryGetComponent(out FishingTrigger spot) && spot == currentFishingSpot) currentFishingSpot = null;
    }
}
