using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace dbTest
{
    class ArchivoSerializacion<Tipo>
    {
        private string _strNombreArchivo;

        public string NombreArchivo
        {
            get { return _strNombreArchivo; }
            set { _strNombreArchivo = value; }
        }

        private System.IO.FileStream flujo;
        private System.Runtime.Serialization.Formatters.Binary.BinaryFormatter seriador;
        private long _lngPosicionAnterior;

        public long PosicionAnterior
        {
            get { return _lngPosicionAnterior; }
            set { _lngPosicionAnterior = value; }
        }
        public ArchivoSerializacion(string strNombreArchivo)
        {
            _strNombreArchivo = strNombreArchivo;
            PosicionAnterior = 0;
        }
        public void Crear()
        {
            flujo = new FileStream(NombreArchivo, FileMode.Create);
            seriador = new BinaryFormatter();
        }
        public void AbrirEnModoEscritura()
        {
            if (File.Exists(NombreArchivo))
            {
                flujo = new FileStream(NombreArchivo, FileMode.Append);
                seriador = new BinaryFormatter();
            }
            else
            {
                this.Crear();
            }
            PosicionAnterior = 0;
        }
        public void AbrirEnModoLectura()
        {
            if (File.Exists(NombreArchivo))
            {
                flujo = new FileStream(NombreArchivo, FileMode.Open);
                seriador = new BinaryFormatter();
                PosicionAnterior = 0;
            }
            else
            {
                throw new Exception("No existe el archivo: " + NombreArchivo);
            }
        }
        public void AbrirEnModoLecturaYEscritura()
        {
            if (File.Exists(NombreArchivo))
            {
                flujo = new FileStream(NombreArchivo, FileMode.Open, FileAccess.ReadWrite);
            }
            else
            {
                this.Crear();
            }
            seriador = new BinaryFormatter();
            PosicionAnterior = 0;
        }
        public void GrabarObjeto(Tipo miObjeto)
        {
            seriador.Serialize(flujo, miObjeto);
        }
        public Tipo LeerObjeto()
        {
            PosicionAnterior = flujo.Position;
            Tipo miObjeto = (Tipo)(seriador.Deserialize(flujo));
            return (miObjeto);
        }
        public void ModificarObjeto(Tipo miObjeto)
        {
            flujo.Seek(PosicionAnterior, SeekOrigin.Begin);
            this.GrabarObjeto(miObjeto);
        }
        public void Cerrar()
        {
            if (flujo != null)
            {
                flujo.Close();
            }
        }
        public void EliminarArchivo()
        {
            File.Delete(NombreArchivo);
        }
        public void RenombrarArchivo(string strNuevoNombreArchivo)
        {
            File.Move(strNuevoNombreArchivo, NombreArchivo);
        }
        ~ArchivoSerializacion()
        {
            this.Cerrar();
        }
    }
}
