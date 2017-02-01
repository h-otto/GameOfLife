using GameOfLife.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        const int MAX_BOARD_SIZE = 80;

        readonly DispatcherTimer AutoplayTimer;
        readonly Stepper Stepper;

        BoardModel mModel;
        VisualBoardModel mVisualModel;

        public MainWindow()
        {
            InitializeComponent();

            Stepper = new Stepper();

            AutoplayTimer = new DispatcherTimer();
            AutoplayTimer.Interval = TimeSpan.FromSeconds(0.5);
            AutoplayTimer.Tick += AutoplayTimer_Tick;

            //kezdeti tábla
            CreateNewBoard(10, 10);
        }

        private void CreateNewBoard(int rowCount, int colCount)
        {
            AutoplayTimer.Stop();

            if (mVisualModel != null)
                mVisualModel.Reset();

            mModel = new BoardModel(rowCount, colCount, Stepper);
            mVisualModel = new VisualBoardModel(grdBoard, mModel);
        }

        private void StopAutoplay()
        {
            AutoplayTimer.Stop();
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
            mModel.Step();
            AutoplayTimer.Start();
        }

        private void chbAutoplay_Unchecked(object sender, RoutedEventArgs e)
        {
            StopAutoplay();
        }

        private void btnNewBoard_Click(object sender, RoutedEventArgs e)
        {
            int rowCouut;
            int colCount;
            if(int.TryParse(tbRowCount.Text,out rowCouut) == false)
            {
                ShowError($"Helytelen sozszám érték: '{tbRowCount.Text}'");
                return;
            }
            if (int.TryParse(tbColCount.Text, out colCount) == false)
            {
                ShowError($"Helytelen oszlopszám érték: '{tbColCount.Text}'");
                return;
            }

            if (rowCouut < 1 || rowCouut > MAX_BOARD_SIZE)
            {
                ShowError($"Sorok száma 1-től {MAX_BOARD_SIZE}-ig terjedhet");
                return;
            }
            if (colCount < 1 || colCount > MAX_BOARD_SIZE)
            {
                ShowError($"Oszlopok száma 1-től {MAX_BOARD_SIZE}-ig terjedhet");
                return;
            }

            CreateNewBoard(rowCouut, colCount);
        }

        private void ShowError(string v)
        {
            MessageBox.Show(v, "Hiba", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }
}
