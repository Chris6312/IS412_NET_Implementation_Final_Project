using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace IS412_NET_Implementation_Final_Project
{
    public partial class Form2 : Form
    {
        // Storage for Identity values returned from database
        private int partID;
        private int maintID;
        private List<CustomerPlusParts> custPlusParts = new List<CustomerPlusParts>();
        
        // Connection string
        string connstr = Utility.GetConnectionString();

        public Form2()
        {
            InitializeComponent();
        }

        private void dgvPartsUsed_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                string partName; // set partName variable to string
                double unitPrice = 0D; // set unitPrice variable to double
                int quantity = 0;      // set quantity variable to an int
                double subTotal = 0D;  // set subTotal variable to a double

                DataGridViewCell dgvcPartName = dgvPartsUsed.Rows[e.RowIndex].Cells[0]; // get value of part name
                DataGridViewCell dgvcUnitPrice = dgvPartsUsed.Rows[e.RowIndex].Cells[1];  // get value of unit price from parts used datagridview
                DataGridViewCell dgvcQuantity = dgvPartsUsed.Rows[e.RowIndex].Cells[2]; // get value of quantity from parts used datagridview

                partName = dgvcPartName.EditedFormattedValue.ToString();
                unitPrice = double.Parse(dgvcUnitPrice.EditedFormattedValue.ToString()); // set value of unitPrice
                quantity = int.Parse(dgvcQuantity.EditedFormattedValue.ToString()); // set value of quantity
                subTotal = unitPrice * quantity; // calculate subTotal from unitPrice * quantity
                dgvPartsUsed.Rows[e.RowIndex].Cells[3].Value = subTotal.ToString("F"); // write value of subTotal to sub total column in parts used datagridview

                // Create connection
                SqlConnection conn = new SqlConnection(connstr);

                // Create a SqlCommand, and identify it as a stored procedure
                SqlCommand cmdNewPart = new SqlCommand("CarMaint.uspNewPart", conn);
                cmdNewPart.CommandType = CommandType.StoredProcedure;

                // Add input parameter for Part Name from the stored procedure and specify what to use as its value
                cmdNewPart.Parameters.Add(new SqlParameter("@PartID", SqlDbType.Int));
                cmdNewPart.Parameters["@PartID"].Value = partID;

                // Add input parameter for Part Name from the stored procedure and specify what to use as its value
                cmdNewPart.Parameters.Add(new SqlParameter("@PartName", SqlDbType.VarChar, 50));
                cmdNewPart.Parameters["@PartName"].Value = partName;

                // Add input parameter for Unit Price from the stored procedure and specify what to use as its value
                cmdNewPart.Parameters.Add(new SqlParameter("@UnitPrice", SqlDbType.Float));
                cmdNewPart.Parameters["@UnitPrice"].Value = unitPrice;

                // Add input parameter for Quantity from the stored procedure and specify what to use as its value
                cmdNewPart.Parameters.Add(new SqlParameter("@PartQuantity", SqlDbType.Int));
                cmdNewPart.Parameters["@PartQuantity"].Value = quantity;

                // Add input parameter for Sub Total from the stored procedure and specify what to use as its value
                cmdNewPart.Parameters.Add(new SqlParameter("@SubTotal", SqlDbType.Float));
                cmdNewPart.Parameters["@SubTotal"].Value = subTotal;

                // Add input parameter for AddDate from the stored procedure and specify what to use as its value
                cmdNewPart.Parameters.Add(new SqlParameter("@AddDate", SqlDbType.Date));
                cmdNewPart.Parameters["@AddDate"].Value = DateTime.Now;

                // try-catch-finally
                try
                {
                    // Open the connection
                    conn.Open();

                    // Run the stored procedure
                    cmdNewPart.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("Data not saved"); // display error message
                }
                finally
                {
                    conn.Close(); // closes connection
                }

                CalculateTotal(); // calculates totalParts cost 
            }
        }

        private void dgvMaintPerformed_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            CalculateTotal(); // calls calculate total method when leaving cell
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
                txtTaxRate.Text = "8.2"; // sets default value for tax rate
                txtTaxRate.Focus(); // puts focus back on textbox if invalid
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
            // populates state combobox with US States on load
            cboState.DisplayMember = "Name";
            cboState.ValueMember = "Abbreviations";
            cboState.DataSource = StateArray.States();
        }

        private void txtZipCode_Leave(object sender, EventArgs e)
        {
            // check to see if zip code text box is not empty
            // and verify correct zip code format is entered
            // or throw error message
            string text = txtZipCode.Text;
            if (txtZipCode.Text != string.Empty) // check text box to see if empty
            {
                if (!Regex.Match(text, @"^\d{5}$").Success) // regex expression
                {
                    MessageBox.Show("Please enter a valid ZIP Code"); // error message
                    txtZipCode.Select(); // selects zip code text box if invalid entry
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            custPlusParts.Add(new CustomerPlusParts() // add below values custPlusParts List<> when submit button pressed 
            {
                custName = txtCustName.Text, // gets customer's name from textbox
                carYear = txtYear.Text, // gets year of vehicle from textbox
                carMake = txtMake.Text, // gets make of vehicle from textbox
                carModel = txtModel.Text, // gets model of vehicle from textbox
                maintDescription = txtDescription.Text, // gets description of maintenance needed from textbox
                totalPrice = decimal.Parse(txtRepairTotal.Text), // gets price from total repair textbox and converts to decimal format
                mechRecommendation = txtRecommend.Text // gets mechanics recommendation
            });

            string txt = "";
            foreach (CustomerPlusParts aCustPlusParts in custPlusParts) // loops thru custPlusParts list
            {
                txt += aCustPlusParts + Environment.NewLine + Environment.NewLine; // returns CustomerPlusParts ToString() and adds a line in between each entry
            }
            txtTotalSummary.Text = txt; // displays ToString() values in total daily summary textbox when submit button is pressed
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtTotalSummary.Clear(); // clears total summary textbox when Clear button clicked
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult diagResult = MessageBox.Show("Are you sure you want to quit?", "Quit?", MessageBoxButtons.YesNo);
            if (diagResult == DialogResult.Yes) // if user clicks yes
            {
                Application.Exit(); // exit the program
            }
        }
    }
}
