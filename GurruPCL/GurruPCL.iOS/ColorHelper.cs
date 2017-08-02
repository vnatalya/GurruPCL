using System;
using CoreGraphics;
using UIKit;

namespace GurruPCL.iOS
{
	public class ColorHelper
	{
		public static UIColor UIColorFromHex(int hexColor, int alpha = 255)
		{
			return UIColor.FromRGBA(
				((((hexColor & 0xFF0000) >> 16)) / 255.0f),
				((((hexColor & 0xFF00) >> 8)) / 255.0f),
				(((hexColor & 0xFF)) / 255.0f),
				alpha
			);
		}

		public static CGColor CGColorFromHex(int hexColor, int alpha = 255)
		{
			return new CGColor(
				((((hexColor & 0xFF0000) >> 16)) / 255.0f),
				((((hexColor & 0xFF00) >> 8)) / 255.0f),
				(((hexColor & 0xFF)) / 255.0f),
				alpha
			);
		}
	}
}
