using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;

namespace Playground
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				IntrinsicCameraParameters intrinsicCameraParameters = new IntrinsicCameraParameters();
				// We initialize the intrinsic matrix such that the two focal lengths have a ratio of 1.0
				intrinsicCameraParameters.IntrinsicMatrix[0, 0] = 1.0;
				intrinsicCameraParameters.IntrinsicMatrix[1, 1] = 1.0;

				intrinsicCameraParameters.IntrinsicMatrix.Save("IntrinsixMatrix.xml");

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
			finally
			{
				Console.ReadLine();
			}
		}
	}
}
