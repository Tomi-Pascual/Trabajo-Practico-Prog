﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.IO;

namespace Trabajo_Practico
{
    public partial class Sign_In : Form
    {
        public Sign_In()
        {
            InitializeComponent();

        }

        static public string conexion_database = "Data Source=DESKTOP-K0KPPT1;Initial Catalog=dppractica;Integrated Security=True";
        

        private void btn_iniciarSesion_Click(object sender, EventArgs e)
        {
            string email = txt_email.Text;

            if (txt_email.Text == "")
            {
                MessageBox.Show("Ingrese su email.","Error", MessageBoxButtons.OK,MessageBoxIcon.Error );
            }
            else if (txt_email.Text != "admin" && txt_email.Text != "123" && ValidacionEMAIL(e, email) == false)
            {
                MessageBox.Show("Mala redaccion del email.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(txt_contrasena.Text == "")
            {
                MessageBox.Show("Ingrese su contrasena.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (login() == false)
            {
                MessageBox.Show("Datos Incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if(txt_email.Text == "admin" && txt_contrasena.Text == "123456")
                {
                    Administrador_de_Usuarios frm1 = new Administrador_de_Usuarios(conexion_database);
                    frm1.ShowDialog();
                }
                else
                {
                    string email_global = txt_email.Text;
                    Store frm = new Store(email_global);
                    frm.ShowDialog();
                }
            }
        }

        private void lbl_crearCuenta_Click(object sender, EventArgs e)
        {
            Sign_Up frm = new Sign_Up(conexion_database);   
            frm.ShowDialog();
        }

        public bool login()
        {
            SqlConnection cn = new SqlConnection(conexion_database);

            cn.Open();
            SqlCommand cmd = new SqlCommand("select * from usuarios where email = '" + txt_email.Text + "' and contrasena = '" + txt_contrasena.Text + "'", cn);
            SqlDataReader registro = cmd.ExecuteReader();

            if (registro.Read())
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        static public bool ValidacionEMAIL(EventArgs e, string Mail)//validacion formato de direccion de e-mail
        {

            Regex mRegxExpression;

            if (Mail.Trim() != string.Empty)
            {

                mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");

                if (!mRegxExpression.IsMatch(Mail.Trim()))
                {
                    //no es correcta
                    return false;
                }
                else
                {
                    //es correcta
                    return true;
                }

            }
            else
            {
                //no es correcta, esta vacia
                return false;
            }

        }

        private void Sign_In_Load(object sender, EventArgs e)
        {
            //
        }
    }
}