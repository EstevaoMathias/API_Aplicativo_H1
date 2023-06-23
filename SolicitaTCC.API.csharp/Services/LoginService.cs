using System.Data;

using System.Data.SqlClient;
using System;
using SolicitaTCC.API.csharp.Models;

namespace SolicitaTCC.API.csharp.Services
{
    public class LoginService
    {
        string connectionStringLocalhost = @"Data Source=LOCALHOST Catalog=APLICACAO_H1;User ID=ESTEVAO;Password=123;Integrated Security=SSPI;TrustServerCertificate=True";
        string connectionString = @"Data Source=201.62.57.93,1434;Initial Catalog=BD042070;User ID=RA042070;Password=042070;TrustServerCertificate=True";

        public Login Login(userLogin userLogin)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);

                DataTable dt1 = new DataTable();
                using (SqlDataAdapter adp = new SqlDataAdapter(@"EXEC PR_LOGIN  @NOME, @SENHA", conn))
                {
                    adp.SelectCommand.CommandType = CommandType.Text;
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@NOME", userLogin.Nome));
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@SENHA", userLogin.Senha));
                    adp.Fill(dt1);

                    if (dt1.Rows.Count <= 0)
                    {
                        throw new Exception("Nome ou senha incorretos!");
                    }
                    else
                    {
                        return new Login(dt1.Rows[0]["PessoaID"].ToString());
                    }
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Login LoginCreate(CreateLogin user)
        {
            try
            { 

                SqlConnection conn = new SqlConnection(connectionString);

                DataTable dt1 = new DataTable();
                using (SqlDataAdapter adp = new SqlDataAdapter(@"EXEC PR_NEW_USER @NOME, @ID_TIPO_PESSOA, @EMAIL, @SENHA", conn))
                {
                    adp.SelectCommand.CommandType = CommandType.Text;
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@NOME", user.Nome));
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@ID_TIPO_PESSOA", user.TipoPessoaID));
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@EMAIL", user.Email));
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@SENHA", user.Senha));
                    adp.Fill(dt1);

                    if (dt1.Rows.Count > 0)
                    {
                        try
                        {
                            return new Login(dt1.Rows[0]["PessoaID"].ToString());

                        }
                        catch (Exception ex)
                        {

                            throw new Exception("E-mail já utilizado!");

                        }
                    }
                    else
                    {
                        throw new Exception("Erro ao gerar cadastro!");
                    }


                    

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Pessoa GetPeople(Login user)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);

                DataTable dt1 = new DataTable();
                using (SqlDataAdapter adp = new SqlDataAdapter(@"EXEC PR_SRC_USER, @ID_PESSOA", conn))
                {
                    adp.SelectCommand.CommandType = CommandType.Text;
                    adp.SelectCommand.Parameters.Add(new SqlParameter("@ID_PESSOA", Convert.ToInt32(user.PessoaID)));

                    adp.Fill(dt1);

                    if (dt1.Rows.Count == 1)
                    {
                        List<Pessoa> response = new Functions().DataTableToPessoaList(dt1);
                        return response[0];
                    }
                    else
                    {
                        throw new Exception("Nenhum usuario foi encontrado!");
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
