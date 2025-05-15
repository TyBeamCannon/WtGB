using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;


    [SerializeField] FatigueManager fatigueManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(horizontal, 0f, vertical).normalized;


        transform.position += moveDir * moveSpeed * Time.deltaTime;

        //Tyler// Testing out the Fatigue on the character
        if(Input.GetKeyDown(KeyCode.T))
        {
            fatigueManager.UseStamina(10);
            Debug.Log("Used 10 Stamina");
        }
        //Tyler// Testing out the Fatigue on the character
    }
}
