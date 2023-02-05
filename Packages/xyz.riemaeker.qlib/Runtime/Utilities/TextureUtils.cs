using Qlib.Assets;
using UnityEngine;

namespace Qlib.Utilities
{
	public static class TextureUtils
	{
		private static Palette _defaultPalette;

		public static Palette GlobalPalette
		{
			get
			{
				// TODO: add override option in settings
				if (_defaultPalette == null)
					_defaultPalette = CreateDefaultPalette();

				return _defaultPalette;
			}
		}

		public static Texture2D CreateTexture(int[] data, int width, int height, bool alphaChannel = false,
			Palette palette = null)
		{
			if (palette == null)
				palette = GlobalPalette;

			var colors = new Color[width * height];

			for (int i = 0; i < colors.Length; i++)
				colors[i] = palette.Colors[data[i]];

			var texture = new Texture2D(width, height, alphaChannel ? TextureFormat.RGBA32 : TextureFormat.RGB24, false);
			texture.filterMode = FilterMode.Point;
			texture.SetPixels(colors);
			texture.Apply(false);

			return texture;
		}

		public static int[] Palettize(Texture2D texture)
		{
			var texPixels = texture.GetPixels();
			var pixels = new int[texPixels.Length];

			for (int i = 0; i < texPixels.Length; i++)
				pixels[i] = GetClosestPaletteMatch(texPixels[i], GlobalPalette.Colors);

			return pixels;
		}

		/// <summary>
		/// Uses Pythagorean distance in 3D colour space to find the closest match to a given colour on
		/// a given colour palette, and returns the index on the palette at which that match was found.
		/// </summary>
		/// <param name="col">The colour to find the closest match to</param>
		/// <param name="colorPalette">The palette of available colours to match</param>
		/// <returns>The index on the palette of the colour that is the closest to the given colour.</returns>
		public static int GetClosestPaletteMatch(Color col, Color[] colorPalette)
		{
			int colorMatch = 0;
			float leastDistance = float.MaxValue;
			float red = col.r;
			float green = col.g;
			float blue = col.b;
			for (int i = 0; i < colorPalette.Length; ++i)
			{
				Color paletteColor = colorPalette[i];
				float redDistance = paletteColor.r - red;
				float greenDistance = paletteColor.g - green;
				float blueDistance = paletteColor.b - blue;
				float distance = (redDistance * redDistance) + (greenDistance * greenDistance) + (blueDistance * blueDistance);
				if (distance >= leastDistance)
					continue;
				colorMatch = i;
				leastDistance = distance;
				if (distance == 0)
					return i;
			}
			
			return colorMatch;
		}

