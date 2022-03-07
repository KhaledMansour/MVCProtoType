using System;

public class CellModel
{
	public int cellValue { get; private set; }
	public event Action<bool> OnCellSelectedChanged;
	public bool cellSelected
	{
		get
		{
			return _cellSelected;
		}
		set
		{
			_cellSelected = value;
			OnCellSelectedChanged?.Invoke (value);
		}
	}
	private bool _cellSelected;
	public CellModel(bool isSelected, int cellValue)
	{
		cellSelected = isSelected;
		this.cellValue = cellValue;
	}
}

