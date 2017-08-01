using System;
using CoreGraphics;
using UIKit;

namespace GurruPCL.iOS
{
	public class ColorHelper
	{
		public static UIColor UIColorFromHex(int hexColor)
		{
			return UIColor.FromRGB(
				((((hexColor & 0xFF0000) >> 16)) / 255.0f),
				((((hexColor & 0xFF00) >> 8)) / 255.0f),
				(((hexColor & 0xFF)) / 255.0f)
			);
		}

		public static CGColor CGColorFromHex(int hexColor)
		{
			return new CGColor(
				((((hexColor & 0xFF0000) >> 16)) / 255.0f),
				((((hexColor & 0xFF00) >> 8)) / 255.0f),
				(((hexColor & 0xFF)) / 255.0f)
			);
		}
	}
}
