using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace dbTest
{
    class Usuario 
    {
        private string _strNombre, _strApellidoPaterno, _strApellidoMaterno;
        private int _intId;

        public int ID
        {
            get { return _intId; }
            set { _intId = value; }
        }

        public string Nombre
        {
            get { return _strNombre; }
            set { _strNombre = value; }
        }

        public string ApellidoPaterno
        {
            get { return _strApellidoPaterno; }
            set { _strApellidoPaterno = value; }
        }


        public string ApellidoMaterno
        {
            get { return _strApellidoMaterno; }
            set { _strApellidoMaterno = value; }
        }

        
        
        
    }
}
