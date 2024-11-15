﻿using System.Windows;
using Avalonia;
using Avalonia.Media;
using XLib.Avalonia.Drawing;

namespace AXNode.SubSystem.NodeEditSystem.Panel.Layer
{
    /// <summary>
    /// 选框图层
    /// </summary>
    public class SelectBoxLayer : SingleBoard
    {
        public Point Start { get; set; } = new Point();

        public Point End { get; set; } = new Point();

        public override void Render(DrawingContext context)
        {
            base.Render(context);
            var start = new Point(Start.X + 0.5, Start.Y + 0.5);
            var end = new Point(End.X + 0.5, End.Y + 0.5);
            if (End.X >= Start.X)
                context.DrawRectangle(_blue, _border, new Rect(start, end));
            else
                context.DrawRectangle(_orange, _border, new Rect(start, end));
        }

        private readonly Pen _border = new Pen(new SolidColorBrush(Color.FromRgb(255, 255, 255)), 1);
        private readonly Brush _blue = new SolidColorBrush(Color.FromArgb(32, 0, 128, 235));
        private readonly Brush _orange = new SolidColorBrush(Color.FromArgb(32, 255, 120, 0));
    }
}