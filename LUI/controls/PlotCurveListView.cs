using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LUI.controls
{
    public partial class PlotCurveListView : UserControl
    {
        public GraphControl Graph { get; set; }

        public int Count
        {
            get
            {
                return DataGrid.Rows.Count;
            }
        }

        public IEnumerable<DataGridViewRow> Rows
        {
            get
            {
                foreach (DataGridViewRow row in DataGrid.Rows)
                {
                    yield return row;
                }
            }
        }

        public IEnumerable<IList<double>> Curves
        {
            get
            {
                return Rows.Select((row) => row.Tag as double[]);
            }
        }

        public IEnumerable<IList<double>> SaveCurves
        {
            get
            {
                foreach (DataGridViewRow row in 
                    Rows.Where((row) => (bool)row.Cells[CurveSave.Index].Value))
                {
                    yield return row.Tag as double[];
                }
            }
        }

        public IEnumerable<IList<double>> DisplayCurves
        {
            get
            {
                foreach (DataGridViewRow row in 
                    Rows.Where((row) => (bool)row.Cells[CurveVisible.Index].Value))
                {
                    yield return row.Tag as double[];
                }
            }
        }

        public IEnumerable<string> CurveNames
        {
            get
            {
                return Rows.Select((row) => 
                    row.Cells[CurveName.Index].Value as string);
            }
        }

        public IEnumerable<string> SaveCurveNames
        {
            get
            {
                foreach (DataGridViewRow row in 
                    Rows.Where((row) => (bool)row.Cells[CurveSave.Index].Value))
                {
                    yield return row.Cells[CurveName.Index].Value as string;
                }
            }
        }

        public IEnumerable<string> DisplayCurveNames
        {
            get
            {
                foreach (DataGridViewRow row in 
                    Rows.Where((row) => (bool)row.Cells[CurveVisible.Index].Value))
                {
                    yield return row.Cells[CurveName.Index].Value as string;
                }
            }
        }

        public double[] SelectedCurve
        {
            get
            {
                if (DataGrid.SelectedRows.Count > 0)
                    return DataGrid.SelectedRows[0].Tag as double[];
                return null;
            }
        }

        public event EventHandler SelectionChanged;

        public PlotCurveListView()
        {
            InitializeComponent();

            DataGrid.CellValidating += DataGrid_CellValidating;
            DataGrid.CellValueChanged += DataGrid_CellValueChanged;
            DataGrid.CurrentCellDirtyStateChanged += DataGrid_CurrentCellDirtyStateChanged;
            DataGrid.SelectionChanged += OnSelectionChanged;
        }

        void OnSelectionChanged(object sender, EventArgs e)
        {
            SelectionChanged.Raise(sender, e);
        }

        void DataGrid_CurrentCellDirtyStateChanged(object sender, System.EventArgs e)
        {
            if (DataGrid.CurrentCell is DataGridViewCheckBoxCell)
            {
                DataGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        void DataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == CurveVisible.Index &&
                DataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].IsInEditMode)
            {
                PlotCurves(true);
            }
        }

        void DataGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == CurveName.Index)
            {
                if (CurveNames.Any((name) => name == (string)e.FormattedValue))
                {
                    DataGrid.CancelEdit();
                }
            }
        }

        public void Add(double[] X, double[] Curve)
        {
            var RowIndex = DataGrid.Rows.Add();

            DataGrid.Rows[RowIndex].Tag = Curve.Clone(); // Deep copy for value types.
            DataGrid.Rows[RowIndex].Cells[CurveVisible.Index].Tag = X.Clone();

            DataGrid.Rows[RowIndex].Cells[CurveName.Index].Value = "Curve_" + DataGrid.RowCount.ToString();
            var color = Graph.NextColor;
            DataGrid.Rows[RowIndex].Cells[CurveColor.Index].Tag = System.Drawing.Color.FromArgb(color.ToArgb()); // Deep copy.
            DataGrid.Rows[RowIndex].Cells[CurveColor.Index].Style = new DataGridViewCellStyle() { ForeColor = color };
            DataGrid.Rows[RowIndex].Cells[CurveColor.Index].Value = color.IsNamedColor ? color.Name : "Unkown";
            DataGrid.Rows[RowIndex].Cells[CurveVisible.Index].Value = true;
            DataGrid.Rows[RowIndex].Cells[CurveSave.Index].Value = true;
            
            DataGrid.ClearSelection();
            DataGrid.Rows[RowIndex].Selected = true;
            PlotRow(RowIndex);
        }

        public double[] FindCurveByName(string _CurveName)
        {
            var row = Rows.SingleOrDefault((it) => (string)it.Cells[CurveName.Index].Value == _CurveName);
            return row != null ? row.Tag as double[] : null;
        }

        public void PlotCurves()
        {
            PlotCurves(false);
        }

        public void PlotCurves(bool ClearBeforePlot)
        {
            if (ClearBeforePlot) Graph.ClearData();
            foreach (var row in Rows)
            {
                if ((bool)row.Cells[CurveVisible.Index].Value)
                {
                    Graph.MarkerColor = (System.Drawing.Color)row.Cells[CurveColor.Index].Tag;
                    Graph.DrawPoints((double[])row.Cells[CurveVisible.Index].Tag, (double[])row.Tag);
                }
            }
            Graph.Invalidate();
        }

        private void PlotRow(int RowIndex)
        {
            PlotRow(DataGrid.Rows[RowIndex]);
        }

        private void PlotRow(DataGridViewRow Row)
        {
            Graph.MarkerColor = (System.Drawing.Color)Row.Cells[CurveColor.Index].Tag;
            Graph.DrawPoints((double[])Row.Cells[CurveVisible.Index].Tag, (double[])Row.Tag);
            Graph.Invalidate();
        }
    }
}
