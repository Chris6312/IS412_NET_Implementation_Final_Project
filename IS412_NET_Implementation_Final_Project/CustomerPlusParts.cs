using System;

namespace IS412_NET_Implementation_Final_Project
{
    public class CustomerPlusParts : Customer
    {
        public decimal totalPrice;
        public string mechRecommendation { get; set; }

        // seven parameter derived class constructor
        public CustomerPlusParts(string name, string year, string make, string model, string description, decimal price, string recommendation) 
            : base(/*name, year, make, model, description*/)
        {
            TotalPrice = price; // validate total price via property
            mechRecommendation = recommendation;
        } // end eight parameter CustomerPlusParts constructor

        public CustomerPlusParts()
        {
        }

        public decimal TotalPrice
        {
            get
            {
                return totalPrice;
            } // end get
            set
            {
                if (value >= 0)
                    totalPrice = value;
                else
                    throw new ArgumentOutOfRangeException("TotalPrice", value, "Total Price must be greater than or equal to 0");
            } // end set
        } // end property TotalPrice
    
        // return string representation of CustomerPlusParts object
        public override string ToString()
        {
            return string.Format("{0}\r\nPrice:\t\t\t{1:C}\r\nMechanic Recommendations: {2}",
                base.ToString(), totalPrice, mechRecommendation);
                
        } // end method ToString
    }
}
