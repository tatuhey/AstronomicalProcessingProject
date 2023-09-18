using System;
using System.Drawing;
using System.Windows.Forms;

public class CustomButton : Button
{
    public Color OutlineColor { get; set; } = Color.Black;
    public Color FillColor { get; set; } = Color.White;

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        // Create a Graphics object for drawing
        using (Graphics g = e.Graphics)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Set the outline color and fill color
            using (Pen outlinePen = new Pen(OutlineColor, 2))
            using (SolidBrush fillBrush = new SolidBrush(FillColor))
            {
                SizeF textSize = g.MeasureString(Text, Font);
                float x = (Width - textSize.Width) / 2;
                float y = (Height - textSize.Height) / 2;

                // Draw the outline
                g.DrawString(Text, Font, outlinePen.Brush, x - 1, y - 1);
                g.DrawString(Text, Font, outlinePen.Brush, x - 1, y + 1);
                g.DrawString(Text, Font, outlinePen.Brush, x + 1, y - 1);
                g.DrawString(Text, Font, outlinePen.Brush, x + 1, y + 1);

                // Draw the filled text
                g.DrawString(Text, Font, fillBrush, x, y);
            }
        }
    }
}
