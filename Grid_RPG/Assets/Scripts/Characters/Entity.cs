using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;


[System.Serializable]
public class Stats
{
    public TextMeshProUGUI liveText;
    public TextMeshProUGUI manaText;
    public int maxHealth;
    public int currentHealth;

    public int maxMana;
    public int currentMana;

    public int Attack;

    //public int moveRange;
    public int attackRange;

    public bool isMelee;
}
public class Entity : MonoBehaviour
{
    public enum EntityType
    {
        PlayerUnit,
        EnemyUnit
    }
    [EnumToggleButtons]
    public EntityType entityType;
    bool guarding;
    public Stats stats;
    public Sprite self;
    public CombatManager CM;
    public Animator animController;
    public Skill[] skills;
    private void Awake()
    {
        animController = GetComponent<Animator>();
    }
    void Start()
    {
        stats.currentHealth = stats.maxHealth;
        stats.currentMana = stats.maxMana;
        stats.liveText.text = stats.currentHealth.ToString();
        stats.manaText.text = stats.currentMana.ToString();
        CM = CombatManager.Instance;
    }

    public virtual void RecieveDamage(int damage)
    {
        stats.currentHealth = Mathf.Clamp(stats.currentHealth -= (int)Mathf.Floor((damage * (0.5f * (1 + (guarding == true ? 0 : 1))))), 0, stats.maxHealth);
        stats.liveText.text = stats.currentHealth.ToString();
        Vector3 pos = stats.liveText.transform.position;
        DamagePopup.Create(pos, damage);
        animController.SetTrigger("hit");
        if (stats.currentHealth == 0)
        {
            StartCoroutine(Death());
        }
    }

    public void HealEntity(int amount)
    {
        stats.currentHealth = Mathf.Clamp(stats.currentHealth += amount, 0, stats.maxHealth);
        stats.liveText.text = stats.currentHealth.ToString();
        Vector3 pos = stats.liveText.transform.position;
        DamagePopup.Create(pos, amount);

    }
    public void UpdateMana()
    {
        stats.manaText.text = stats.currentMana.ToString();
    }

    public virtual void StartOfTurn()
    {
        guarding = false;
    }

    public virtual void EndOfTurn()
    { }

    public void Guard()
    {
        guarding = true;
    }

    IEnumerator Death()
    {
        animController.SetTrigger("death");
        yield return new WaitForSeconds(1f);
        CM.RemoveEntity(gameObject.GetComponent<Movement>());
        Destroy(gameObject);
    }
}
