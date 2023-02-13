using System.Collections.Generic;
using System.Drawing;

namespace Task3
{
    public partial class Form1 : Form
    {
        Random _random = new Random();

        Pen _reproductionColor;
        Pen _newColor;
        List<Point> _point = new List<Point>();
        class Lines 
        {
            public List<Point> LinePoint = new List<Point>(); 
            public Color Color;            
            public Lines(List<Point> _point, Color color)
            {
                for (int i = 0; i < _point.Count; i++)
                {
                    LinePoint.Add(_point[i]);
                }
                Color = color;
            }
        }
        public Form1()
        {
            InitializeComponent();
            ColorButton.Enabled = false;
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            if (ColorDialog.ShowDialog() == DialogResult.OK)
            {
                _newColor = new Pen(ColorDialog.Color, 10);
                ColorButton.BackColor = _newColor.Color;
            }
        }

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Panel.Capture = true;
                _reproductionColor = new Pen(Color.FromArgb(_random.Next(255), _random.Next(255), _random.Next(255), _random.Next(255)), 10);
            }
        }

        private void Panel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Panel.Capture = false;
                _point.Clear();
            }
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (Panel.Capture)
            {
                _point.Add(new Point(e.X, e.Y));
                if (_point.Count >= 2)
                {
                    Graphics g = Panel.CreateGraphics();
                    g.DrawLine(_reproductionColor, _point[_point.Count - 2], _point[_point.Count - 1]);
                }
            }
        }
    }
}