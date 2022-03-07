using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[System.Serializable]
public class GameProperties
{
	// when you change these props you can generate any level with any width and height and also you can change game rules for each level
	public int gridWidthCount;
	public int gridHeightCount;
	public int ruleMatchingWinNum; // 21
	public int maxScore; // 60
	public int minCellValue; // 1
	public int maxCellValue; // 11
}

public class GameController : MonoBehaviour
{
	[SerializeField]
	GameProperties gameProperties;
	[SerializeField]
	private CellView cellViewObject;
	[SerializeField]
	private RectTransform gridTransform;
	[SerializeField]
	private Text scoreTxt;
	[SerializeField]
	private WinScreen winScreen;

	private List<CellController> selectedCells;
	private GridLayoutGroup gridLayout;
	public CellController[,] grid;

	private int score;

	public void OnCellSelected(CellController cell)
	{
		selectedCells.Add (cell);
		var currentScore = selectedCells.Sum (x => x.cellModel.cellValue);
		if (currentScore >= gameProperties.ruleMatchingWinNum)
		{
			if (currentScore == gameProperties.ruleMatchingWinNum)
			{
				score += gameProperties.ruleMatchingWinNum;
				CheckWinState ();
			}
			else if (currentScore > gameProperties.ruleMatchingWinNum)
			{
				score -= currentScore / 2;
			}
			selectedCells.ForEach (x => x.OnCellDisabled ());
			selectedCells.Clear ();
		}
		scoreTxt.text = "Score:" + score;
	}
	public void OnCellDeselected(CellController cell)
	{
		selectedCells.Remove (cell);
	}
	void Start()
	{
		selectedCells = new List<CellController> ();
		gridLayout = gridTransform.GetComponent<GridLayoutGroup> ();
		InitGrid ();
	}
	private void InitGrid()
	{

		var cellSizeX = gridTransform.rect.width / gameProperties.gridWidthCount;
		var cellSizeY = gridTransform.rect.height / gameProperties.gridHeightCount;
		gridLayout.cellSize = new Vector2 (cellSizeX - 10, cellSizeY - 10);
		gridLayout.spacing = new Vector2 (10, 10);
		grid = new CellController[gameProperties.gridWidthCount, gameProperties.gridHeightCount];
		for (int i = 0; i < gameProperties.gridWidthCount; i++)
		{
			for (int j = 0; j < gameProperties.gridHeightCount; j++)
			{
				var cellValue = Random.Range (gameProperties.minCellValue, gameProperties.maxCellValue + 1);
				var cellModel = new CellModel (false, cellValue);
				var cellView = Instantiate (cellViewObject, gridTransform);
				var cellController = new CellController (cellView, cellModel);
				cellController.OnCellSelected += OnCellSelected;
				cellController.OnCellDeselected += OnCellDeselected;
				grid[i, j] = cellController;
			}
		}
	}
	private void CheckWinState()
	{
		if (score >= gameProperties.maxScore)
		{
			var gameData = new EndGameData ()
			{
				score = this.score,
				isWin = true
			};
			winScreen.Init (gameData, OnRestartLevel);
		}
	}
	private void OnRestartLevel()
	{
		SceneManager.LoadScene (0);
	}
}
