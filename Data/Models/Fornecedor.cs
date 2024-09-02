using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Data.Models
{
    public class Fornecedor
    {
        private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        public string Email { get; set; }

        public bool ValidMail()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                return false;
            }

            return EmailRegex.IsMatch(Email);
        }
    }

}
