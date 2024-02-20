using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class CustomSlider : UserControl
{
    private int _minimum = 0;
    private int _maximum = 100;
    private int _value = 0;

    public int Minimum
    {
        get { return _minimum; }
        set { _minimum = value; Invalidate(); }
    }

    public int Maximum
    {
        get { return _maximum; }
        set { _maximum = value; Invalidate(); }
    }

    public int Value
    {
        get { return _value; }
        set { _value = Math.Max(Minimum, Math.Min(Maximum, value)); Invalidate(); }
    }

    public event EventHandler ValueChanged;

    protected virtual void OnValueChanged(EventArgs e)
    {
        ValueChanged?.Invoke(this, e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        HandleMouseInput(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        if (e.Button == MouseButtons.Left)
        {
            HandleMouseInput(e);
        }
    }

    private void HandleMouseInput(MouseEventArgs e)
    {
        float percent = (float)e.X / Width;
        Value = (int)(percent * (Maximum - Minimum)) + Minimum;
        OnValueChanged(EventArgs.Empty);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        DrawSlider(e.Graphics);
    }

    private Color defaultStartGradientColor = ColorTranslator.FromHtml("#B9FF42");
    private Color defaultEndGradientColor = ColorTranslator.FromHtml("#000000");
    private Color defaultThumbColor = ColorTranslator.FromHtml("#B9FF42");

    private Color startGradientColor;
    private Color endGradientColor;
    private Color thumbColor;

    public Color StartGradientColor
    {
        get { return isUsingDefaultColors ? defaultStartGradientColor : startGradientColor; }
        set
        {
            startGradientColor = value;
            isUsingDefaultColors = false;
            Invalidate();
        }
    }

    public Color EndGradientColor
    {
        get { return isUsingDefaultColors ? defaultEndGradientColor : endGradientColor; }
        set
        {
            endGradientColor = value;
            isUsingDefaultColors = false;
            Invalidate();
        }
    }

    public Color ThumbColor
    {
        get { return isUsingDefaultColors ? defaultThumbColor : thumbColor; }
        set
        {
            thumbColor = value;
            isUsingDefaultColors = false;
            Invalidate();
        }
    }

    private bool isUsingDefaultColors = true;

    public void UseDefaultColors()
    {
        isUsingDefaultColors = true;
        Invalidate();
    }

    private void DrawSlider(Graphics g)
    {
        int trackHeight = 1;
        using (var trackBrush = new LinearGradientBrush(
            new Point(0, Height / 2 - trackHeight / 2),
            new Point(Width, Height / 2 - trackHeight / 2),
            StartGradientColor, EndGradientColor))
        {
            g.FillRectangle(trackBrush, 0, Height / 2 - trackHeight / 2, Width, trackHeight);
        }

        float percent = (float)(Value - Minimum) / (Maximum - Minimum);
        int thumbX = (int)(percent * Width);

        int thumbWidth = 6;
        using (var thumbBrush = new SolidBrush(ThumbColor))
        {
            g.FillEllipse(thumbBrush, thumbX - thumbWidth / 2, Height / 2 - thumbWidth / 2, thumbWidth, thumbWidth);
        }
    }

}
