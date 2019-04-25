using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
 * A generic class for handling displaying data in a list.  Requires at least extending AdapterView and ViewHolder.
 */
public class AdapterView<T, VH>: MonoBehaviour where VH: ViewHolder<T>
{
	// The prototype view holder prefab.
	public VH viewHolderPrefab;
	
	// The container to hold all the view holders.
	public Transform container;

	private List<VH> _viewHolders = new List<VH>();

	/**
	 * Create a new instance of the view holder type.
	 */
	protected virtual VH CreateViewHolder()
	{
		VH viewHolder = Object.Instantiate(viewHolderPrefab, container);
		_viewHolders.Add(viewHolder);
		return viewHolder;
	}
	
	/**
	 * Bind the view holder with a piece of data.
	 */
	protected virtual void BindViewHolder(VH viewHolder, T data)
	{
		viewHolder.BindData(data);
	}

	/**
	 * Bind a list of data into the container as view holder instances.
	 */
	public void BindDataList(List<T> dataList)
	{
		// Rebind all existing view holders.
		for (int i = 0; i < _viewHolders.Count; i++)
		{
			_viewHolders[i].BindData(dataList[i]);
		}
		
		// Create and bind new view holders as needed.
		for (int i = _viewHolders.Count; i < dataList.Count(); i++)
		{
			BindViewHolder(CreateViewHolder(), dataList[i]);
		}
		
		// Destroy extra unused view holders.
		for (int i = dataList.Count; i < _viewHolders.Count; i++)
		{
			Destroy(_viewHolders[i].gameObject);
		}
		_viewHolders.RemoveRange(dataList.Count, _viewHolders.Count - dataList.Count);
	}
}

public abstract class ViewHolder<T> : MonoBehaviour
{
	public abstract void BindData(T data);
}