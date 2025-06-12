using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CombatManager : TemporalSingleton<CombatManager>
{
    public List<Movement> leftSideCharacters;
    public List<Movement> rightSideCharacters;

    public List<Movement> allCharacters;

    public List<Movement> actionList;

    private GridManager GM;

    public Button MoveButton;
    public Button AttackButton;
    public Button SkillButton;
    public Button Skill1;
    public Button Skill2;
    public Button GuardButton;
    public Image image;

    public bool buttonSelected = false;
    public bool hasMoved = false;
    bool attackConfirmed = false;
    bool skillSelected = false;
    bool wantToUseSkills = false;
    Tile.TileObject[] targetTiles = new Tile.TileObject[10];
    public Skill SkillToUse;
    List<Skill> availableSKills;
    void Start()
    {
        GM = GridManager.Instance;

        leftSideCharacters = new List<Movement>();
        rightSideCharacters = new List<Movement>();
        actionList = new List<Movement>();


        foreach (Movement mov in GM.PlayerGrid.GetAllEntities())
        {
            if (mov != null)
            {
                mov.UpdatePrio();
                leftSideCharacters.Add(mov);
            }
        }
        foreach (Movement mov in GM.EnemyGrid.GetAllEntities())
        {
            if (mov != null)
            {
                mov.UpdatePrioRight();
                rightSideCharacters.Add(mov);
            }
        }

        StartCoroutine(LateStart(0.5f));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && SkillToUse != null && skillSelected)
        {
            if (SkillToUse.cost.ValidateCost(actionList[0].entity.stats))
            {
                Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pz.z = 0;
                targetTiles = SkillToUse.GetTargets(pz);
                skillSelected = false;
            }
        }
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        StartRound();
    }

    void SortCharacters(List<Movement> list)
    {
        foreach (Movement mov in leftSideCharacters)
        {
            if (mov != null)
            {
                mov.UpdatePrio();
            }
        }
        foreach (Movement mov in rightSideCharacters)
        {
            if (mov != null)
            {
                mov.UpdatePrioRight();
            }
        }
        list.Sort(delegate (Movement a, Movement b)
        {
            if (a.GeneralPrio == b.GeneralPrio)
            {
                return 0;
            }
            else if (a.GeneralPrio > b.GeneralPrio)
            {
                return -1;
            }
            return 1;
        });
    }

    void StartTurn()
    {
        //reactivamos botones inactivos
        hasMoved = false;
        MoveButton.interactable = true;
        AttackButton.interactable = true;
        GuardButton.interactable = true;
        Skill1.interactable = false;
        Skill2.interactable = false;
        Skill1.GetComponent<SkillToolTip>().UpdateTooltip();
        Skill2.GetComponent<SkillToolTip>().UpdateTooltip();
        SkillButton.GetComponentInChildren<TextMeshProUGUI>().text = "Skills";
        Debug.Log("Turn: " + actionList[0].gameObject.name);
        if (actionList[0].entity.skills != null)
        {
            if (actionList[0].entity.skills[0] != null)
                Skill1.GetComponentInChildren<TextMeshProUGUI>().text = actionList[0].entity.skills[0].name;
            if (actionList[0].entity.skills[1] != null)
                Skill2.GetComponentInChildren<TextMeshProUGUI>().text = actionList[0].entity.skills[1].name;
        }
        actionList[0].entity.StartOfTurn();
        image.sprite = actionList[0].entity.self;
    }

    void EndTurn()
    {
        actionList[0].entity.EndOfTurn();
        actionList.Remove(actionList[0]);
        attackConfirmed = false;
        SkillToUse = null;
        skillSelected = false;
        SortCharacters(actionList);
        UncheckAllTiles();
        targetTiles = null;
        if (actionList.Count == 0)
        {
            EndRound();
        }
        StartTurn();
    }

    public void Move()
    {
        if (!buttonSelected)
        {
            if (targetTiles != null)
            {
                for (int i = 0; i < targetTiles.Length; i++)
                {
                    if (targetTiles[i] != null)
                        targetTiles[i].UnTargetTile();
                }
            }
            targetTiles = null;
            actionList[0].SelectEntity();
            actionList[0].MarkTilesInRange();
            buttonSelected = true;
            GuardButton.interactable = false;
            AttackButton.interactable = false;
            SkillButton.interactable = false;
            Skill1.interactable = false;
            Skill2.interactable = false;
            attackConfirmed = false;
            skillSelected = false;
            //UncheckTiles();

        }
        else
        {
            actionList[0].DeSelect();
            buttonSelected = false;
            GuardButton.interactable = true;
            AttackButton.interactable = true;
            SkillButton.interactable = true;
            Skill1.interactable = false;
            Skill2.interactable = false;
            UncheckAllTiles();
            if (targetTiles != null)
            {
                for (int i = 0; i < targetTiles.Length; i++)
                {
                    if (targetTiles[i] != null)
                        targetTiles[i].UnTargetTile();
                }
            }
        }

    }

    public void Attack()
    {

        if (!attackConfirmed)
        {
            Entity ent = actionList[0].GetComponent<Entity>();
            int x;
            int y;
            actionList[0].ReturnPosition(out x, out y);
            if (rightSideCharacters.Contains(actionList[0]))
            {
                targetTiles = actionList[0].myGrid.ObjectiveTiles(x, y, ent.stats.attackRange, Tile.Direction.Left);
            }
            else
            {
                targetTiles = actionList[0].myGrid.ObjectiveTiles(x, y, ent.stats.attackRange, Tile.Direction.Right);
                //Debug.Log(targetTiles);
            }

            for (int i = 0; i < targetTiles.Length; i++)
            {
                targetTiles[i].TargetTile();
            }
            attackConfirmed = true;
        }
        else
        {
            //hacemos ataque
            if (rightSideCharacters.Contains(actionList[0]))
            {
                int x;
                int y;
                //es enemigo
                Entity ent = actionList[0].GetComponent<Entity>();
                actionList[0].ReturnPosition(out x, out y);
                actionList[0].entity.animController.SetTrigger("basicAttack");
                if (ent.stats.isMelee)
                {
                    actionList[0].myGrid.MeleeDamage(x, y, ent.stats.Attack, Tile.Direction.Left, Entity.EntityType.PlayerUnit);
                }
                else
                {
                    actionList[0].myGrid.RangedDamage(x, y, ent.stats.Attack, ent.stats.attackRange, Tile.Direction.Left, Entity.EntityType.PlayerUnit);
                }

                UncheckAllTiles();
                EndTurn();
            }
            else if (leftSideCharacters.Contains(actionList[0]))
            {
                int x;
                int y;
                Entity ent = actionList[0].GetComponent<Entity>();
                actionList[0].ReturnPosition(out x, out y);
                actionList[0].entity.animController.SetTrigger("basicAttack");
                if (ent.stats.isMelee)
                {
                    actionList[0].myGrid.MeleeDamage(x, y, ent.stats.Attack, Tile.Direction.Right, Entity.EntityType.EnemyUnit);
                }
                else
                {
                    actionList[0].myGrid.RangedDamage(x, y, ent.stats.Attack, ent.stats.attackRange, Tile.Direction.Right, Entity.EntityType.EnemyUnit);
                }

                UncheckAllTiles();
                EndTurn();
            }
        }


    }

    private void UncheckAllTiles()
    {
        for (int i = 0; i < GridManager.Instance.PlayerGrid.allTiles.Length; i++)
        {
            GridManager.Instance.PlayerGrid.allTiles[i].UnTargetTile();
            GridManager.Instance.EnemyGrid.allTiles[i].UnTargetTile();
        }
    }


    public void Guard()
    {
        actionList[0].entity.Guard();
        EndTurn();
    }

    public void RemoveEntity(Movement character)
    {
        if (leftSideCharacters.Remove(character))
        {
            GM.RemoveCharacter(character);
        }
        if (rightSideCharacters.Remove(character))
        {
            GM.RemoveCharacter(character);
        }
        actionList.Remove(character);
    }

    void EndRound()
    {
        SkillToUse = null;
        skillSelected = false;
        StartRound();
    }

    void StartRound()
    {
        foreach (Movement mov in leftSideCharacters)
        {
            actionList.Add(mov);
        }
        foreach (Movement mov in rightSideCharacters)
        {
            actionList.Add(mov);
        }
        SortCharacters(actionList);
        StartTurn();
    }

    public void SelectSkill(int index)
    {
        if (SkillToUse == null)
        {
            SkillToUse = actionList[0].entity.skills[index];
            if (index == 0)
            {
                Skill2.interactable = false;
            }
            else
            {
                Skill1.interactable = false;
            }
            skillSelected = true;
        }
        else
        {
            if (SkillToUse.cost.TryApplyCost(actionList[0].entity.stats))
            {
                for (int i = 0; i < targetTiles.Length; i++)
                {
                    if (targetTiles[i] != null)
                        targetTiles[i].UnTargetTile();
                }
                switch (index)
                {
                    case 0:
                        actionList[0].entity.animController.SetTrigger("skill_1");
                        break;
                    case 1:
                        actionList[0].entity.animController.SetTrigger("skill_2");
                        break;
                    default:
                        break;
                }
                SkillToUse.Use();
                actionList[0].entity.UpdateMana();

            }
            UncheckAllTiles();
            EndTurn();
        }
    }

    public void Skills()
    {
        if (!wantToUseSkills)
        {
            SkillButton.GetComponentInChildren<TextMeshProUGUI>().text = "Cancel";
            Skill1.interactable = true;
            Skill2.interactable = true;
            GuardButton.interactable = false;
            AttackButton.interactable = false;
            MoveButton.interactable = false;
            wantToUseSkills = true;
            if (targetTiles != null)
            {
                for (int i = 0; i < targetTiles.Length; i++)
                {
                    if (targetTiles[i] != null)
                        targetTiles[i].UnTargetTile();
                }
            }
            targetTiles = null;
        }
        else
        {
            SkillButton.GetComponentInChildren<TextMeshProUGUI>().text = "Skills";
            Skill1.interactable = false;
            Skill2.interactable = false;
            GuardButton.interactable = true;
            AttackButton.interactable = true;
            if (!hasMoved)
            {
                MoveButton.interactable = true;
            }

            wantToUseSkills = false;
            if (targetTiles != null)
            {
                for (int i = 0; i < targetTiles.Length; i++)
                {
                    if (targetTiles[i] != null)
                        targetTiles[i].UnTargetTile();
                }
            }
            skillSelected = false;
            SkillToUse = null;
        }
    }

    public void FetchSkill()
    {
        foreach (Skill s in actionList[0].entity.skills)
        {

        }
    }
}
