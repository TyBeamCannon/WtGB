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
   
    Vector3 velocity;
    Vector3 moveDirection;
    int jumpCount;
    bool isGrounded;
    bool isSprinting;

    private FarmTile currentfarmTile;
    private MiningNode currentMiningNode;
    private FishingTrigger currentFishingSpot;


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
        }
        //Tyler// Testing out the Fatigue on the character

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
