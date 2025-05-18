using UnityEngine;
using UnityEngine.UI;


public class FatigueManager : MonoBehaviour
{
    [Header("Stamina")]
    [SerializeField] int currentStamina;
    [SerializeField] int maxStamina;
    [SerializeField] int fullStaminaCap = 100;
    [SerializeField] int minStaminaFloor = 50;
    [SerializeField] int fatigueLossPerDay = 10;
    [SerializeField] float regenRate = 1f;
    [SerializeField] int regenAmount = 1;

    [SerializeField] float regenTimer = 0f;
    [SerializeField] bool restedToday = true;

    [SerializeField] private Slider staminaSlider;
 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
       
        maxStamina = fullStaminaCap;
        currentStamina = maxStamina;
        UpdateStaminaUI();
    }

    // Update is called once per frame
    void Update()
    {
        RegenStamina(Time.deltaTime);
    }

    public void RegenStamina(float delteTime)
    {
        if(currentStamina < maxStamina)
        {
            regenTimer += delteTime;
            if(regenTimer >= regenRate)
            {
                currentStamina += regenAmount;
                currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
                regenTimer = 0f;
                UpdateStaminaUI();
            }
        }
    }

    public bool UseStamina(int amount)
    {
        if (currentStamina >= amount)
        {
            currentStamina -= amount;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            UpdateStaminaUI();
            return true;
        }
        else
        {
            Debug.Log("Not Enough Stamina!");
            return false;
        }
    }

    public void EndOfDayCheck()
    {
        if (!restedToday)
        {
            maxStamina -= fatigueLossPerDay;
            maxStamina = Mathf.Clamp(maxStamina, minStaminaFloor, fullStaminaCap);
        }

        restedToday = false;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
    }

    public void RestoreStamina()
    {
        restedToday = true;
        maxStamina = fullStaminaCap;
        currentStamina = maxStamina;
        UpdateStaminaUI() ;
    }

    private void UpdateStaminaUI()
    {
        if(staminaSlider != null)
        {
            staminaSlider.value = (float)currentStamina / maxStamina;
        }
    }

}
