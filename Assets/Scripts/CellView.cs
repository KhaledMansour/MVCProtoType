using UnityEngine;
using UnityEngine.UI;
using System;

public class CellView : MonoBehaviour
{
	[SerializeField]
	private Text cellValue;
	[SerializeField]
	private Button cellButton;
	[SerializeField]
	private Image cellSelectedImage;
	public bool IsCellInteractable { get {return cellButton.IsInteractable (); } }
	public void Init (int cellValue, Action onCellClicked)
	{
		this.cellValue.text = cellValue + "";
		cellButton.onClick.AddListener (() => { onCellClicked (); });
	}
	public void OnCellSelctedChanged(bool isSelected)
	{
		cellSelectedImage.enabled = isSelected;
	}
	public void DisableCellBtn()
	{
		cellButton.interactable = false;
	}
}
