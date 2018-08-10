using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConexionDB
{
    public partial class TipoProductos : Form
    {
        private String clave_tipo_producto, tipo_producto;
        public TipoProductos()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clave_tipo_producto = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            tipo_producto = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }
    }
}
