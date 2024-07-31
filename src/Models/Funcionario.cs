namespace EMPLOYEE.API.Models
{
    public class Funcionario
    {
        public int Id { get; }
        public string Nome { get; set; }
        public DateTime Data_nascimento { get; set; }
        public string Cpf { get; set; }
        public string Nome_municipio { get; set; }
        public string Uf_municipio { get; set; }
        public int Chapa { get; set; }
        public string Status { get; set; }
    }

    public class FuncionarioInsert
    {
        public int Id { get; }
        public string Nome { get; set; }
        public DateTime Data_nascimento { get; set; }
        public string Cpf { get; set; }
        public int Municipio_nasc { get; set; }
        public int Chapa { get; set; }
        public string Status { get; set; }
    }
}