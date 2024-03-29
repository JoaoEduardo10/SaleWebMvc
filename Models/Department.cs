namespace SalesWebMvc.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();


#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
        public Department()
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
        {

        }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }


        public void AddSales(Seller seller)
        {
            Sellers.Add(seller);
        }

        public double TotalSales(DateTime start, DateTime end)
        {
            return Sellers.Sum(seller => seller.TotalSales(start, end));
        }
    }
}