using System;



namespace DvtkHighLevelInterface.Messages
{
	/// <summary>
	/// Collection of DulMessages.
	/// </summary>
	public sealed class DulMessageCollection : DvtkData.Collections.NullSafeCollectionBase
	{
		//
		// - Constructors -
		//

		/// <summary>
		/// Default constructor.
		/// </summary>
		public DulMessageCollection()
		{
			// Do nothing.
		}

		/// <summary>
		/// Constructor with initialization. Shallow copy.
		/// </summary>
		/// <param name="arrayOfValues">values to copy.</param>
		public DulMessageCollection(DulMessage[] arrayOfValues)
		{
			if (arrayOfValues == null)
			{
				throw new ArgumentNullException();
			}

			foreach (DulMessage value in arrayOfValues)
			{
				this.Add(value);
			}
		}



		//
		// - Properties -
		//

		/// <summary>
		/// Gets or sets the item at the specified index.
		/// </summary>
		/// <value>The item at the specified <c>index</c>.</value>
		public new DulMessage this[int index]
		{
			get 
			{
				return (DulMessage)base[index];
			}
			set
			{
				base.Insert(index, value);
			}
		}



		//
		// - Methods -
		//

		/// <summary>
		/// Adds an item to the <see cref="System.Collections.IList"/>.
		/// </summary>
		/// <param name="value">The item to add to the <see cref="System.Collections.IList"/>. </param>
		/// <returns>The position into which the new element was inserted.</returns>
		public int Add(DulMessage value)
		{
			return base.Add(value);
		}

		/// <summary>
		/// Determines whether the <see cref="System.Collections.IList"/> contains a specific item.
		/// </summary>
		/// <param name="value">The item to locate in the <see cref="System.Collections.IList"/>.</param>
		/// <returns><see langword="true"/> if the item is found in the <see cref="System.Collections.IList"/>; otherwise, <see langword="false"/>.</returns>
		public bool Contains(DulMessage value)
		{
			return base.Contains(value);
		}

		/// <summary>
		/// Determines the index of a specific item in the <see cref="System.Collections.IList"/>.
		/// </summary>
		/// <param name="value">The item to locate in the <see cref="System.Collections.IList"/>.</param>
		/// <returns>The index of <c>value</c> if found in the list; otherwise, -1.</returns>
		public int IndexOf(DulMessage value)
		{
			return base.IndexOf(value);
		}

		/// <summary>
		/// Inserts an item to the IList at the specified position.
		/// </summary>
		/// <param name="index">The zero-based index at which <c>value</c> should be inserted. </param>
		/// <param name="value">The item to insert into the <see cref="System.Collections.IList"/>.</param>
		public void Insert(int index, DulMessage value)
		{
			base.Insert(index, value);
		}

		/// <summary>
		/// Removes the first occurrence of a specific item from the IList.
		/// </summary>
		/// <param name="value">The item to remove from the <see cref="System.Collections.IList"/>.</param>
		public void Remove(DulMessage value)
		{
			base.Remove(value);
		}
	}
}
