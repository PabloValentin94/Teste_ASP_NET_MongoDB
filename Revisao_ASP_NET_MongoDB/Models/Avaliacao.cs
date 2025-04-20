using System.ComponentModel.DataAnnotations;

namespace Revisao_ASP_NET_MongoDB.Models
{
    public class Avaliacao
    {
        private Guid _id;

        [Display(Name = "ID da Avaliação")]
        public Guid Id { get { return this._id; } set { this._id = value; } }

        [Required]
        [Display(Name = "Data de Avaliação")]
        public string? Data_Avaliacao { get; set; }

        [Required]
        public int Nota { get; set; }

        [Required]
        [Display(Name = "ID do Carro Avaliado")]
        public Guid Id_Carro { get; set; }

        [Display(Name = "Carro Avaliado")]
        public Carro? Carro { get; set; }
    }
}
