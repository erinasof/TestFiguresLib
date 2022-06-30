using System;
using System.Collections.Generic;
using System.Text;

namespace FiguresLib
{	
	public interface IFigure
	{		
		public double GetSquare();
	}

	// перспективы для развития: Square высчитывается не каждый раз при вызове GetSquere,
	// а один раз во время первого вызова и сбрасывается, если фигура изменена
	public abstract class Figure : IFigure
	{
		private bool _isBlocked = true;
		protected bool IsBlocked
		{
			get { return _isBlocked; }
			private set
			{
				_isBlocked = value;
				if (IsBlocked == false)
				{					
					CheckLegal();
				}
			}
		}		

		public double GetSquare()
		{
			if (IsBlocked == true)
			{
				throw new BlockException("Figure is blocked!");
			}
			
			return GeometryHelper.GetSquare(this);						
		}

		protected abstract void CheckLegal();		
		
		public void Block()
        {
			IsBlocked = true;
        }

		public void Unblock()
		{
			IsBlocked = false;
		}
	}
}
