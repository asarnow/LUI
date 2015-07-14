using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LUI.controls
{
    public partial class PlotCurveListView : UserControl
    {
        public GraphControl Graph { get; set; }

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
                foreach (DataGridViewRow row in Rows.Where((row) => (bool)row.Cells["CurveSave"].Value))
                {
                    yield return row.Tag as double[];
                }
            }
        }

        public IEnumerable<IList<double>> DisplayCurves
        {
            get
            {
                foreach (DataGridViewRow row in Rows.Where((row) => (bool)row.Cells["CurveVisible"].Value))
                {
                    yield return row.Tag as double[];
                }
            }
        }

        public IEnumerable<string> CurveNames
        {
            get
            {
                return Rows.Select((row) => row.Cells["CurveName"].Value as string);
            }
        }

        public IEnumerable<string> SaveCurveNames
        {
            get
            {
                foreach (DataGridViewRow row in Rows.Where((row) => (bool)row.Cells["CurveSave"].Value))
                {
                    yield return row.Cells["CurveName"].Value as string;
                }
            }
        }

        public IEnumerable<string> DisplayCurveNames
        {
            get
            {
                foreach (DataGridViewRow row in Rows.Where((row) => (bool)row.Cells["CurveVisible"].Value))
                {
                    yield return row.Cells["CurveName"].Value as string;
                }
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

        public PlotCurveListView()
        {
            InitializeComponent();

            DataGrid.CellValidating += DataGrid_CellValidating;
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

        public void Add(double[] Curve)
        {
            var RowIndex = DataGrid.Rows.Add();
            DataGrid.Rows[RowIndex].Cells["CurveName"].Value = "Curve_" + DataGrid.RowCount.ToString();
            var color = Graph.NextColor;
            DataGrid.Rows[RowIndex].Cells["CurveColor"].Tag = System.Drawing.Color.FromArgb(color.ToArgb()); // Deep copy.
            DataGrid.Rows[RowIndex].Cells["CurveColor"].Style = new DataGridViewCellStyle() { ForeColor = color };
            DataGrid.Rows[RowIndex].Cells["CurveColor"].Value = color.IsNamedColor ? color.Name : "Unkown";
            DataGrid.Rows[RowIndex].Cells["CurveVisible"].Value = true;
            DataGrid.Rows[RowIndex].Cells["CurveSave"].Value = true;
            DataGrid.Rows[RowIndex].Tag = Curve.Clone(); // Deep copy for value types.
        }

        public double[] FindCurveByName(string CurveName)
        {
            var row = Rows.SingleOrDefault((it) => (string)it.Cells["CurveName"].Value == CurveName);
            return row != null ? row.Tag as double[] : null;
        }
    }
}
