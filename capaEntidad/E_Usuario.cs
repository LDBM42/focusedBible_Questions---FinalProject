namespace capaEntidad
{
    public class E_Usuario
    {
        private static int _id;

        public static int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private static string _nombreusuario;

        public static string Nombreusuario
        {
            get { return _nombreusuario; }
            set { _nombreusuario = value; }
        }
        private static string _password;

        public static string Password
        {
            get { return _password; }
            set { _password = value; }
        }
       
        private static int _logged;

        public static int Logged
        {
            get { return _logged; }
            set { _logged = value; }
        }
    }
}
