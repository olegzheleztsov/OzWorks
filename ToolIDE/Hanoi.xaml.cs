using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Oz.Algorithms.Rod;

namespace ToolIDE
{
    public partial class Hanoi : Window
    {
        private readonly List<Disk> _disks = new();
        private const double MaxDiskWidth = 200;
        private const double MinDiskWidth = 50;
        private const double DiskHeight = 8;
        private const double DiskDistance = 10;
        private const double FirstPillarX = 150;
        private const double SecondPillarX = 350;
        private const double ThirdPillarX = 550;
        private TimeSpan _moveDelay = TimeSpan.FromSeconds(1);
        private CancellationTokenSource _cancellationTokenSource;

        private readonly HanoiSolver.Pillar[] _pillars = new[]
        {
            new HanoiSolver.Pillar(0),
            new HanoiSolver.Pillar(1),
            new HanoiSolver.Pillar(2)
        };

        
        
        
        private Brush _diskColor = Brushes.Green;
        
        public Hanoi()
        {
            InitializeComponent();
        }

        private void CreateDisks(int count)
        {
            ClearDisks();

            double widthDiff = (MaxDiskWidth - MinDiskWidth) / count;

            for (int i = 0; i < count; i++)
            {
                double diskWidth = MaxDiskWidth - i * widthDiff;
                double diskHeight = DiskHeight;
                double xPos = GetDiskXPos(diskWidth, 0);
                double yPos = GetBaseCanvasHeight() - i * DiskDistance;
                Rectangle rectangle = new Rectangle()
                {
                    Width = diskWidth,
                    Height = diskHeight,
                    Fill = _diskColor,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1
                };
                _drawCanvas.Children.Add(rectangle);
                Canvas.SetLeft(rectangle, xPos);
                Canvas.SetTop(rectangle, yPos);
                var disk = new Disk(0, rectangle, count - i);
                _disks.Add(disk);
            }

        }

        private double GetDiskXPos(double diskWidth, int pillarIndex)
            => pillarIndex switch
            {
                0 => FirstPillarX - diskWidth / 2,
                1 => SecondPillarX - diskWidth / 2,
                2 => ThirdPillarX - diskWidth / 2,
                _ => throw new ArgumentException()
            };

        private double GetBaseCanvasHeight() => _drawCanvas.ActualHeight - 20;

        private Disk GetSmallestDisk(HanoiSolver.Pillar pillar)
        {
            return _disks.Where(d => d.PillarIndex == pillar.Index).OrderBy(d => d.DiskSize).FirstOrDefault();
        }

        private void MoveDisk(HanoiSolver.Pillar fromPillar, HanoiSolver.Pillar toPillar)
        {
            var disk = GetSmallestDisk(fromPillar);
            var newX = GetDiskXPos(disk.Rectangle.Width, toPillar.Index);
            int toPillarCount = _disks.Count(d => d.PillarIndex == toPillar.Index);
            var newY = GetBaseCanvasHeight() - toPillarCount * DiskDistance;
            disk.PillarIndex = toPillar.Index;
            //Canvas.SetLeft(disk.Rectangle, newX);
            //Canvas.SetTop(disk.Rectangle, newY);


            var oldX = Canvas.GetLeft(disk.Rectangle);
            var oldY = Canvas.GetTop(disk.Rectangle);

            var xAnim = new DoubleAnimation()
            {
                From = oldX,
                To = newX,
                Duration = TimeSpan.FromSeconds(0.6)
            };
            var yAnim = new DoubleAnimation()
            {
                From = oldY,
                To = newY,
                Duration = TimeSpan.FromSeconds(0.6)
            };
            Storyboard.SetTarget(xAnim, disk.Rectangle);
            Storyboard.SetTarget(yAnim, disk.Rectangle);
            Storyboard.SetTargetProperty(xAnim, new PropertyPath("(Canvas.Left)"));
            Storyboard.SetTargetProperty(yAnim, new PropertyPath("(Canvas.Top)"));
            var s = new Storyboard();
            s.Children.Add(xAnim);
            s.Children.Add(yAnim);
            s.Begin();
        }
        
        

        private void ClearDisks()
        {
            foreach (var disk in _disks)
            {
                _drawCanvas.Children.Remove(disk.Rectangle);
            }
            _disks.Clear();
        }

        private async void OnStart(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(_countTextBox.Text);
            CreateDisks(count);
            _cancellationTokenSource = new CancellationTokenSource();
            var solver = new HanoiSolver(count);
            _startButton.IsEnabled = false;
            await solver.SolveAsync(_pillars[0], _pillars[1], _pillars[2], int.Parse(_countTextBox.Text),
                async (pFrom, pTo) =>
                {
                    await Task.Delay(_moveDelay);
                    Dispatcher.BeginInvoke(
                        () => MoveDisk(pFrom, pTo));
                }, _cancellationTokenSource.Token);
            _cancellationTokenSource = null;
            _startButton.IsEnabled = true;
        }
    }
}