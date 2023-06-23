using System.Data;

namespace SolicitaTCC.API.csharp.Models
{
    public class userLogin
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
    }

    public class Login
    {
        public string PessoaID { get; set; }

        public Login(string id_pessoa)
        {
            this.PessoaID = id_pessoa;
        }
    }

    public class CreateLogin
    {
        public string Nome { get; set; }
        public int TipoPessoaID { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }

    public class Pessoa
    {
        public int PessoaID { get; set; }
        public string Nome { get; set; }
        public int TipoPessoaID { get; set; }
        public string Email { get; set; }


    }

    public class Functions
    {
        public List<Pessoa> DataTableToPessoaList(DataTable dt)
        {
            List<Pessoa> pessoas = new List<Pessoa>();

            foreach (DataRow row in dt.Rows)
            {
                Pessoa pessoa = new Pessoa();

                pessoa.PessoaID = Convert.ToInt32(row["PessoaID"]);
                pessoa.Nome = row["Nome"].ToString();
                pessoa.TipoPessoaID = Convert.ToInt32(row["TipoPessoaID"]);
                pessoa.Email = row["Email"].ToString();

                pessoas.Add(pessoa);
            }

            return pessoas;
        }
    }

    


}
