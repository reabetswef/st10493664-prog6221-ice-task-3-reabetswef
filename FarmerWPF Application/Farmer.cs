using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace FarmerWPF_Application
{
    public class Farmer
    {
        //Data Members
        private String firstName;
        private String surname;
        private int farmCode;
        private int noOfCrops;

        //Properties
        public String FirstName { get { return firstName; } set { firstName = value; } }
        public String Surname { get { return surname; } set { surname = value; } }
        public int FarmCode { get { return farmCode; } set { farmCode = value; } }
        public int NoOfCrops { get { return noOfCrops; } set { noOfCrops = value; } } 
    }
}
