using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using System.IO;
using System.Globalization;

namespace CameraCalibrator.Support
{
	public class HomographyMatrixSupport
	{
		public static void Save(HomographyMatrix homographyMatrix, string filePath)
		{
			using (TextWriter writer = new StreamWriter(filePath))
			{
				for (int x = 0; x < 3; x++)
				{
					for (int y = 0; y < 3; y++)
					{
						writer.WriteLine(homographyMatrix[x, y].ToString(CultureInfo.InvariantCulture));
					}
				}
			}
		}

		public static HomographyMatrix Load(string filePath)
		{
			HomographyMatrix homographyMatrix = new HomographyMatrix();
			using (TextReader reader = new StreamReader(filePath))
			{
				for (int x = 0; x < 3; x++)
				{
					for (int y = 0; y < 3; y++)
					{
						homographyMatrix[x, y] = GetNextValue(reader);
					}
				}
			}

			return homographyMatrix;
		}

		private static double GetNextValue(TextReader reader)
		{
			string line;
			while ((line = reader.ReadLine()) != null)
			{
				line = line.Trim();
				if (line.Length == 0 || line.StartsWith("#"))
				{
					continue;
				}

				double value = double.Parse(line, CultureInfo.InvariantCulture);
				return value;
			}

			throw new EndOfStreamException("Unexpected end of file");
		}
	}
}
