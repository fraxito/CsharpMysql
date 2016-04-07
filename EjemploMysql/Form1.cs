using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//añadimos los import de MySql
using System.Data.Odbc;
using MySql.Data.Types;
using MySql.Data.MySqlClient;

namespace EjemploMysql
{
    public partial class Form1 : Form
    {
        //este ejemplo conectará con una base de datos
        //Mysql

        //necesito 5 parámetros:
        //Server: la ip o nombre dns del servidor
        //Database: nombre de la base de datos
        //Uid: usuario (ojo: no se puede dejar en blanco)
        //Pwd: clave de acceso si la tuviera
        //Port: default=3306 

        //parámetros de la conexión
        private string connStr;

        //variable que maneja la conexion
        private MySqlConnection conn;

        //consulta que quiero hacer a la base de datos
        private String sentencia_SQL;

        //variable que sirve para crear la conexion
        private static MySqlCommand comando;

        //guarda el resultado de la consulta
        private MySqlDataReader resultado;

        private DataTable datos = new DataTable();

        private int contadorFila = 0;
        private int numeroFilas = 0;

        public Form1()
        {
            InitializeComponent();

            connStr = "Server=localhost; Database=test; Uid=root; Pwd=root; Port=3306";
            conn = new MySqlConnection(connStr);
            conn.Open();
            sentencia_SQL = "Select * from pokemon";
            comando = new MySqlCommand(sentencia_SQL, conn);
            resultado = comando.ExecuteReader();
            datos.Load(resultado);
            conn.Close();
            numeroFilas = datos.Rows.Count; 
        }

        private String consulta(String columna, int numFila) {
            //si consigue leer un dato de la base de datos es que todo
            //esta ok
            DataRow fila = datos.Rows[numFila];
            if (fila != null) {
                return fila[columna].ToString();
            }
            else return "no existe";
        }


        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = consulta("name", contadorFila);
            label2.Text = consulta("habitat", contadorFila);
            contadorFila++;
        }
    }
}
