using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL.Graphics
{
	public struct Matrix
	{
		public float Row1Col1 { get; private set; }
		public float Row1Col2 { get; private set; }
		public float Row2Col1 { get; private set; }
		public float Row2Col2 { get; private set; }

		public Matrix(float row1col1, float row1col2, float row2col1, float row2col2)
			: this()
		{
			Row1Col1 = row1col1;
			Row1Col2 = row1col2;
			Row2Col1 = row2col1;
			Row2Col2 = row2col2;
		}

		public float this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return Row1Col1;
					case 1: return Row1Col2;
					case 2: return Row2Col1;
					case 3: return Row2Col2;
					default: throw new ArgumentOutOfRangeException();
				}
			}

			set
			{
				switch (index)
				{
					case 0: Row1Col1 = value; break;
					case 1: Row1Col2 = value; break;
					case 2: Row2Col1 = value; break;
					case 3: Row2Col2 = value; break;
					default: throw new ArgumentOutOfRangeException();
				}
			}
		}

		public float this[int row, int column]
		{
			get { return this[row * 2 + column]; }
			set { this[row * 2 + column] = value; }
		}

		/// <summary>
		/// Calculates the inverse matrix of the passed 2D matrix.
		/// </summary>
		/// <param name="matrix"></param>
		/// <returns></returns>
		public static Matrix Invert(Matrix matrix)
		{
			// [a b]  ==>  [d -b]
			// [c d]  ==>  [-c a]
			// determinant = 1 / (a * d - b * c)

			float determinant = 1 / ((matrix.Row1Col1 * matrix.Row2Col2) - (matrix.Row1Col2 * matrix.Row2Col1));

			float newRow1Col1 = matrix.Row2Col2;
			float newRow1Col2 = matrix.Row1Col2 * -1;
			float newRow2Col1 = matrix.Row2Col1 * -1;
			float newRow2Col2 = matrix.Row1Col1;

			newRow1Col1 /= determinant;
			newRow1Col2 /= determinant;
			newRow2Col1 /= determinant;
			newRow2Col2 /= determinant;

			return new Matrix(newRow1Col1, newRow1Col2, newRow2Col1, newRow2Col2);
		}

		public static Matrix CreateScale(float scaleX, float scaleY)
		{
			return new Matrix()
			{
				Row1Col1 = scaleX,
				Row1Col2 = 0,
				Row2Col1 = 0,
				Row2Col2 = scaleY
			};
		}
	}
}
