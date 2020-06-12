using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using System.IO;


namespace dbTest
{
    public partial class Form1 : Form
    {
        Usuario user; //Objeto

        public Form1()
        {
            InitializeComponent();
        }

        string cs = @"server=localhost;userid=root;password=CarlosMenchaca1704;database=dbtest"; //Conexion mysql

        public delegate MySqlConnection conexionMysql(string cc); //delegado

        conexionMysql del = new conexionMysql(conextarMySQL);


        static MySqlConnection conextarMySQL(string cc)
        {
            var con = new MySqlConnection(cc);
            return con;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //Boton Probar conexion
        {
            try
            {
                NuevoObjetoUsuario();
                ProbarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " REVISE SU CONEXION A INTERNET");
            }
        }

        private void button2_Click(object sender, EventArgs e) //Boton Refrescar o tambien mostrar datos de la bd
        {
            try
            {
                NuevoObjetoUsuario();
                Refrescar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e) //Boton Insertar o agregar
        {
            try
            {
                NuevoObjetoUsuario();
                Insertar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e) //Boton Actualizar
        {
            try
            {
                NuevoObjetoUsuario();
                Actualizar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e) //Boton Eliminar
        {
            try
            {
                Eliminar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) //Evento 1 Mostrara los datos de donde este el cursor y al dar click, se veran en los textbox
        {
            DataGridViewRow CurrentRow = dataGridView1.CurrentRow;
            txtNombre.Text = CurrentRow.Cells[1].Value.ToString();
            txtApellidoP.Text = CurrentRow.Cells[2].Value.ToString();
            txtApellidoM.Text = CurrentRow.Cells[3].Value.ToString();

        }

        private void button1_Click_1(object sender, EventArgs e) //Boton Salir
        {
            Application.Exit();
        }

        public void NuevoObjetoUsuario() //Metodo Nuevo Usuario
        {
            user = new Usuario();
            user.Nombre = txtNombre.Text;
            user.ApellidoPaterno = txtApellidoP.Text;
            user.ApellidoMaterno = txtApellidoM.Text;
        }

        private void btnBuscar_Click(object sender, EventArgs e) //Boton Buscar
        {
            try
            {
                NuevoObjetoUsuario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        // M E T O D O S
        public void Refrescar()
        {
            var con = conextarMySQL(cs);
            con.Open();
            MySqlDataAdapter MyDA = new MySqlDataAdapter();
            string sqlSelectAll = "select idusuario, nombre, apellidopaterno, apellidomaterno from usuarios";
            MyDA.SelectCommand = new MySqlCommand(sqlSelectAll, con);

            DataTable table = new DataTable();
            MyDA.Fill(table);

            BindingSource bSource = new BindingSource();
            bSource.DataSource = table;


            dataGridView1.DataSource = bSource;

        }


        public void ProbarConexion()
        {
            var con = conextarMySQL(cs);
            con.Open();

            MessageBox.Show($"MySQL version : {con.ServerVersion}");
        }

        public void Insertar()
        {
            var con = conextarMySQL(cs);
            con.Open();

            var cmd = new MySqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "insert into usuarios(nombre, apellidoPaterno, apellidoMaterno) values('" + txtNombre.Text + "', '" + txtApellidoP.Text + "', '" + txtApellidoM.Text + "')";
            cmd.ExecuteNonQuery();
        }

        public void Actualizar()
        {
            var con = conextarMySQL(cs);
            con.Open();
            var cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "update usuarios set nombre= '" + txtNombre.Text + "',apellidoPaterno='" + txtApellidoP.Text + "',apellidoMaterno= '" + txtApellidoM.Text + "'where idusuario= '" + Convert.ToInt32(txtID.Text) + "";
            cmd.CommandText = "update usuarios set nombre = '" + txtNombre.Text + "' where idusuario = " + Convert.ToInt32(txtID.Text) + "";
            cmd.ExecuteNonQuery();
        }

        public void Eliminar()
        {
            var con = conextarMySQL(cs);
            con.Open();

            var cmd = new MySqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "delete from usuarios where idusuario = " + Convert.ToInt32(txtID.Text) + ";";
            cmd.ExecuteNonQuery();
        }

        private void btnCrearArchivo_Click(object sender, EventArgs e)
        {
            try
            {
                string[] lineas = {txtEscribirArchivo.Text };
                using (StreamWriter outputfile = new StreamWriter("C:\\Users\\USUARIO\\Desktop\\Miarchivo.txt"))
                {
                    foreach (string linea in lineas)
                    {
                        outputfile.WriteLine(linea);
                    }
                }
                MessageBox.Show("Archivo creado, se encuentra en el escritorio");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
        
    

