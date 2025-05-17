using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CRUD_CLIENTES.Model
{
    public class Cliente
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }

        public Cliente(string nome, string email, string cpf)
        {
            Nome = nome;
            Email = email;
            CPF = cpf;
        }
        private Cliente()
        {
        }
    }
}
