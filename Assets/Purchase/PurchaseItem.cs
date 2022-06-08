public class PurchaseItem
{
    public string title;
    public string item_image;
    public float price;
    public string currency;

    public PurchaseItem(PurchaseItemResponse response)
    {
        title = response.title;
        item_image = response.item_image;
        price = response.price;
        currency = response.currency;
    }

    public PurchaseItem(string title, string itemImage, int price, string currency)
    {
        this.title = title;
        item_image = itemImage;
        this.price = price;
        this.currency = currency;
    }
}