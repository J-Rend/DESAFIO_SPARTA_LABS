namespace GestaoOficinas.Api.ValueObjects
{
    public class LoginObject
    {
        public LoginObject(string CNPJ, string password)
        {
            this.CNPJ = CNPJ;
            Password = password;
        }

        public string CNPJ { get; private set; }
        public string Password { get; private set; }


    }
}
