using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GameOfLife.Wpf
{
    class VisualBoardModel
    {
        const double MaxCellSize = 300000;

        readonly Brush ElemVisibleFill = new SolidColorBrush(Colors.ForestGreen);
        readonly Brush ElemHiddenFill = new SolidColorBrush(Colors.Transparent);
        readonly Transform ElemTransform = new ScaleTransform(0.8, 0.8);

        readonly Grid Layout;
        readonly int RowCount;
        readonly int ColCount;
        readonly BoardModel Model;

        readonly Shape[,] Elements;

        public VisualBoardModel(Grid layout, BoardModel model)
        {
            Layout = layout;
            RowCount = model.RowCount;
            ColCount = model.ColCount;
            Model = model;

            ElemVisibleFill.Freeze();
            ElemHiddenFill.Freeze();
            ElemTransform.Freeze();

            Elements = new Shape[model.RowCount, model.RowCount];

            CreateLayout();
            CreateCells();

            Model.ModelChanged += Model_ModelChanged;
        }

        private void Model_ModelChanged(ModelChangedEventArgs e)
        {
            if (e.Type == EModelChangeType.Reset)
                UpdateFromModel();
            else
            {
                foreach (var point in e.ChangedCells)
                {
                    var val = Model[point.Row, point.Col];
                    Elements[point.Row, point.Col].Fill = val ? ElemVisibleFill : ElemHiddenFill;
                }
            }
        }

        private void CreateCells()
        {
            Layout.Children.Clear();

            for (int row = 0; row < RowCount; row++)
            {
                for (int col = 0; col < ColCount; col++)
                {
                    var elem = new Rectangle
                    {
                        Stretch = Stretch.Uniform,
                        Stroke = Brushes.Black,
                        StrokeThickness = 0.5,
                        Fill = ElemHiddenFill,
                        //Margin = new Thickness(0.5),
                        RenderTransformOrigin = new Point(0.5, 0.5),
                        //RenderTransform = ElemTransform,
                    };
                    elem.ToolTip = $"({row}; {col})";

                    Elements[row, col] = elem;

                    elem.SetValue(Grid.RowProperty, row);
                    elem.SetValue(Grid.ColumnProperty, col);
                    Layout.Children.Add(elem);
                }
            }
        }

        private void CreateLayout()
        {
            Layout.RowDefinitions.Clear();
            for (int i = 0; i < RowCount; i++)
            {
                Layout.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(1, GridUnitType.Star),
                    MaxHeight = MaxCellSize,
                });
            }

            Layout.ColumnDefinitions.Clear();
            for (int i = 0; i < ColCount; i++)
            {
                Layout.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(1, GridUnitType.Star),
                    MaxWidth = MaxCellSize,
                });
            }
        }

        private void UpdateFromModel()
        {
            for (int row = 0; row < RowCount; row++)
            {
                for (int col = 0; col < ColCount; col++)
                {
                    var val = Model[row, col];
                    Elements[row, col].Fill = val ? ElemVisibleFill : ElemHiddenFill;
                }
            }
        }
    }
}
