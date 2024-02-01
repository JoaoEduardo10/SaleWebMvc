namespace SalesWebMvc.Models.ViewModels
{
    public class SellerFormViewModel
    {
        public Seller Seller { get; set; }

        public ICollection<Department> Departments { get; set; }


#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
        public SellerFormViewModel()
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
        {

        }

        public SellerFormViewModel(Seller seller, ICollection<Department> departments)
        {
            Seller = seller;
            Departments = departments;
        }
    }
}