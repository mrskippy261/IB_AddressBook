using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IB_AddressBook
{
    internal class IndividualCOntact
    {
        private int Contact_ID = 0;
        private string Contact_Name = string.Empty;
        private string Contact_Address = string.Empty;
        private string Contact_CNum = string.Empty;
        private string Contact_EAdd = string.Empty;

        public IndividualCOntact(int cont_Count, string name, string add, string cNum, string eAdd)
        {
            Contact_ID = cont_Count;
            Contact_Name = name;
            Contact_Address = add;
            Contact_CNum = cNum;
            Contact_EAdd = eAdd;
        }

        public string icInfoDump()
        {
            return $"{Contact_ID},{Contact_Name}" +
                $",{Contact_Address},{Contact_CNum},{Contact_EAdd}";
        }
    }
}
