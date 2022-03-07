using System;
using UnityEngine;
[System.Serializable]
public class CellController
{
	public CellView cellView { get; private set; }
	public CellModel cellModel { get; private set; }
	public event Action<CellController> OnCellSelected;
	public event Action<CellController> OnCellDeselected;
	public CellController(CellView cellView, CellModel cellModel)
	{
		this.cellView = cellView;
		this.cellModel = cellModel;
		this.cellView.Init (cellModel.cellValue, OnClickOnCell);
		this.cellModel.OnCellSelectedChanged += OnCellSelectedChanged;
		//this.cellView.
	}
	public void OnClickOnCell()
	{
		cellModel.cellSelected = !cellModel.cellSelected;
	}
	public void OnCellSelectedChanged(bool isSelected)
	{
		cellView.OnCellSelctedChanged (isSelected);
		if (isSelected)
		{
			OnCellSelected (this);
		}
		else
		{
			OnCellDeselected (this);
		}
	}
	public void OnCellDisabled()
	{
		cellView.DisableCellBtn ();
	}
	public bool IsCellActive()
	{
		return cellView.IsCellInteractable;
	}

}
