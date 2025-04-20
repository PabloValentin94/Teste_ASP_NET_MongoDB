using System.ComponentModel.DataAnnotations;

namespace Revisao_ASP_NET_MongoDB.Models
{
    public class Carro
    {
        private Guid _id;

        [Display(Name = "ID do Carro")]
        public Guid Id { get { return this._id; } set { this._id = value; } }

        [Required]
        public string? Nome { get; set; }

        [Required]
        public string? Marca { get; set; }
    }
}