		private static Palette CreateDefaultPalette()
		{
			var palette = ScriptableObject.CreateInstance<Palette>();

			palette.Colors = new Color[]
			{
				new (0.00000f, 0.00000f, 0.00000f, 1),
				new (0.05882f, 0.05882f, 0.05882f, 1),
				new (0.12157f, 0.12157f, 0.12157f, 1),
				new (0.18431f, 0.18431f, 0.18431f, 1),
				new (0.24706f, 0.24706f, 0.24706f, 1),
				new (0.29412f, 0.29412f, 0.29412f, 1),
				new (0.35686f, 0.35686f, 0.35686f, 1),
				new (0.41961f, 0.41961f, 0.41961f, 1),
				new (0.48235f, 0.48235f, 0.48235f, 1),
				new (0.54510f, 0.54510f, 0.54510f, 1),
				new (0.60784f, 0.60784f, 0.60784f, 1),
				new (0.67059f, 0.67059f, 0.67059f, 1),
				new (0.73333f, 0.73333f, 0.73333f, 1),
				new (0.79608f, 0.79608f, 0.79608f, 1),
				new (0.85882f, 0.85882f, 0.85882f, 1),
				new (0.92157f, 0.92157f, 0.92157f, 1),
				new (0.05882f, 0.04314f, 0.02745f, 1),
				new (0.09020f, 0.05882f, 0.04314f, 1),
				new (0.12157f, 0.09020f, 0.04314f, 1),
				new (0.15294f, 0.10588f, 0.05882f, 1),
				new (0.18431f, 0.13725f, 0.07451f, 1),
				new (0.21569f, 0.16863f, 0.09020f, 1),
				new (0.24706f, 0.18431f, 0.09020f, 1),
				new (0.29412f, 0.21569f, 0.10588f, 1),
				new (0.32549f, 0.23137f, 0.10588f, 1),
				new (0.35686f, 0.26275f, 0.12157f, 1),
				new (0.38824f, 0.29412f, 0.12157f, 1),
				new (0.41961f, 0.32549f, 0.12157f, 1),
				new (0.45098f, 0.34118f, 0.12157f, 1),
				new (0.48235f, 0.37255f, 0.13725f, 1),
				new (0.51373f, 0.40392f, 0.13725f, 1),
				new (0.56078f, 0.43529f, 0.13725f, 1),
				new (0.04314f, 0.04314f, 0.05882f, 1),
				new (0.07451f, 0.07451f, 0.10588f, 1),
				new (0.10588f, 0.10588f, 0.15294f, 1),
				new (0.15294f, 0.15294f, 0.20000f, 1),
				new (0.18431f, 0.18431f, 0.24706f, 1),
				new (0.21569f, 0.21569f, 0.29412f, 1),
				new (0.24706f, 0.24706f, 0.34118f, 1),
				new (0.27843f, 0.27843f, 0.40392f, 1),
				new (0.30980f, 0.30980f, 0.45098f, 1),
				new (0.35686f, 0.35686f, 0.49804f, 1),
				new (0.38824f, 0.38824f, 0.54510f, 1),
				new (0.41961f, 0.41961f, 0.59216f, 1),
				new (0.45098f, 0.45098f, 0.63922f, 1),
				new (0.48235f, 0.48235f, 0.68627f, 1),
				new (0.51373f, 0.51373f, 0.73333f, 1),
				new (0.54510f, 0.54510f, 0.79608f, 1),
				new (0.00000f, 0.00000f, 0.00000f, 1),
				new (0.02745f, 0.02745f, 0.00000f, 1),
				new (0.04314f, 0.04314f, 0.00000f, 1),
				new (0.07451f, 0.07451f, 0.00000f, 1),
				new (0.10588f, 0.10588f, 0.00000f, 1),
				new (0.13725f, 0.13725f, 0.00000f, 1),
				new (0.16863f, 0.16863f, 0.02745f, 1),
				new (0.18431f, 0.18431f, 0.02745f, 1),
				new (0.21569f, 0.21569f, 0.02745f, 1),
				new (0.24706f, 0.24706f, 0.02745f, 1),
				new (0.27843f, 0.27843f, 0.02745f, 1),
				new (0.29412f, 0.29412f, 0.04314f, 1),
				new (0.32549f, 0.32549f, 0.04314f, 1),
				new (0.35686f, 0.35686f, 0.04314f, 1),
				new (0.38824f, 0.38824f, 0.04314f, 1),
				new (0.41961f, 0.41961f, 0.05882f, 1),
				new (0.02745f, 0.00000f, 0.00000f, 1),
				new (0.05882f, 0.00000f, 0.00000f, 1),
				new (0.09020f, 0.00000f, 0.00000f, 1),
				new (0.12157f, 0.00000f, 0.00000f, 1),
				new (0.15294f, 0.00000f, 0.00000f, 1),
				new (0.18431f, 0.00000f, 0.00000f, 1),
				new (0.21569f, 0.00000f, 0.00000f, 1),
				new (0.24706f, 0.00000f, 0.00000f, 1),
				new (0.27843f, 0.00000f, 0.00000f, 1),
				new (0.30980f, 0.00000f, 0.00000f, 1),
				new (0.34118f, 0.00000f, 0.00000f, 1),
				new (0.37255f, 0.00000f, 0.00000f, 1),
				new (0.40392f, 0.00000f, 0.00000f, 1),
				new (0.43529f, 0.00000f, 0.00000f, 1),
				new (0.46667f, 0.00000f, 0.00000f, 1),
				new (0.49804f, 0.00000f, 0.00000f, 1),
				new (0.07451f, 0.07451f, 0.00000f, 1),
				new (0.10588f, 0.10588f, 0.00000f, 1),
				new (0.13725f, 0.13725f, 0.00000f, 1),
				new (0.18431f, 0.16863f, 0.00000f, 1),
				new (0.21569f, 0.18431f, 0.00000f, 1),
				new (0.26275f, 0.21569f, 0.00000f, 1),
				new (0.29412f, 0.23137f, 0.02745f, 1),
				new (0.34118f, 0.26275f, 0.02745f, 1),
				new (0.37255f, 0.27843f, 0.02745f, 1),
				new (0.41961f, 0.29412f, 0.04314f, 1),
				new (0.46667f, 0.32549f, 0.05882f, 1),
				new (0.51373f, 0.34118f, 0.07451f, 1),
				new (0.54510f, 0.35686f, 0.07451f, 1),
				new (0.59216f, 0.37255f, 0.10588f, 1),
				new (0.63922f, 0.38824f, 0.12157f, 1),
				new (0.68627f, 0.40392f, 0.13725f, 1),
				new (0.13725f, 0.07451f, 0.02745f, 1),
				new (0.18431f, 0.09020f, 0.04314f, 1),
				new (0.23137f, 0.12157f, 0.05882f, 1),
				new (0.29412f, 0.13725f, 0.07451f, 1),
				new (0.34118f, 0.16863f, 0.09020f, 1),
				new (0.38824f, 0.18431f, 0.12157f, 1),
				new (0.45098f, 0.21569f, 0.13725f, 1),
				new (0.49804f, 0.23137f, 0.16863f, 1),
				new (0.56078f, 0.26275f, 0.20000f, 1),
				new (0.62353f, 0.30980f, 0.20000f, 1),
				new (0.68627f, 0.38824f, 0.18431f, 1),
				new (0.74902f, 0.46667f, 0.18431f, 1),
				new (0.81176f, 0.56078f, 0.16863f, 1),
				new (0.87451f, 0.67059f, 0.15294f, 1),
				new (0.93725f, 0.79608f, 0.12157f, 1),
				new (1.00000f, 0.95294f, 0.10588f, 1),
				new (0.04314f, 0.02745f, 0.00000f, 1),
				new (0.10588f, 0.07451f, 0.00000f, 1),
				new (0.16863f, 0.13725f, 0.05882f, 1),
				new (0.21569f, 0.16863f, 0.07451f, 1),
				new (0.27843f, 0.20000f, 0.10588f, 1),
				new (0.32549f, 0.21569f, 0.13725f, 1),
				new (0.38824f, 0.24706f, 0.16863f, 1),
				new (0.43529f, 0.27843f, 0.20000f, 1),
				new (0.49804f, 0.32549f, 0.24706f, 1),
				new (0.54510f, 0.37255f, 0.27843f, 1),
				new (0.60784f, 0.41961f, 0.32549f, 1),
				new (0.65490f, 0.48235f, 0.37255f, 1),
				new (0.71765f, 0.52941f, 0.41961f, 1),
				new (0.76471f, 0.57647f, 0.48235f, 1),
				new (0.82745f, 0.63922f, 0.54510f, 1),
				new (0.89020f, 0.70196f, 0.59216f, 1),
				new (0.67059f, 0.54510f, 0.63922f, 1),
				new (0.62353f, 0.49804f, 0.59216f, 1),
				new (0.57647f, 0.45098f, 0.52941f, 1),
				new (0.54510f, 0.40392f, 0.48235f, 1),
				new (0.49804f, 0.35686f, 0.43529f, 1),
				new (0.46667f, 0.32549f, 0.38824f, 1),
				new (0.41961f, 0.29412f, 0.34118f, 1),
				new (0.37255f, 0.24706f, 0.29412f, 1),
				new (0.34118f, 0.21569f, 0.26275f, 1),
				new (0.29412f, 0.18431f, 0.21569f, 1),
				new (0.26275f, 0.15294f, 0.18431f, 1),
				new (0.21569f, 0.12157f, 0.13725f, 1),
				new (0.16863f, 0.09020f, 0.10588f, 1),
				new (0.13725f, 0.07451f, 0.07451f, 1),
				new (0.09020f, 0.04314f, 0.04314f, 1),
				new (0.05882f, 0.02745f, 0.02745f, 1),
				new (0.73333f, 0.45098f, 0.62353f, 1),
				new (0.68627f, 0.41961f, 0.56078f, 1),
				new (0.63922f, 0.37255f, 0.51373f, 1),
				new (0.59216f, 0.34118f, 0.46667f, 1),
				new (0.54510f, 0.30980f, 0.41961f, 1),
				new (0.49804f, 0.29412f, 0.37255f, 1),
				new (0.45098f, 0.26275f, 0.32549f, 1),
				new (0.41961f, 0.23137f, 0.29412f, 1),
				new (0.37255f, 0.20000f, 0.24706f, 1),
				new (0.32549f, 0.16863f, 0.21569f, 1),
				new (0.27843f, 0.13725f, 0.16863f, 1),
				new (0.23137f, 0.12157f, 0.13725f, 1),
				new (0.18431f, 0.09020f, 0.10588f, 1),
				new (0.13725f, 0.07451f, 0.07451f, 1),
				new (0.09020f, 0.04314f, 0.04314f, 1),
				new (0.05882f, 0.02745f, 0.02745f, 1),
				new (0.85882f, 0.76471f, 0.73333f, 1),
				new (0.79608f, 0.70196f, 0.65490f, 1),
				new (0.74902f, 0.63922f, 0.60784f, 1),
				new (0.68627f, 0.59216f, 0.54510f, 1),
				new (0.63922f, 0.52941f, 0.48235f, 1),
				new (0.59216f, 0.48235f, 0.43529f, 1),
				new (0.52941f, 0.43529f, 0.37255f, 1),
				new (0.48235f, 0.38824f, 0.32549f, 1),
				new (0.41961f, 0.34118f, 0.27843f, 1),
				new (0.37255f, 0.29412f, 0.23137f, 1),
				new (0.32549f, 0.24706f, 0.20000f, 1),
				new (0.26275f, 0.20000f, 0.15294f, 1),
				new (0.21569f, 0.16863f, 0.12157f, 1),
				new (0.15294f, 0.12157f, 0.09020f, 1),
				new (0.10588f, 0.07451f, 0.05882f, 1),
				new (0.05882f, 0.04314f, 0.02745f, 1),
				new (0.43529f, 0.51373f, 0.48235f, 1),
				new (0.40392f, 0.48235f, 0.43529f, 1),
				new (0.37255f, 0.45098f, 0.40392f, 1),
				new (0.34118f, 0.41961f, 0.37255f, 1),
				new (0.30980f, 0.38824f, 0.34118f, 1),
				new (0.27843f, 0.35686f, 0.30980f, 1),
				new (0.24706f, 0.32549f, 0.27843f, 1),
				new (0.21569f, 0.29412f, 0.24706f, 1),
				new (0.18431f, 0.26275f, 0.21569f, 1),
				new (0.16863f, 0.23137f, 0.18431f, 1),
				new (0.13725f, 0.20000f, 0.15294f, 1),
				new (0.12157f, 0.16863f, 0.12157f, 1),
				new (0.09020f, 0.13725f, 0.09020f, 1),
				new (0.05882f, 0.10588f, 0.07451f, 1),
				new (0.04314f, 0.07451f, 0.04314f, 1),
				new (0.02745f, 0.04314f, 0.02745f, 1),
				new (1.00000f, 0.95294f, 0.10588f, 1),
				new (0.93725f, 0.87451f, 0.09020f, 1),
				new (0.85882f, 0.79608f, 0.07451f, 1),
				new (0.79608f, 0.71765f, 0.05882f, 1),
				new (0.73333f, 0.65490f, 0.05882f, 1),
				new (0.67059f, 0.59216f, 0.04314f, 1),
				new (0.60784f, 0.51373f, 0.02745f, 1),
				new (0.54510f, 0.45098f, 0.02745f, 1),
				new (0.48235f, 0.38824f, 0.02745f, 1),
				new (0.41961f, 0.32549f, 0.00000f, 1),
				new (0.35686f, 0.27843f, 0.00000f, 1),
				new (0.29412f, 0.21569f, 0.00000f, 1),
				new (0.23137f, 0.16863f, 0.00000f, 1),
				new (0.16863f, 0.12157f, 0.00000f, 1),
				new (0.10588f, 0.05882f, 0.00000f, 1),
				new (0.04314f, 0.02745f, 0.00000f, 1),
				new (0.00000f, 0.00000f, 1.00000f, 1),
				new (0.04314f, 0.04314f, 0.93725f, 1),
				new (0.07451f, 0.07451f, 0.87451f, 1),
				new (0.10588f, 0.10588f, 0.81176f, 1),
				new (0.13725f, 0.13725f, 0.74902f, 1),
				new (0.16863f, 0.16863f, 0.68627f, 1),
				new (0.18431f, 0.18431f, 0.62353f, 1),
				new (0.18431f, 0.18431f, 0.56078f, 1),
				new (0.18431f, 0.18431f, 0.49804f, 1),
				new (0.18431f, 0.18431f, 0.43529f, 1),
				new (0.18431f, 0.18431f, 0.37255f, 1),
				new (0.16863f, 0.16863f, 0.30980f, 1),
				new (0.13725f, 0.13725f, 0.24706f, 1),
				new (0.10588f, 0.10588f, 0.18431f, 1),
				new (0.07451f, 0.07451f, 0.12157f, 1),
				new (0.04314f, 0.04314f, 0.05882f, 1),
				new (0.16863f, 0.00000f, 0.00000f, 1),
				new (0.23137f, 0.00000f, 0.00000f, 1),
				new (0.29412f, 0.02745f, 0.00000f, 1),
				new (0.37255f, 0.02745f, 0.00000f, 1),
				new (0.43529f, 0.05882f, 0.00000f, 1),
				new (0.49804f, 0.09020f, 0.02745f, 1),
				new (0.57647f, 0.12157f, 0.02745f, 1),
				new (0.63922f, 0.15294f, 0.04314f, 1),
				new (0.71765f, 0.20000f, 0.05882f, 1),
				new (0.76471f, 0.29412f, 0.10588f, 1),
				new (0.81176f, 0.38824f, 0.16863f, 1),
				new (0.85882f, 0.49804f, 0.23137f, 1),
				new (0.89020f, 0.59216f, 0.30980f, 1),
				new (0.90588f, 0.67059f, 0.37255f, 1),
				new (0.93725f, 0.74902f, 0.46667f, 1),
				new (0.96863f, 0.82745f, 0.54510f, 1),
				new (0.65490f, 0.48235f, 0.23137f, 1),
				new (0.71765f, 0.60784f, 0.21569f, 1),
				new (0.78039f, 0.76471f, 0.21569f, 1),
				new (0.90588f, 0.89020f, 0.34118f, 1),
				new (0.49804f, 0.74902f, 1.00000f, 1),
				new (0.67059f, 0.90588f, 1.00000f, 1),
				new (0.84314f, 1.00000f, 1.00000f, 1),
				new (0.40392f, 0.00000f, 0.00000f, 1),
				new (0.54510f, 0.00000f, 0.00000f, 1),
				new (0.70196f, 0.00000f, 0.00000f, 1),
				new (0.84314f, 0.00000f, 0.00000f, 1),
				new (1.00000f, 0.00000f, 0.00000f, 1),
				new (1.00000f, 0.95294f, 0.57647f, 1),
				new (1.00000f, 0.96863f, 0.78039f, 1),
				new (1.00000f, 1.00000f, 1.00000f, 1),
				new (0.62353f, 0.35686f, 0.32549f, 1)
			};

			return palette;
		}
	}
}