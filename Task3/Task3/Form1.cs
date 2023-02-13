namespace Task3
{
    public partial class Form1 : Form
    {
        Random _random = new Random();

        Pen _pen;
        Color _lineColor;
        List<Point> _points = new List<Point>();
        class Lines
        {
            public List<Point> LinePoints = new List<Point>();
            public Color PenColor;
            public Lines(List<Point> _points, Color color)
            {
                for (int i = 0; i < _points.Count; i++)
                {
                    LinePoints.Add(_points[i]);
                }
                PenColor = color;
            }
        }
        List<Lines> _lines = new List<Lines>(); // Массив всех линий, нарисованных на панели
        int count = 0;
        public Form1()
        {
            InitializeComponent();
            ColorButton.Enabled = false;
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            Graphics g = Panel.CreateGraphics();
            if (ColorDialog.ShowDialog() == DialogResult.OK)
            {
                ColorButton.BackColor = ColorDialog.Color;
                _lines[LinesListBox.SelectedIndex].PenColor = ColorDialog.Color;
                Pen newpen = new Pen(ColorDialog.Color, 10);
                for (int i = 1; i < _lines[LinesListBox.SelectedIndex].LinePoints.Count; i++)
                {
                    g.DrawLine(new Pen(Color.White, 20), _lines[LinesListBox.SelectedIndex].LinePoints[i - 1], _lines[LinesListBox.SelectedIndex].LinePoints[i]);
                    g.DrawLine(newpen, _lines[LinesListBox.SelectedIndex].LinePoints[i - 1], _lines[LinesListBox.SelectedIndex].LinePoints[i]);
                }
            }
        }

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Panel.Capture = true;
                _lineColor = Color.FromArgb(_random.Next(255), _random.Next(255), _random.Next(255), _random.Next(255));
            }
        }

        private void Panel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Panel.Capture = false;

                _lines.Add(new Lines(_points, _lineColor));
                count++;
                LinesListBox.Items.Add("Фигура " + count);
                _points.Clear();
            }
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (Panel.Capture)
            {
                _points.Add(new Point(e.X, e.Y));
                _pen = new Pen(_lineColor, 10);

                if (_points.Count >= 2)
                {
                    Graphics g = Panel.CreateGraphics();
                    g.DrawLine(_pen, _points[_points.Count - 2], _points[_points.Count - 1]);
                }
            }
        }

        private void LinesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Graphics g = Panel.CreateGraphics();
            ColorButton.BackColor = _lines[LinesListBox.SelectedIndex].PenColor;
            ColorButton.Enabled = true;
            for (int i = 1; i < _lines[LinesListBox.SelectedIndex].LinePoints.Count; i++)
            {
                g.DrawLine(new Pen(_lineColor,20), _lines[LinesListBox.SelectedIndex].LinePoints[i - 1], _lines[LinesListBox.SelectedIndex].LinePoints[i]);
            }

        }
    }
}