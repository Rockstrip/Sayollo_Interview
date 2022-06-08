public class UserInfo
{
    private string _email;
    private string _cardNumber;
    private string _expirationDate;
    
    public UserInfo(string email, string cardNumber, string expirationDate)
    {
        _email = email;
        _cardNumber = cardNumber;
        _expirationDate = expirationDate;
    }
}