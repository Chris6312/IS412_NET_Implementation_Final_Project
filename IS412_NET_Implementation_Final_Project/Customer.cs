using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS412_NET_Implementation_Final_Project
{
    public class Customer : Object
    {
        public string custName { get; set; }
        public string carYear { get; set; }
        public string carMake { get; set; }
        public string carModel { get; set; }
        public string maintDescription { get; set; }

        // six parameter constructor
        public Customer(/*string name, string year, string make, string model, string description*/)
        {
            //custName = name;
            //carYear = year;
            //carMake = make;
            //carModel = model;
            //maintDescription = description;
        }

        // return string representation of Customer object
        public override string ToString()
        {
            return string.Format("{0}:\t\t{1}\r\n{2}:\t\t{3} {4} {5}\r\n{6}:\t{7}",
                "Customer's name", custName,
                "Year/Make/Model", carYear, carMake, carModel,
                "Maintenance Description", maintDescription);
        } // end method ToString
    } // end class Customer
}
