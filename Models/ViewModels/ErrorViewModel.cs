namespace SalesWebMvc.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }
    public string Message { get; set; }

#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
    public ErrorViewModel()
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
    {
    }

    public ErrorViewModel(string message)
    {
        Message = message;
    }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
