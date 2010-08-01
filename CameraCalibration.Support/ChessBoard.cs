using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV.Structure;
using System.Drawing;

namespace CameraCalibrator.Support
{
	public class ChessBoard
	{
		public int XCount { get; private set; }
		public int YCount { get; private set; }
		public int CornerCount { get; private set; }
		public Size PatternSize { get; private set; }
		public MCvPoint3D32f[] CornerPoints { get; private set; }

		public ChessBoard(int xCount, int yCount)
		{
			this.XCount = xCount;
			this.YCount = yCount;

			this.PatternSize = new Size(this.XCount - 1, this.YCount - 1);
			this.CornerCount = this.PatternSize.Width * this.PatternSize.Height;

			List<MCvPoint3D32f> cornerPoints = new List<MCvPoint3D32f>(this.CornerCount);
			for (int x = 0; x < this.XCount - 1; x++)
			{
				for (int y = 0; y < this.YCount - 1; y++)
				{
					MCvPoint3D32f cornerPoint = new MCvPoint3D32f(x * 25, y * 25, 0);
					cornerPoints.Add(cornerPoint);
				}
			}
			this.CornerPoints = cornerPoints.ToArray();
		}
	}
}
