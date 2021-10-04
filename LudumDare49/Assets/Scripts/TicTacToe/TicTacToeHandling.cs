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

    [SerializeField]
    private GameObject[] Boards = new GameObject[5];

    [SerializeField]
    private GameObject UIBoard;


    private int CurrentTurn;
    private BoardV2 gamingBoard;

    void Start()
    {
        gamingBoard = new BoardV2();

        CurrentTurn = 0;
    }

    public void PlayerPressedButton(int PressedButtonFieldIndex)
    {
        if (gamingBoard.getTurn()== BoardV2.State.X && gamingBoard.move(PressedButtonFieldIndex))
        {
            PlayingBoardFields[PressedButtonFieldIndex].enabled = false;
            PlayingBoardFields[PressedButtonFieldIndex].transform.GetChild(0).GetComponent<Image>().sprite = X_Icon;
            PlayingBoardFields[PressedButtonFieldIndex].transform.GetChild(0).GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);


            //DoAITurn();
            //CheckGameOver();

            StartCoroutine(DeactivateAndActivateBoard(1000));
            StartCoroutine(AITurn(2000));
            
        }
    }

    private IEnumerator DeactivateAndActivateBoard(int WaitForMilliseconds)
	{
        yield return new WaitForSeconds(WaitForMilliseconds / 1000);
        Boards[CurrentTurn].SetActive(false);
        UIBoard.SetActive(false);
        CurrentTurn++;
        if(CurrentTurn<=4)
		{
            Boards[CurrentTurn].SetActive(true);
		}
    }

    private IEnumerator AITurn(int WaitForMilliseconds)
	{
        yield return new WaitForSeconds(WaitForMilliseconds / 1000);
        DoAITurn();
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
