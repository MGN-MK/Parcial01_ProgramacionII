using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CombatStatus
{
    INITIAL_WAIT,
    WAITING_FOR_FIGHTER,
    FIGHTER_ACTION,
    CHECK_ACTION_MESSAGES,
    CHECK_FOR_VICTORY,
    NEXT_TURN,
    CHECK_FIGHTER_STATUS_CONDITION
}

public class CombatManager : MonoBehaviour
{
    private Character[] playerTeam;
    private Character[] enemyTeam;

    private Character[] characters;
    private int chrIndex;

    private bool combating;
    private string initialText;
    private float initialWait;

    private CombatStatus combatStatus;

    private Skill currentChrSkill;

    private List<Character> returnBuffer;

    void Start()
    {
        returnBuffer = new List<Character>();

        characters = GameObject.FindObjectsOfType<Character>();

        SortCharactersBySpeed();
        MakeTeams();

        WriteInitialText();
        LogPanel.Log(initialText);

        combatStatus = CombatStatus.INITIAL_WAIT;

        chrIndex = -1;
        combating = true;
        StartCoroutine(CombatLoop());
    }

    private void SortCharactersBySpeed()
    {
        bool sorted = false;
        while (!sorted)
        {
            sorted = true;

            for (int i = 0; i < characters.Length - 1; i++)
            {
                Character a = characters[i];
                Character b = characters[i + 1];

                float aSpeed = a.GetCurrentStats().speed;
                float bSpeed = b.GetCurrentStats().speed;

                if (bSpeed > aSpeed)
                {
                    characters[i] = b;
                    characters[i + 1] = a;

                    sorted = false;
                }
            }
        }
    }

    private void MakeTeams()
    {
        List<Character> playersBuffer = new List<Character>();
        List<Character> enemiesBuffer = new List<Character>();

        foreach (var chrs in this.characters)
        {
            if (chrs.team == Team.PLAYERS)
            {
                playersBuffer.Add(chrs);
            }
            else if (chrs.team == Team.ENEMIES)
            {
                enemiesBuffer.Add(chrs);
            }

            chrs.combatManager = this;
        }

        playerTeam = playersBuffer.ToArray();
        enemyTeam = enemiesBuffer.ToArray();
    }

