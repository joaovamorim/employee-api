using EMPLOYEE.API.Models;
using Dapper;
using Oracle.ManagedDataAccess.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("OracleConnection");

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("getemployee", async () =>
{
    List<Funcionario> listFuncionario = new List<Funcionario>();

    try
    {
        using (var connection = new OracleConnection(connectionString))
        {
            await connection.OpenAsync();

            var query = @"select f.id, f.nome, f.data_nascimento, f.cpf, f.chapa, m.nome as nome_municipio, m.uf as uf_municipio, f.status
                          from funcionario f
                          join municipio m on f.municipio_nasc = m.codigo";

            listFuncionario = (List<Funcionario>)await connection.QueryAsync<Funcionario>(query);

            await connection.CloseAsync();
        }
    }
    catch (System.Exception)
    {
        throw;
    }

    return Results.Ok(listFuncionario);
});

app.MapPost("insertemployee", async (string nome, string data_nascimento, string cpf, string municipio_nasc, int chapa, string status) =>
{
    List<FuncionarioInsert> listFuncionario = new List<FuncionarioInsert>();

    try
    {
        using (var connection = new OracleConnection(connectionString))
        {
            await connection.OpenAsync();

            var query = @"insert into funcionario (nome, data_nascimento, cpf, municipio_nasc, chapa, status)
                            values ('" + nome + "', TO_DATE('" + data_nascimento + "', 'DD-MM-YYYY'), '" + cpf + "', (select codigo from municipio where upper(nome) like '" + municipio_nasc.ToUpper() + "'), '" + chapa + "', '" + status + "')";

            var funcionarios = await connection.QueryAsync<FuncionarioInsert>(query);

            listFuncionario = funcionarios.ToList();

            await connection.CloseAsync();
        }
    }
    catch (System.Exception)
    {
        throw;
    }

    return Results.Ok(true);
});

app.Run();
