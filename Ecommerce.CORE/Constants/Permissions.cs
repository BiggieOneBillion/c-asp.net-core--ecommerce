namespace Ecommerce.CORE.Constants;

public static class Permissions
{
    public static class Users
    {
        public const string View = "Users.View";
        public const string Edit = "Users.Edit";
        public const string Delete = "Users.Delete";
    }

    public static class Products
    {
        public const string View = "Products.View";
        public const string Search = "Products.Search";
        public const string Create = "Products.Create";
        public const string Update = "Products.Update";
        public const string Delete = "Products.Delete";
    }

    public static class Categories
    {
        public const string View = "Categories.View";
        public const string Create = "Categories.Create";
        public const string Update = "Categories.Update";
        public const string Delete = "Categories.Delete";
    }

    public static class Inventory
    {
        public const string View = "Inventory.View";
        public const string Manage = "Inventory.Manage";
    }

    public static class Orders
    {
        public const string ViewAll = "Orders.ViewAll";
        public const string ViewOwn = "Orders.ViewOwn";
        public const string Create = "Orders.Create";
        public const string UpdateStatus = "Orders.UpdateStatus";
        public const string Refund = "Orders.Refund";
    }

    public static class Payments
    {
        public const string ViewAll = "Payments.ViewAll";
        public const string ViewOwn = "Payments.ViewOwn";
        public const string Process = "Payments.Process";
    }
}