    IEnumerator CombatLoop()
    {
        while (combating)
        {
            switch (combatStatus)
            {
                case CombatStatus.WAITING_FOR_FIGHTER:
                    yield return null;
                    break;

                case CombatStatus.FIGHTER_ACTION:
                    LogPanel.Log(characters[chrIndex].nameID + " usó " + currentChrSkill.skillName + ".");

                    yield return null;

                    // Executing fighter skill
                    currentChrSkill.Run();

                    // Wait for fighter skill animation
                    yield return new WaitForSeconds(currentChrSkill.animationDuration);
                    combatStatus = CombatStatus.CHECK_ACTION_MESSAGES;

                    break;
                case CombatStatus.CHECK_ACTION_MESSAGES:
                    string nextMessage = currentChrSkill.GetNextMessage();

                    if (nextMessage != null)
                    {
                        LogPanel.Log(nextMessage);
                        yield return new WaitForSeconds(2f);
                    }
                    else
                    {
                        currentChrSkill = null;
                        combatStatus = CombatStatus.CHECK_FOR_VICTORY;
                        yield return null;
                    }
                    break;

                case CombatStatus.CHECK_FOR_VICTORY:
                    bool arePlayersAlive = false;
                    foreach (var figther in playerTeam)
                    {
                        arePlayersAlive |= figther.isAlive;
                    }

                    // if (this.playerTeam[0].isAlive OR this.playerTeam[1].isAlive)

                    bool areEnemiesAlive = false;
                    foreach (var figther in enemyTeam)
                    {
                        areEnemiesAlive |= figther.isAlive;
                    }

                    bool victory = areEnemiesAlive == false;
                    bool defeat  = arePlayersAlive == false;

                    if (victory)
                    {
                        LogPanel.Log("Victoria!");
                        combating = false;
                    }

                    if (defeat)
                    {
                        LogPanel.Log("Derrota!");
                        combating = false;
                    }

                    if (combating)
                    {
                        combatStatus = CombatStatus.NEXT_TURN;
                    }

                    yield return null;
                    break;
                case CombatStatus.NEXT_TURN:
                    yield return new WaitForSeconds(1f);

                    Character current = null;

                    do {
                        chrIndex = (chrIndex + 1) % characters.Length;

                        current = characters[chrIndex];
                    } while (current.isAlive == false);

                    combatStatus = CombatStatus.CHECK_FIGHTER_STATUS_CONDITION;

                    break;

                case CombatStatus.INITIAL_WAIT:
                    yield return new WaitForSeconds(initialWait);
                    combatStatus = CombatStatus.NEXT_TURN;
                    break;

                case CombatStatus.CHECK_FIGHTER_STATUS_CONDITION:
                    var currentFighter = characters[chrIndex];

                    var statusCondition = currentFighter.GetCurrentCondition();

                    if (statusCondition != null)
                    {
                        statusCondition.Apply();

                        while (true)
                        {
                            string nextSCMessage = statusCondition.GetNextMessage();
                            if (nextSCMessage == null)
                            {
                                break;
                            }

                            LogPanel.Log(nextSCMessage);
                            yield return new WaitForSeconds(2f);
                        }

                        if (statusCondition.BlocksTurn())
                        {
                            combatStatus = CombatStatus.CHECK_FOR_VICTORY;
                            break;
                        }
                    }

                    if(currentFighter.isAlive == true)
                    {
                        LogPanel.Log("Es el turno de " + currentFighter.nameID + ".");
                        currentFighter.InitTurn();
                        combatStatus = CombatStatus.WAITING_FOR_FIGHTER;
                    }
                    else
                    {
                        combatStatus = CombatStatus.NEXT_TURN;
                    }
                    
                    break;
            }
        }
    }

    public Character[] FilterJustAlive(Character[] team)
    {
        returnBuffer.Clear();

        foreach (var chrs in team)
        {
            if (chrs.isAlive)
            {
                returnBuffer.Add(chrs);
            }
        }

        return returnBuffer.ToArray();
    }

    public Character[] GetOpposingTeam()
    {
        Character currentChr = characters[chrIndex];

        Character[] team = null;
        if (currentChr.team == Team.PLAYERS)
        {
            team = enemyTeam;
        }
        else if (currentChr.team == Team.ENEMIES)
        {
            team = playerTeam;
        }

        return FilterJustAlive(team);
    }

    public Character[] GetAllyTeam()
    {
        Character currentFighter = characters[chrIndex];

        Character[] team = null;
        if (currentFighter.team == Team.PLAYERS)
        {
            team = playerTeam;
        }
        else
        {
            team = enemyTeam;
        }

        return FilterJustAlive(team);
    }

    public void OnCharacterSkill(Skill skill)
    {
        currentChrSkill = skill;
        combatStatus = CombatStatus.FIGHTER_ACTION;
    }

    private void WriteInitialText()
    {
        if(playerTeam.Length >= 4)
        {
            initialText = "Equipo! ";
        }
        else
        {
            foreach (var chr in playerTeam)
            {
                initialText += chr.nameID + "! ";
            }
        }

        initialText += enemyTeam[0].nameID + " ataca! ";

        if (enemyTeam.Length >= 4)
        {
            initialText = initialText + "Varios enemigos se unen al combate! ";
        }
        else
        {
            for(int i = 1; i < enemyTeam.Length; i++)
            {
                initialText += enemyTeam[i].nameID + " se une al combate! ";
            }
        }

        initialWait = playerTeam.Length + enemyTeam.Length;
    }
}
