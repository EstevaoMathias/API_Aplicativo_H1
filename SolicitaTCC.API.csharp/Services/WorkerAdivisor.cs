using System.Data;

using System.Data.SqlClient;
using System;
using SolicitaTCC.API.csharp.Models;

namespace SolicitaTCC.API.csharp.Services
{
    public class WorkerAdivisor
    {
        string connectionStringLocalhost = @"Data Source=LOCALHOST Catalog=APLICACAO_H1;User ID=ESTEVAO;Password=123;Integrated Security=SSPI;TrustServerCertificate=True";
        string connectionString = @"Data Source=201.62.57.93,1434;Initial Catalog=BD042070;User ID=RA042070;Password=042070;TrustServerCertificate=True";

        public List<Workers> getAdvisor(getWorker data)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);

                DataTable dt1 = new DataTable();
                using (SqlDataAdapter adp = new SqlDataAdapter(@"EXEC PR_PEGA_PROFESSOR @ID_PESSOA", conn))
                {
                    adp.SelectCommand.CommandType = CommandType.Text;
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@ID_PESSOA", Convert.ToInt32(data.PessoaID)));


                    adp.Fill(dt1);

                    if (dt1.Rows.Count > 0)
                    {
                        List<Workers> respo = new WorkerFunctions().DataTableListWorkers(dt1);

                        return respo;
                    }
                    else
                    {
                        throw new Exception("Nenhum professor foi encontrado para esse usuario!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool sendRequest(postRequestWorker data)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);

                DataTable dt1 = new DataTable();
                using (SqlDataAdapter adp = new SqlDataAdapter(@"EXEC PR_SOLICITA_TRABALHO @ID_ALUNO, @ID_PROFESSOR, @NOME, @DESCRICAO", conn))
                {
                    adp.SelectCommand.CommandType = CommandType.Text;
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@ID_ALUNO", Convert.ToInt32(data.AlunoID)));
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@ID_PROFESSOR", Convert.ToInt32(data.OrientadorID)));
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@NOME", data.NomeProjeto));
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@DESCRICAO", data.Descricao));


                    adp.Fill(dt1);

                    if (dt1.Rows.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        throw new Exception("Não foi possivel criar a solicitação!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<RequestWorkers> ListRequest(getRequestsWorker data)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                DataTable dt1 = new DataTable();
                using (SqlDataAdapter adp = new SqlDataAdapter(@"EXEC PR_PEGA_SOLICITACAO @ID_ALUNO, @ID_PROFESSOR", conn))
                {
                    adp.SelectCommand.CommandType = CommandType.Text;
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@ID_ALUNO", Convert.ToInt32(data.AlunoID)));
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@ID_PROFESSOR", Convert.ToInt32(data.OrientadorID)));


                    adp.Fill(dt1);

                    if (dt1.Rows.Count > 0)
                    {

                        List<RequestWorkers> response = new List<RequestWorkers>();
                        LoginService loginService = new LoginService();

                        foreach (DataRow row in dt1.Rows)
                        {
                            RequestWorkers request = new RequestWorkers();

                            request.SolicitacaoID = Convert.ToInt32(row["SolicitacaoID"]);
                            request.Aluno = loginService.GetPeople(new Login(row["AlunoSolicitanteID"].ToString()));
                            request.Professor = loginService.GetPeople(new Login(row["ProfessorOrientadorID"].ToString()));
                            request.NomeProjeto = row["NomeProjeto"].ToString();
                            request.Descricao = row["Descricao"].ToString();

                            response.Add(request);
                        }

                        return response;
                    }
                    else
                    {
                        throw new Exception("Nenhuma solcitação para esses parametros!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

        /* public bool CancelRequest(cancelRequestsWorker data)
         {
             try
             {
                 SqlConnection conn = new SqlConnection(connectionString);
                 DataTable dt1 = new DataTable();
                 using (SqlDataAdapter adp = new SqlDataAdapter(@"EXEC PR_REPROVA_SOLICITACAO @ID_PROFESSOR, @ID_SOLICITACAO, @JUSTIFICATIVA", conn))
                 {
                     adp.SelectCommand.CommandType = CommandType.Text;
                     adp.SelectCommand.Parameters.Add(new SqlParameter("@ID_SOLICITACAO", Convert.ToInt32(data.ID_SOLICITACAO)));
                     adp.SelectCommand.Parameters.Add(new SqlParameter("@ID_PROFESSOR", Convert.ToInt32(data.ID_PROFESSOR)));
                     adp.SelectCommand.Parameters.Add(new SqlParameter("@JUSTIFICATIVA", data.JUSTIFICATIVA));


                     adp.Fill(dt1);

                     return true;
                 }
             }
             catch (Exception ex)
             {
                 return false;
                 throw new Exception(ex.Message);
             }
         }
        */

/*
public List<getStageTask> getStageTask()
{
    try
    {
        SqlConnection conn = new SqlConnection(connectionString);
        DataTable dt1 = new DataTable();
        using (SqlDataAdapter adp = new SqlDataAdapter(@"EXEC PR_PEGA_ETAPA_TAREFA", conn))
        {
            adp.SelectCommand.CommandType = CommandType.Text;

            adp.Fill(dt1);

            if (dt1.Rows.Count > 0)
            {

                List<getStageTask> response = new List<getStageTask>();

                foreach (DataRow row in dt1.Rows)
                {
                    getStageTask request = new getStageTask();

                    request.ID_ETAPA = Convert.ToInt32(row["ID_ETAPA"]);
                    request.DESCRICAO = row["DESCRICAO"].ToString();

                    response.Add(request);
                }

                return response;
            }
            else
            {
                throw new Exception("Nenhum estagio para esses parametros!");
            }
        }
    }
    catch (Exception ex)
    {
        throw new Exception(ex.Message);
    }
}


public bool createTask(createTask data)
{
    try
    {
        SqlConnection conn = new SqlConnection(connectionString);
        DataTable dt1 = new DataTable();
        using (SqlDataAdapter adp = new SqlDataAdapter(@"EXEC PR_CRIACAO_TAREFA @ID_PROJETO	,@ID_ETAPA ,@TITULO ,@DESCRICAO	,@DT_INICIO	,@DT_PREVISTA", conn))
        {
            adp.SelectCommand.CommandType = CommandType.Text;
            adp.SelectCommand.Parameters.Add(new SqlParameter("@ID_PROJETO", Convert.ToInt32(data.ID_PROJETO)));
            adp.SelectCommand.Parameters.Add(new SqlParameter("@ID_ETAPA", Convert.ToInt32(data.ID_ETAPA)));
            adp.SelectCommand.Parameters.Add(new SqlParameter("@TITULO", data.TITULO));
            adp.SelectCommand.Parameters.Add(new SqlParameter("@DESCRICAO", data.DESCRICAO));
            adp.SelectCommand.Parameters.Add(new SqlParameter("@DT_INICIO", data.DT_INICIO));
            adp.SelectCommand.Parameters.Add(new SqlParameter("@DT_PREVISTA", data.DT_PREVISTA));


            adp.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    catch (Exception ex)
    {
        return false;
        throw new Exception(ex.Message);
    }
}

public List<getTask> getTask(getTaskWorker data)
{
    try
    {
        SqlConnection conn = new SqlConnection(connectionString);
        DataTable dt1 = new DataTable();
        using (SqlDataAdapter adp = new SqlDataAdapter(@"EXEC PR_PEGA_TAREFA @ID_PROJETO", conn))
        {
            adp.SelectCommand.CommandType = CommandType.Text;
            adp.SelectCommand.Parameters.Add(new SqlParameter("@ID_PROJETO", Convert.ToInt32(data.ID_PROJETO)));

            adp.Fill(dt1);

            if (dt1.Rows.Count > 0)
            {

                List<getTask> response = new List<getTask>();

                foreach (DataRow row in dt1.Rows)
                {
                    getTask request = new getTask();

                    request.ID_TAREFA = Convert.ToInt32(row["ID_TAREFA"]);
                    request.ID_PROJETO = Convert.ToInt32(row["ID_PROJETO"]);
                    request.ID_ETAPA = Convert.ToInt32(row["ID_ETAPA"]);
                    request.ETAPA = row["ETAPA"].ToString();
                    request.TITULO = row["TITULO"].ToString();
                    request.DESCRICAO = row["DESCRICAO"].ToString();
                    request.DT_INICIO = row["DT_INICIO"].ToString();
                    request.DT_PREVISTA = row["DT_PREVISTA"].ToString();
                    request.FL_FINALIZADA = Convert.ToInt32(row["FL_FINALIZADA"]);
                    request.DT_FINALIZADA = row["DT_FINALIZADA"].ToString();
                    request.DT_CADASTRO = row["DT_CADASTRO"].ToString();
                    request.FL_ATIVO = Convert.ToInt32(row["FL_ATIVO"]);


                    response.Add(request);
                }

                return response;
            }
            else
            {
                throw new Exception("Nenhuma tarefa para esses parametros!");
            }
        }
    }
    catch (Exception ex)
    {
        throw new Exception(ex.Message);
    }
}

public bool concludedTask(concludedTask data)
{
    try
    {
        SqlConnection conn = new SqlConnection(connectionString);
        DataTable dt1 = new DataTable();
        using (SqlDataAdapter adp = new SqlDataAdapter(@"EXEC PR_CONCLUI_TAREFA @ID_TAREFA", conn))
        {
            adp.SelectCommand.CommandType = CommandType.Text;
            adp.SelectCommand.Parameters.Add(new SqlParameter("@ID_TAREFA", Convert.ToInt32(data.ID_TAREFA)));


            adp.Fill(dt1);

            return true;
        }
    }
    catch (Exception ex)
    {
        throw new Exception(ex.Message);
    }
}


}
}
*/

