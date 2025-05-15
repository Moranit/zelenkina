using System;
using System.Windows;
using System.Windows.Media;

namespace LineSegmentApp
{
    public partial class MainWindow : Window
    {
        private LineSegment _currentSegment;

        public MainWindow()
        {
            InitializeComponent();
            _currentSegment = new LineSegment(0,0);
            UpdateCurrentSegmentDisplay();
        }

        private void CreateSegment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double x1 = double.Parse(txtX1.Text);
                double x2 = double.Parse(txtX2.Text);

                _currentSegment = new LineSegment(x1, x2);
                UpdateCurrentSegmentDisplay();
                AddResult($"Created new segment: {_currentSegment}");
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numbers for segment coordinates.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CheckContains_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(txtCheckNumber.Text, out double number))
            {
                bool contains = _currentSegment.Contains(number);
                txtContainsResult.Text = contains ? "Contains" : "Does not contain";
                AddResult($"Segment {_currentSegment} {(contains ? "contains" : "does not contain")} {number}");
            }
            else
            {
                MessageBox.Show("Please enter a valid number to check.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GetLength_Click(object sender, RoutedEventArgs e)
        {
            double length = !_currentSegment;
            AddResult($"Length of segment {_currentSegment} is: {length}");
        }

        private void IncrementSegment_Click(object sender, RoutedEventArgs e)
        {
            _currentSegment++;
            UpdateCurrentSegmentDisplay();
            AddResult($"After increment (++segment): {_currentSegment}");
        }

        private void ExplicitToInt_Click(object sender, RoutedEventArgs e)
        {
            int x1 = (int)_currentSegment;
            AddResult($"Explicit conversion to int (X1): {x1}");
        }

        private void ImplicitToDouble_Click(object sender, RoutedEventArgs e)
        {
            double x2 = _currentSegment;
            AddResult($"Implicit conversion to double (X2): {x2}");
        }

        private void SegmentPlusNumber_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtAddNumber.Text, out int number))
            {
                var newSegment = _currentSegment + number;
                AddResult($"Segment {_currentSegment} + {number} = {newSegment}");
            }
            else
            {
                MessageBox.Show("Please enter a valid integer to add.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NumberPlusSegment_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtAddNumber.Text, out int number))
            {
                var newSegment = number + _currentSegment;
                AddResult($"{number} + Segment {_currentSegment} = {newSegment}");
            }
            else
            {
                MessageBox.Show("Please enter a valid integer to add.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SegmentLessThan_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtCompareNumber.Text, out int number))
            {
                bool result = _currentSegment < number;
                AddResult($"Segment {_currentSegment} < {number}: {result}");
            }
            else
            {
                MessageBox.Show("Please enter a valid integer to compare.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SegmentGreaterThan_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtCompareNumber.Text, out int number))
            {
                bool result = _currentSegment > number;
                AddResult($"Segment {_currentSegment} > {number}: {result}");
            }
            else
            {
                MessageBox.Show("Please enter a valid integer to compare.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateCurrentSegmentDisplay()
        {
            txtCurrentSegment.Text = _currentSegment.ToString();
        }

        private void AddResult(string message)
        {
            txtResults.Text += $"{message}\n";
            txtResults.ScrollToEnd();
        }
    }
}