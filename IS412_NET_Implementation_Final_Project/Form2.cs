using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace IS412_NET_Implementation_Final_Project
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void dgvPartsUsed_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                double unitPrice = 0D; // set unitPrice variable to double
                int quantity = 0;      // set quantity variable to an int
                double subTotal = 0D;  // set subTotal variable to a double

                DataGridViewCell dgvcUnitPrice = dgvPartsUsed.Rows[e.RowIndex].Cells[1];  // get value of unit price from parts used datagridview
                DataGridViewCell dgvcQuantity = dgvPartsUsed.Rows[e.RowIndex].Cells[2]; // get value of quantity from parts used datagridview

                unitPrice = double.Parse(dgvcUnitPrice.EditedFormattedValue.ToString()); // set value of unitPrice
                quantity = int.Parse(dgvcQuantity.EditedFormattedValue.ToString()); // set value of quantity
                subTotal = unitPrice * quantity; // calculate subTotal from unitPrice * quantity
                dgvPartsUsed.Rows[e.RowIndex].Cells[3].Value = subTotal.ToString("F"); // write value of subTotal to sub total column in parts used datagridview

                CalculateTotal(); // calculates totalParts cost 
            }
        }

        private void dgvMaintPerformed_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            CalculateTotal();
        }

        void CalculateTotal()
        {
            double taxRate = 0D, taxAmt = 0D, totalParts = 0D, totalLabor = 0D, totalPartsAndLabor, repairTotal; // define variables

            // calculates sum of all sub totals from parts used datagridview and sets totalParts variable
            totalParts = (from DataGridViewRow row in dgvPartsUsed.Rows
                            where row.Cells[3].FormattedValue.ToString() != string.Empty
                            select Convert.ToDouble(row.Cells[3].FormattedValue)).Sum();

            // calculates sum of all labor cost totals from maint performed datagridview and sets totalLabor variable
            List<double> colMaintCost = new List<double>(from DataGridViewRow dgRow in dgvMaintPerformed.Rows
                                     where dgRow.Cells[1].EditedFormattedValue.ToString() != string.Empty
                                     select Convert.ToDouble(dgRow.Cells[1].EditedFormattedValue)).ToList();

            totalLabor = colMaintCost.Sum();

            try
            {
                taxRate = double.Parse(txtTaxRate.Text); // set taxRate to tax rate textbox variable
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid tax rate");
                txtTaxRate.Text = "8.2";
                txtTaxRate.Focus();
            }

            totalPartsAndLabor = totalParts + totalLabor; // calculate total parts and labor and set totalPartsAndLabor

            taxAmt = totalPartsAndLabor * taxRate / 100;  // calculate taxAmt based off of totalPartsAndLabor value and tax rate divided by 100
            repairTotal = totalPartsAndLabor + taxAmt;    // calculate repairTotal from sum of totalPartsAndLabor and taxAmt

            // set text boxes to appropriate values
            txtTotalParts.Text = totalParts.ToString("F");
            txtTotalLabor.Text = totalLabor.ToString("F");
            txtTaxAmt.Text = taxAmt.ToString("F");
            txtRepairTotal.Text = repairTotal.ToString("F");
        } // end CalculateTotal()

        private void Form2_Load(object sender, EventArgs e)
        {
            cboState.DisplayMember = "Name";
            cboState.ValueMember = "Abbreviations";
            cboState.DataSource = StateArray.States();
        }
    }
}
