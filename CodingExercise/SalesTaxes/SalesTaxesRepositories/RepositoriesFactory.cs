namespace SalesTaxesRepositories
{
    public static class RepositoriesFactory
    {
        public static IBasketRepository GetBasketRepository()
        {
            return new BasketRepository();
        }

        public static IItemRepository GetItemRepository()
        {
            return new ItemRepository();
        }

        public static ICategoryRepository GetCategoryRepository()
        {
            return new CategoryRepository();
        }
    }
}
