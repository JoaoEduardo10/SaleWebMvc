using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} 'e obrigatório")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} tamanho maximo é {2} e o minimo é {1}")]
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]

        [Required(ErrorMessage = "{0} 'e obrigatório")]
        [EmailAddress(ErrorMessage = "{0} esta em um formato invalido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} 'e obrigatório")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0}'e obrigatório")]
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double BaseSalary { get; set; }

        [Required(ErrorMessage = "{0} 'e obrigatório")]
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
        public Seller()
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
        { }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime start, DateTime end)
        {
            return Sales.Where(sr => sr.Date >= start && sr.Date <= end).Sum(sr => sr.Amount);
        }
    }

}