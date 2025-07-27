﻿using System.Windows;
using System.Windows.Interop;

namespace InkCanvasForClass_Remastered.Helpers
{
    internal class IsOutsideOfScreenHelper
    {
        public static bool IsOutsideOfScreen(FrameworkElement target)
        {
            var hwndSource = (HwndSource)PresentationSource.FromVisual(target);
            if (hwndSource is null)
            {
                return true;
            }

            var hWnd = hwndSource.Handle;
            var targetBounds = GetPixelBoundsToScreen(target);

            var screens = System.Windows.Forms.Screen.AllScreens;
            return !screens.Any(x => x.Bounds.IntersectsWith(targetBounds));

            System.Drawing.Rectangle GetPixelBoundsToScreen(FrameworkElement visual)
            {
                var pixelBoundsToScreen = Rect.Empty;
                pixelBoundsToScreen.Union(visual.PointToScreen(new Point(0, 0)));
                pixelBoundsToScreen.Union(visual.PointToScreen(new Point(visual.ActualWidth, 0)));
                pixelBoundsToScreen.Union(visual.PointToScreen(new Point(0, visual.ActualHeight)));
                pixelBoundsToScreen.Union(visual.PointToScreen(new Point(visual.ActualWidth, visual.ActualHeight)));
                return new System.Drawing.Rectangle(
                    (int)pixelBoundsToScreen.X, (int)pixelBoundsToScreen.Y,
                    (int)pixelBoundsToScreen.Width, (int)pixelBoundsToScreen.Height);
            }
        }
    }
}