using SkiaSharp;

namespace Aria_Net.Services {
	public class CaptchaService {
		public byte[] GenerateCaptcha(string text) {
			int width = 200, height = 60;

			using var surface = SKSurface.Create(new SKImageInfo(width, height));
			var canvas = surface.Canvas;
			canvas.Clear(SKColors.White);

			// Skew the text
			var paint = new SKPaint {
				Color = SKColors.Black,
				TextSize = 40,
				IsAntialias = true,
				Typeface = SKTypeface.Default
			};

			// Add random rotation to the canvas for text skewing
			canvas.RotateDegrees(new Random().Next(-10, 10), width / 2, height / 2);

			// Draw the captcha text
			canvas.DrawText(text, 20, 40, paint);

			// Add noise (dots)
			for (int i = 0; i < 100; i++) {
				var dotPaint = new SKPaint { Color = SKColors.Gray, IsAntialias = true };
				float x = new Random().Next(0, width);
				float y = new Random().Next(0, height);
				canvas.DrawCircle(x, y, 2, dotPaint);
			}

			// Add random lines for more noise
			for (int i = 0; i < 5; i++) {
				var linePaint = new SKPaint { Color = SKColors.Gray, StrokeWidth = 1 };
				float x1 = new Random().Next(0, width);
				float y1 = new Random().Next(0, height);
				float x2 = new Random().Next(0, width);
				float y2 = new Random().Next(0, height);
				canvas.DrawLine(x1, y1, x2, y2, linePaint);
			}

			using var image = surface.Snapshot();
			using var data = image.Encode(SKEncodedImageFormat.Png, 100);

			return data.ToArray();
		}
	}
}
