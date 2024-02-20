using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class CustomSwitchButton : CheckBox
{
    private Color borderColor = ColorTranslator.FromHtml("#B9FF42");
    private Color switchColor = Color.Black;
    private Color checkedBorderColor = ColorTranslator.FromHtml("#B9FF42");
    private Color checkedSwitchColor = ColorTranslator.FromHtml("#B9FF42");

    public CustomSwitchButton()
    {
        Appearance = Appearance.Button;
        TextAlign = ContentAlignment.MiddleCenter;
        CheckedChanged += CustomSwitchButton_CheckedChanged;
        UpdateSwitchAppearance();
        Checked = false;
    }

    private void CustomSwitchButton_CheckedChanged(object sender, EventArgs e)
    {
        UpdateSwitchAppearance();
    }

    private void UpdateSwitchAppearance()
    {
        FlatStyle = FlatStyle.Flat;
        FlatAppearance.BorderSize = 1;
        FlatAppearance.BorderColor = Checked ? checkedBorderColor : borderColor;
        FlatAppearance.CheckedBackColor = Checked ? checkedSwitchColor : switchColor;
        FlatAppearance.MouseDownBackColor = Checked ? checkedBorderColor : switchColor;
        FlatAppearance.MouseOverBackColor = Checked ? checkedBorderColor : switchColor;
    }

    protected override void OnPaint(PaintEventArgs pevent)
    {
        GraphicsPath path = new GraphicsPath();
        path.AddEllipse(ClientRectangle);
        Region = new Region(path);

        using (SolidBrush switchBrush = new SolidBrush(Checked ? checkedSwitchColor : switchColor))
        {
            pevent.Graphics.FillEllipse(switchBrush, ClientRectangle);
        }

        using (Pen borderPen = new Pen(Checked ? checkedBorderColor : borderColor, 1))
        {
            pevent.Graphics.DrawEllipse(borderPen, 0, 0, Width - 5, Height - 5);
        }

        TextRenderer.DrawText(pevent.Graphics, Text, Font, ClientRectangle, ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
    }
}
