using System.Data;

namespace SolicitaTCC.API.csharp.Models
{
    public class getWorker
    {
        public int PessoaID { get; set; }

    }

    public class Workers
    {
        public int PessoaID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }

    public class RequestWorkers
    {
        public int SolicitacaoID { get; set; }
        public Pessoa Aluno { get; set; }
        public Pessoa Professor { get; set; }
        public string NomeProjeto { get; set; }
        public string Descricao { get; set; }

    }

    public class ProjectWorkers
    {
        public int ProjetoID { get; set; }
        public int SolicitacaoID { get; set; }
        public Pessoa Aluno { get; set; }
        public Pessoa Orientador { get; set; }
        public int SituacaoID { get; set; }
        public string NomeProjeto { get; set; }
        public string Descricao { get; set; }
        public string Justificativa { get; set; }

    }

    public class postRequestWorker
    {
        public int AlunoID { get; set; }
        public int OrientadorID { get; set; }
        public string NomeProjeto { get; set; }
        public string Descricao { get; set; }

    }

    public class getRequestsWorker
    {
        public int OrientadorID { get; set; }

    }
    public class updtProjectWorker
    {
        public int ProjetoID { get; set; }
        public int Status { get; set; }
        public int PessoaID { get; set; }
        public string Justificativa { get; set; }

    }
    public class createProjectWorker
    {
        public int AlunoID { get; set; }
        public int OrientadorID { get; set; }
        public int SolicitacaoID { get; set; }

    }

    public class WorkerFunctions
    {
        

        public List<Workers> DataTableListWorkers(DataTable dt)
        {
            List<Workers> professores = new List<Workers>();

            foreach (DataRow row in dt.Rows)
            {
                Workers professor = new Workers();

                professor.PessoaID = Convert.ToInt32(row["PessoaID"]);
                professor.Nome = row["Nome"].ToString();
                professores.Add(professor);
            }

            return professores;
        }

        
    }


}
