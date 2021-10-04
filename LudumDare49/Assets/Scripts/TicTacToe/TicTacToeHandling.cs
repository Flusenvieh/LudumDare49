using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicTacToeHandling : MonoBehaviour
{
    [SerializeField]
    private Sprite X_Icon;

    [SerializeField]
    private Sprite O_Icon;

    [SerializeField]
    private Button[] PlayingBoardFields = new Button[9];

    private BoardV2 gamingBoard;

    void Start()
    {
        gamingBoard = new BoardV2();
    }

    public void PlayerPressedButton(int PressedButtonFieldIndex)
    {
        if (gamingBoard.getTurn()== BoardV2.State.X && gamingBoard.move(PressedButtonFieldIndex))
        {
            PlayingBoardFields[PressedButtonFieldIndex].enabled = false;
            PlayingBoardFields[PressedButtonFieldIndex].transform.GetChild(0).GetComponent<Image>().sprite = X_Icon;
            PlayingBoardFields[PressedButtonFieldIndex].transform.GetChild(0).GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

            
            //DoAITurn();
            CheckGameOver();
        }
    }

    public void DoAITurn()
    {
        TicTacToeAI.Run(gamingBoard.getTurn(), gamingBoard, double.MaxValue);

        List<int> EnemyIndices = gamingBoard.GetEnemyIndices();

        foreach (int Index in EnemyIndices)
        {
            if (!PlayingBoardFields[Index].enabled)
            {
                continue;
            }
            else
            {
                PlayingBoardFields[Index].enabled = false;
                PlayingBoardFields[Index].transform.GetChild(0).GetComponent<Image>().sprite = O_Icon;
                PlayingBoardFields[Index].transform.GetChild(0).GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
        }
    }

    public void CheckGameOver()
    {
        if (gamingBoard.isGameOver())
        {
            ResetGame();
        }
    }

    private void ResetGame()
    {
        ResetButtons();
        CreateNewBoard();
    }

    private void ResetButtons()
    {
        foreach (Button button in PlayingBoardFields)
        {
            button.enabled = true;
            button.transform.GetChild(0).GetComponent<Image>().sprite = null;
            button.transform.GetChild(0).GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
    }

    private void CreateNewBoard()
    {
        gamingBoard = new BoardV2();
    }
}
