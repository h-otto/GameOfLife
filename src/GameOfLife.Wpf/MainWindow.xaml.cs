using GameOfLife.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GameOfLife.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly DispatcherTimer AutoplayTimer;
        readonly Stepper Stepper;

        BoardModel mModel;
        VisualBoardModel mVisualModel;

        public MainWindow()
        {
            InitializeComponent();

            Stepper = new Stepper();
            mModel = new BoardModel(50, 50, Stepper);
            mVisualModel = new VisualBoardModel(grdBoard, mModel);

            AutoplayTimer = new DispatcherTimer();
            AutoplayTimer.Interval = TimeSpan.FromSeconds(0.5);
            AutoplayTimer.Tick += AutoplayTimer_Tick;
        }

        private void AutoplayTimer_Tick(object sender, EventArgs e)
        {
            mModel.Step();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            mModel.Clear();
        }

        private void btnStep_Click(object sender, RoutedEventArgs e)
        {
            mModel.Step();
        }

        private void Blinker_Click(object sender, RoutedEventArgs e)
        {
            mModel.Clear();
            mModel[2, 1] = true;
            mModel[2, 2] = true;
            mModel[2, 3] = true;
        }

        private void Beacon_Click(object sender, RoutedEventArgs e)
        {
            mModel.Clear();
            mModel[2, 2] = true;
            mModel[2, 3] = true;
            mModel[2, 4] = true;
            mModel[3, 1] = true;
            mModel[3, 2] = true;
            mModel[3, 3] = true;
        }

        private void btnRandom_Click(object sender, RoutedEventArgs e)
        {
            mModel.Clear();
            var rnd = new Random();
            for (int row = 0; row < mModel.RowCount; row++)
            {
                for (int col = 0; col < mModel.ColCount; col++)
                {
                    if (rnd.Next(100) < 50)
                    {
                        mModel[row, col] = true;
                    }
                }
            }
        }

        private void chbAutoplay_Checked(object sender, RoutedEventArgs e)
        {
            AutoplayTimer.Start();
        }

        private void chbAutoplay_Unchecked(object sender, RoutedEventArgs e)
        {
            AutoplayTimer.Stop();
        }
    }
}
