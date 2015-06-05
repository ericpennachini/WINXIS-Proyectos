using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestMvcApplication2.Models
{
    public class Usuario
    {
        #region Campos privados

        private Int32 _idUsuario = new Int32();
        private Int32 _dni = new Int32();
        private string _nombre;
        private string _apellido;
        private DateTime _fechaNacimiento;
        private string _domicilio;
        private string _localidad;
        private List<Mail> _listaMails;
        private List<Telefono> _listaTelefonos;

        #endregion

        #region Propiedades

        public Int32 IdUsuario
        {
            get { return _idUsuario; }
            set { _idUsuario = value; }
        }

        public Int32 Dni
        {
            get { return _dni; }
            set { _dni = value; }
        }
        
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }

        public DateTime FechaNacimiento
        {
            get { return _fechaNacimiento; }
            set { _fechaNacimiento = value; }
        }

        public string Domicilio
        {
            get { return _domicilio; }
            set { _domicilio = value; }
        }

        public string Localidad
        {
            get { return _localidad; }
            set { _localidad = value; }
        }

        public List<Mail> ListaMails
        {
            get { return _listaMails; }
            set { _listaMails = value; }
        }
        
        public List<Telefono> ListaTelefonos
        {
            get { return _listaTelefonos; }
            set { _listaTelefonos = value; }
        }

        #endregion

        #region Metodos publicos

        public Usuario() 
        {
            _listaMails = new List<Mail>();
            _listaTelefonos = new List<Telefono>();
        }

        public int CalcularEdad()
        {
            // TODO Generar el código. Averiguar manejo de fechas, restar, sumar dias....
            return 0;
        }

        public void AgregarMail(Mail nuevoMail)
        {
            _listaMails.Add(nuevoMail);
        }

        public void AgregarTelefono(Telefono nuevoTelefono)
        {
            _listaTelefonos.Add(nuevoTelefono);
        }


        #endregion

    }

    public class Telefono
    {
        private string _numeroTel;
        private string _tipoTel;

        public string NumeroTel
        {
            get { return _numeroTel; }
            set { _numeroTel = value; }
        }
        public string TipoTel
        {
            get { return _tipoTel; }
            set { _tipoTel = value; }
        }

        public Telefono(string num, string tipo) 
        {
            _numeroTel = num;
            _tipoTel = tipo;
        }
    }

    public class Mail
    {
        private string _direccionMail;
        private string _tipoMail;

        public string DireccionMail
        {
            get { return _direccionMail; }
            set { _direccionMail = value; }
        }
        public string TipoMail
        {
            get { return _tipoMail; }
            set { _tipoMail = value; }
        }

        public Mail(string mail, string tipo) 
        {
            _direccionMail = mail;
            _tipoMail = tipo;
        }

    }
}